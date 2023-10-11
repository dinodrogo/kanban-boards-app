using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.ViewModels
{
    public class AddComment
    {
        public int Id { get; set; }

        [Required]
        public string Comment { get; set; }
        public Card Card { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
