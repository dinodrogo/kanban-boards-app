using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using ProjectManagementApp.Data;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public class BoardService
    {
        private readonly ProjectManagementAppDbContext _dbContext;

        public BoardService(ProjectManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public BoardList ListBoard()
        {
            var model = new BoardList();

            foreach (var board in _dbContext.Boards)
            {
                model.Boards.Add(new BoardList.Board
                {
                    Id = board.Id,
                    Title = board.Title,
                    CreatedBy = board.CreatedBy,
                    Color= board.Color,
                });
            }

            return model;
        }

        public BoardView GetBoard(int id)
        {
            var model = new BoardView();

            var board = _dbContext.Boards
                .Include(b => b.Columns)
                .ThenInclude(c => c.Cards).ThenInclude(u => u.AssignedTo)
                .SingleOrDefault(x => x.Id == id);

            if (board == null)
                return model;
            model.Id = board.Id;
            model.Title = board.Title;
            model.CreatedBy = board.CreatedBy;

            foreach (var column in board.Columns)
            {
                var modelColumn = new BoardView.Column
                {
                    Title = column.Title,
                    Id = column.Id
                };

                foreach (var card in column.Cards)
                {
                    var modelCard = new BoardView.Card
                    {
                        Id = card.Id,
                        Content = card.Contents,
                        Notes = card.Notes,
                        CreationDate = card.CreationDate,
                        CreatedBy = card.CreatedBy,
                        Color = card.Color,
                        AssignedUser = card.AssignedUser,
                        AssignedTo = card.AssignedTo,
                    };

                    modelColumn.Cards.Add(modelCard);
                }

                model.Columns.Add(modelColumn);
            }

            return model;
        }

        public void AddCard(AddCard viewModel)
        {
            var board = _dbContext.Boards
                .Include(b => b.Columns)
                .SingleOrDefault(x => x.Id == viewModel.Id);

            if (board != null)
            {
                var firstColumn = board.Columns.FirstOrDefault();
                var secondColumn = board.Columns.FirstOrDefault();
                var thirdColumn = board.Columns.FirstOrDefault();

                if (firstColumn == null || secondColumn == null || thirdColumn == null)
                {
                    firstColumn = new Models.Column { Title = "Todo" };
                    secondColumn = new Models.Column { Title = "Doing" };
                    thirdColumn = new Models.Column { Title = "Done" };
                    board.Columns.Add(firstColumn);
                    board.Columns.Add(secondColumn);
                    board.Columns.Add(thirdColumn);
                }

                firstColumn.Cards.Add(new Models.Card
                {
                    Contents = viewModel.Contents,
                    CreationDate = DateTime.Now,
                    CreatedBy = viewModel.CreatedBy
                });
            }

            _dbContext.SaveChanges();
        }

        public void AddBoard(NewBoard viewModel)
        {
            _dbContext.Boards.Add(new Models.Board
            {
                Title = viewModel.Title,
                CreatedBy = viewModel.CreatedBy
            });

            _dbContext.SaveChanges();
        }

        public void DeleteBoard(int id)
        { 
            var board = _dbContext.Boards
                .Include(i => i.Columns)
                .ThenInclude(x => x.Cards)
                .ThenInclude(y=>y.CardComments)
                .FirstOrDefault(i => i.Id == id);

            _dbContext.Boards.Remove(board);
            _dbContext.SaveChanges();
        }

        public void Move(MoveCardCommand command)
        {
            var card = _dbContext.Cards.SingleOrDefault(x => x.Id == command.CardId);
            card.ColumnId = command.ColumnId;
            _dbContext.SaveChanges();
        }

        public void UpdateColor(int id, string color)
        {
            var board = _dbContext.Boards.SingleOrDefault(x => x.Id == id);
                        board.Color= color;
            _dbContext.SaveChanges();
        }
    }
}
