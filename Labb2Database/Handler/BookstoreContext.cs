using Labb2Database.DataModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Labb2Database.Handler
{
    public class BookstoreContext : DbContext
    {
        public DbSet<Authors> Authors { get; set; }
        public DbSet<Books> Books { get; set; }
        public DbSet<Customers> Customers { get; set; }
        public DbSet<Orders> Orders { get; set; }
        public DbSet<StockBalance> StockBalance { get; set; }
        public DbSet<Stores> Stores { get; set; }

        public List<StockBalance> GetStockBalanceInfo()
        {
            return StockBalance
                .Include(sb => sb.Stores)
                .Include(sb => sb.Books)
                .ToList();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<StockBalance>()
                .HasKey(sb => new { sb.StoreID, sb.ISBN });

            modelBuilder.Entity<StockBalance>().ToTable("StockBalance");
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Labb1;Integrated Security=True");
        }
    }
}
