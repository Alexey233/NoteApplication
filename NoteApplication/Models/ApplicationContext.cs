using Microsoft.EntityFrameworkCore;
using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NoteApplication.Models
{
    internal class ApplicationContext : DbContext
    {
        public DbSet<Note> Note { get; set; } = null!;

        public ApplicationContext()
        {

        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password=19781978");
        }
    }
}
