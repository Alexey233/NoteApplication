using Microsoft.EntityFrameworkCore;
using NoteApplication.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
            //для доп защиты используется txt файл, чтобы не показывать пароль

            string password;
            string path = "C:\\Users\\fanto\\OneDrive\\Рабочий стол\\password.txt";
            using(StreamReader sr = new StreamReader(path))
            {
                password = sr.ReadToEnd();
            }

            optionsBuilder.UseNpgsql($"Host=localhost;Port=5432;Database=usersdb;Username=postgres;Password={password}");
        }
    }
}
