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
        //only one dbset, not sure it would be a problem in the future
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
            //Cinema marosMozi = new Cinema() { Id = 3, Name = "Maros Mozi" };
            //Cinema sugarMozi = new Cinema() { Id = 4, Name = "Sugár Mozi" };

            Room ccArena1 = new Room() { Id = 1, CinemaId = ccArena.Id, RoomNumber = 1 };
            Room ccArena2 = new Room() { Id = 2, CinemaId = ccArena.Id, RoomNumber = 2 };
            Room ccArena3 = new Room() { Id = 3, CinemaId = ccArena.Id, RoomNumber = 3 };
            Room ccMamut1 = new Room() { Id = 4, CinemaId = ccMamut.Id, RoomNumber = 1 };
            Room ccMamut2 = new Room() { Id = 5, CinemaId = ccMamut.Id, RoomNumber = 2 };


            Movie movie1 = new Movie() { Id = 1, Title = "Anya" ,Price = 107, RoomId = ccArena2.Id };
            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasOne(room => room.Cinema)
                    .WithMany(cinema => cinema.Rooms)
                    .HasForeignKey(room => room.CinemaId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Movie>(entity =>
            {
                entity.HasOne(movie => movie.Room)
                    .WithOne(room => room.Movie)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });



            modelBuilder.Entity<Cinema>().HasData(ccArena, ccMamut/*, marosMozi, sugarMozi*/);
            modelBuilder.Entity<Room>().HasData(ccArena1, ccArena2, ccArena3,
                ccMamut1, ccMamut2);
            modelBuilder.Entity<Movie>().HasData(movie1);

        }


    }
}
