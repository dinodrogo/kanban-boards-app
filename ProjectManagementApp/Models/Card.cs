using ProjectManagementApp.Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Models
{
  public class Card
    {
        public int Id { get; set; }
        [Required]
        public string Contents { get; set; }
        public string Notes { get; set; }
        public int ColumnId { get; set; }
        public Column Column { get; set; }
        public DateTime CreationDate { get; set; }
        public List<CardComments> CardComments { get; set; } = new List<CardComments>();
        public string CreatedBy { get; set; }
        public string Color { get; set; }
        public AppUser AssignedTo { get; set; }
        public string AssignedUser { get; set; }
    }
}
