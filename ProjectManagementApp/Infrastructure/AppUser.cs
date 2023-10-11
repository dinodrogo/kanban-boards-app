using System;
using Microsoft.AspNetCore.Identity;

namespace ProjectManagementApp.Infrastructure
{
    public class AppUser : IdentityUser<Guid>
    {
        public string DisplayName { get; set; }
        public string PicturePath { get; internal set; }
    }
}