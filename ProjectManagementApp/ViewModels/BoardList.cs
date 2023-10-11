using ProjectManagementApp.Models;
using System.Collections.Generic;

namespace ProjectManagementApp.ViewModels
{
    public class BoardList
    {
        public List<Board> Boards = new List<Board>();

        public class Board
        {
            public int Id { get; set; }
            public string Title { get; set; }
            public List<Column> Columns { get; set; } = new List<Column>();
            public string CreatedBy { get; set; }
            public string Color { get; set; }
        }
    }
}
