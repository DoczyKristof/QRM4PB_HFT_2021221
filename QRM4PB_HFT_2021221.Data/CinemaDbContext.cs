using Microsoft.EntityFrameworkCore;
using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Data
{
    public class CinemaDbContext : DbContext
    {
        public virtual DbSet<Cinema> Cinemas { get; set; }
        public CinemaDbContext()
        {
            Database.EnsureCreated();
        }

        public CinemaDbContext(DbContextOptions<CinemaDbContext> options)
            :base(options){}

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                optionsBuilder.
                    UseLazyLoadingProxies().
                    UseSqlServer(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=|DataDirectory|\Database.mdf;Integrated Security=True");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Cinema ccArena = new Cinema() { Id = 1, Name = "Cinema City Aréna" };
            Cinema ccMamut = new Cinema() { Id = 2, Name = "Cinema City Mamut" };
            Cinema marosMozi = new Cinema() { Id = 3, Name = "Maros Mozi" };
            Cinema sugarMozi = new Cinema() { Id = 4, Name = "Sugár Mozi" };

            modelBuilder.Entity<Cinema>().HasData(ccArena, ccMamut, marosMozi, sugarMozi);
        }


    }
}
