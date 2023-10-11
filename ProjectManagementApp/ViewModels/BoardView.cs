using ProjectManagementApp.Infrastructure;
using System;
using System.Collections.Generic;

namespace ProjectManagementApp.ViewModels
{
    public class BoardView
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public List<Column> Columns { get; set; } = new List<Column>();
        public string CreatedBy { get; set; }

        public class Column
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public List<Card> Cards { get; set; } = new List<Card>();
        }

        public class Card
        {
            public int Id { get; set; }
            public string Content { get; set; }
            public string Notes { get; set; }
            public List<CardComments> CardComments { get; set; } = new List<CardComments>();
            public DateTime CreationDate { get; set; }
            public string CreatedBy { get; set; }
            public string Color { get; set; }
            public AppUser AssignedTo { get; set; }
            public string AssignedUser { get; set; }
        }
        public class CardComments
        {
            public int Id { get; set; }
            public string Comment { get; set; }
            public string CreatedBy { get; set; }
        }
    }
}
