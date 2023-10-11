using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Reflection;
using ProjectManagementApp.Infrastructure;
using ProjectManagementApp.Models;
using ProjectManagementApp.ViewModels;

namespace ProjectManagementApp.Data
{
    public class ProjectManagementAppDbContext :
        IdentityDbContext<AppUser, IdentityRole<Guid>, Guid>
    {

      public ProjectManagementAppDbContext(DbContextOptions<ProjectManagementAppDbContext> options) 
            : base(options)
      {
      }

      public DbSet<Board> Boards { get; set; }
      public DbSet<Card> Cards { get; set; }
      public DbSet<Column> Columns { get; set; }

        public DbSet<CardComments> CardComments { get; set; }

      protected override void OnModelCreating(ModelBuilder builder)
      {
          base.OnModelCreating(builder);

          builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
      }

      public DbSet<ProjectManagementApp.ViewModels.AddComment> AddComment { get; set; }
    }
}
