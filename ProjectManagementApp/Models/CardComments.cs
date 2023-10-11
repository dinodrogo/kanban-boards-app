using System;
using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApp.Models
{
    public class CardComments
    {
        public int Id { get; set; }
        [Required]
        public string Comment { get; set; }
        public Card Card { get; set; }
        public DateTime CreationDate { get; set; }
        public string CreatedBy { get; set; }
    }
}
