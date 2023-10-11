using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.ViewModels
{
    public class BoardDetails
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
        public string CreatedBy { get; set; }
        public string Color { get; set; }
    }
}
