using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementApp.Infrastructure;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.ViewModels
{
    public class CardDetails
    {
        public int Id { get; set; }
        public string Contents { get; set; }
        public string Notes { get; set; }

        public int Column { get; set; }
        public List<SelectListItem> Columns { get; set; } = new List<SelectListItem>();
        public List<CardComments> CardComments { get; set; }
        public string CreatedBy { get; set; }
        public string Color { get; set; }
        public AppUser AssignedTo { get; set; }
        public string AssignedUser { get; set; }
    }
}
