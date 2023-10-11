using Microsoft.AspNetCore.Mvc;
using System;
using ProjectManagementApp.Models;
using ProjectManagementApp.Services;
using ProjectManagementApp.ViewModels;
using ProjectManagementApp.Infrastructure;

namespace ProjectManagementApp.Controllers
{
    public class CardController : Controller
    {
        private readonly CardService _cardService;

        public CardController(CardService cardService)
        {
            _cardService = cardService;
        }
        
        [HttpGet]
        public IActionResult Details(int id)
        {
            var viewModel = _cardService.GetDetails(id);

            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Update(CardDetails cardDetails)
        {
            _cardService.Update(cardDetails);

            TempData["Message"] = "Saved card Details";

            return RedirectToAction(nameof(Details), new { id = cardDetails.Id });
        }
        [HttpPost]
        public IActionResult Delete(Card card, string board)
        {
            try
            {
                _cardService.Delete(card.Id);

                return RedirectToAction("Index", "Board", new {id = board});
            }
            catch (Exception)
            {
                return RedirectToAction(nameof(Index));
            }
        }
        public IActionResult AddComment(int id)
        {
            return View();
        }

        [HttpPost]
        public IActionResult AddComment(AddComment viewModel)
        {
            if (!ModelState.IsValid) return View(viewModel);

            _cardService.AddComment(viewModel);

            return RedirectToAction("Details", "Card", new { id = viewModel.Id.ToString()});
        }

        [HttpPost]
        public IActionResult UpdateColor(int id, string Color, int boardId)
        {
            _cardService.UpdateColor(id, Color);

            TempData["Message"] = "Saved card Details";
            return RedirectToAction("Index", "Board", new {id = boardId});
        }
        public IActionResult AssignToCard(int cardId, string user)
        {

            var viewModel = _cardService.GetDetails(cardId);
            _cardService.AssignToCard(cardId, user);
            return RedirectToAction("Details", "Card", viewModel);
        }
    }
}