using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using ProjectManagementApp.Models;

namespace ProjectManagementApp.ViewModels
{
    public class AddCard
    {
        public int Id { get; set; }

        [Required]
        public string Contents { get; set; }
        public DateTime CreationDate {get; set; }
        public List<CardComments> CardComments { get; set; }
        public string CreatedBy { get; set; }
    }
}
