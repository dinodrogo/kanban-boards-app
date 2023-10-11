using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectManagementApp.Data;
using ProjectManagementApp.Infrastructure;
using ProjectManagementApp.Models;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Services
{
    public class CardService
    {
        private readonly ProjectManagementAppDbContext _dbContext;

        public CardService(ProjectManagementAppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        
        public CardDetails GetDetails(int id)
        {
            var card = _dbContext
                .Cards
                .Include(c => c.Column).Include(k => k.CardComments).Include(u => u.AssignedTo)
                .SingleOrDefault(x => x.Id == id);

            if (card == null) 
                return new CardDetails();
           
            // retrieve boards
            var board = _dbContext
                .Boards
                .Include(b => b.Columns)
                .SingleOrDefault(x => x.Id == card.Column.BoardId);

            // map board columns
            if (board != null) 
            {
                var availableColumns = board
                    .Columns
                    .Select(x => new SelectListItem
                    {
                        Text = x.Title,
                        Value = x.Id.ToString()
                    });


                return new CardDetails
                {
                    Id = card.Id,
                    Contents = card.Contents,
                    Notes = card.Notes,
                    Columns = availableColumns.ToList(), // list available columns
                    Column = card.ColumnId, // map current column
                    CardComments = card.CardComments,
                    CreatedBy = card.CreatedBy,
                    Color = card.Color,
                    AssignedUser = card.AssignedUser,
                    AssignedTo = card.AssignedTo,
                };
            }
            return null;
        }

        public void Update(CardDetails cardDetails)
        {
            var card = _dbContext.Cards.SingleOrDefault(x => x.Id == cardDetails.Id);
            card.Contents = cardDetails.Contents;
            card.Notes = cardDetails.Notes;
            card.ColumnId = cardDetails.Column;
            card.CardComments = cardDetails.CardComments;
            card.CreatedBy = cardDetails.CreatedBy;
            card.Color = cardDetails.Color;
            card.AssignedUser = cardDetails.AssignedUser;
            card.AssignedTo = cardDetails.AssignedTo;

            _dbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var card = _dbContext.Cards
                .Include(c => c.CardComments)
                .SingleOrDefault(x => x.Id == id);
            _dbContext.Remove(card ?? throw new Exception($"Could not remove {(Card) null}"));
            
            _dbContext.SaveChanges();
        }

        public void AddComment(AddComment viewModel)
        {
            var card = _dbContext.Cards
                .Include(c => c.CardComments)
                .SingleOrDefault(x => x.Id == viewModel.Id);
            CardComments comment = new CardComments();
            comment.Comment = viewModel.Comment;
            comment.CreatedBy = viewModel.CreatedBy;
            comment.CreationDate = DateTime.Now;
            card.CardComments.Add(comment);
            _dbContext.Update(card);
            _dbContext.SaveChanges();
        }
        public void UpdateColor(int id, string color)
        {
            var card = _dbContext.Cards.SingleOrDefault(x => x.Id == id);
            card.Color = color;
            _dbContext.SaveChanges();
        }
        public void AssignToCard(int id, string user)
        {
            var card = _dbContext.Cards.SingleOrDefault(x => x.Id == id);
            var test = _dbContext.Users.SingleOrDefault(x => x.Email == user);
            card.AssignedTo = test;
            _dbContext.SaveChanges();
        }
    }
}