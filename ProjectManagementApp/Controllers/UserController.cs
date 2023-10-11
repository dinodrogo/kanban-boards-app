using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ProjectManagementApp.Infrastructure;
using ProjectManagementApp.ViewModels;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using System;

namespace ProjectManagementApp.Controllers
{
    public class UserController : Controller
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;

        public UserController(UserManager<AppUser> userManager,
            SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if(ModelState.IsValid)
            {
                var user = new AppUser 
                { 
                    UserName = model.Email,
                    Email = model.Email,
                    PicturePath = "/Images/default.jpg"
                };

                var result = await _userManager
                    .CreateAsync(user, model.Password);

                if(result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
            }

            return View(model);
        } 
        
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(
                    model.Email,
                    model.Password,
                    model.RememberMe,
                    lockoutOnFailure: false);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(HomeController.Index), "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }
            }

            return View(model);
        }

        public async Task<IActionResult> SignOut()
        {
            await _signInManager.SignOutAsync();
            
            return RedirectToAction(nameof(Login));
        }
        public IActionResult ListUsers(int id)
        {
            var users = _userManager.Users;
            ViewBag.cardId = id;
            return View(users);
        }
        public IActionResult UploadPhoto()
        {
            return View();
        }
        public IActionResult AddPhoto(string path)
        {
            if (User.Identity.Name != null) { }
            var user = _userManager.Users.Where(x => x.Email == User.Identity.Name).FirstOrDefault();
            user.PicturePath = path;
            _userManager.UpdateAsync(user);
            return RedirectToAction(nameof(UploadPhoto));
        }
        [HttpPost]
        public async Task<ActionResult> AddPhotooAsync(IFormFile postedFile)
        {
            string fileName="";
            //Extract Image File Name.
            if (postedFile.ContentType == "image/jpeg" || postedFile.ContentType == "image/png")
            {
                fileName = Path.GetFileName(DateTime.Now.Millisecond.ToString() + DateTime.Now.DayOfYear.ToString() + postedFile.FileName);

                //Set the Image File Path.
                string filePath = "wwwroot/Images/" + fileName;
                        using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                        {
                            await postedFile.CopyToAsync(fileStream);
                        }
            }
            else
            {

                return RedirectToAction("AddPhoto", "User", new { path = "/Images/" +  "default.jpg"});
            }
            return RedirectToAction("AddPhoto", "User", new { path="/Images/"+fileName });
        }
    }
}