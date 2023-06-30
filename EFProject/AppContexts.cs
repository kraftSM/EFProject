using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFProject.Entities;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EFProject
{
    public class AppContext : DbContext
    {
        // Объекты = таблицы Users Books
        public DbSet<User> Users { get; set; }
        public DbSet<Book> Books { get; set; }
        public string MsSqlConnection => @"Data Source=PC3\SQLEXPRESS;Database=EF;Trusted_Connection=True;TrustServerCertificate=True";

        public AppContext()
        {
            //Database.EnsureDeleted();
            //Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(MsSqlConnection);
            //Console.Write($"---- UseSqlServer = {MsSqlConnection}  \n");
        }
    }
}
