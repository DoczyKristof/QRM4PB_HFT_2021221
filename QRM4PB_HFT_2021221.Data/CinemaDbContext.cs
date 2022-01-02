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
        public virtual DbSet<Movie> Movies { get; set; }
        public virtual DbSet<Room> Rooms { get; set; }

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
            //cinemas
            Cinema ccArena = new Cinema() { Id = 1, Name = "Cinema City Aréna" };

            Cinema ccMamut = new Cinema() { Id = 2, Name = "Cinema City Mamut" };

            Cinema marosMozi = new Cinema() { Id = 3, Name = "Maros Mozi" };

            Cinema sugarMozi = new Cinema() { Id = 4, Name = "Sugár Mozi" };

            //rooms
            Room ccArena1 = new Room() { Id = 1, CinemaId = ccArena.Id, RoomNumber = 1 };
            Room ccArena2 = new Room() { Id = 2, CinemaId = ccArena.Id, RoomNumber = 2 };
            Room ccArena3 = new Room() { Id = 3, CinemaId = ccArena.Id, RoomNumber = 3 };

            Room ccMamut1 = new Room() { Id = 4, CinemaId = ccMamut.Id, RoomNumber = 1 };
            Room ccMamut2 = new Room() { Id = 5, CinemaId = ccMamut.Id, RoomNumber = 2 };
            Room ccMamut3 = new Room() { Id = 6, CinemaId = ccMamut.Id, RoomNumber = 3 };
            
            Room marosMozi1 = new Room() { Id = 7, CinemaId = marosMozi.Id, RoomNumber = 1 };
            Room marosMozi2 = new Room() { Id = 8, CinemaId = marosMozi.Id, RoomNumber = 2 };
            
            Room sugarMozi1 = new Room() { Id = 9, CinemaId = sugarMozi.Id, RoomNumber = 1 };
            Room sugarMozi2 = new Room() { Id = 10, CinemaId = sugarMozi.Id, RoomNumber = 2 };
            Room sugarMozi3 = new Room() { Id = 11, CinemaId = sugarMozi.Id, RoomNumber = 3 };

            //movies
            Movie movie1 = new Movie()
            {
                Id = 1,
                Title = "Marvel's Eternals",
                Price = 2500,
                Type = MovieType.Action,
                Length = "2 hours 37 minutes",
                RoomId = ccArena1.Id
            };
            Movie movie2 = new Movie()
            {
                Id = 2,
                Title = "Spider-Man No way home",
                Price = 2500,
                Type = MovieType.Action,
                Length = "2 hours 30 minutes",
                RoomId = ccArena1.Id
            };
            Movie movie3 = new Movie()
            {
                Id = 3,
                Title = "Red Notice",
                Price = 1900,
                Type = MovieType.Comedy,
                Length = "1 hour 55 minutes",
                RoomId = ccArena2.Id
            };
            Movie movie4 = new Movie()
            {
                Id = 4,
                Title = "Passing",
                Price = 1690,
                Type = MovieType.Drama,
                Length = "1 hour 38 minutes",
                RoomId = ccArena3.Id
            };
            Movie movie5 = new Movie()
            {
                Id = 5,
                Title = "Hive",
                Price = 1890,
                Type = MovieType.Drama,
                Length = "1 hour 24 minutes",
                RoomId = ccArena3.Id
            };
            Movie movie6 = new Movie()
            {
                Id = 6,
                Title = "A gift from Bob",
                Price = 1290,
                Type = MovieType.Drama,
                Length = "1 hour 32 minutes",
                RoomId = ccMamut1.Id
            };
            Movie movie7 = new Movie()
            {
                Id = 7,
                Title = "Nightbooks",
                Price = 0,
                Type = MovieType.Horror,
                Length = "1 hour 43 minutes",
                RoomId = ccMamut2.Id
            };
            Movie movie8 = new Movie()
            {
                Id = 8,
                Title = "Candyman",
                Price = 0,
                Type = MovieType.Horror,
                Length = "1 hour 31 minutes",
                RoomId = ccMamut2.Id
            };
            Movie movie9 = new Movie()
            {
                Id = 9,
                Title = "Ghostbusters: Afterlife",
                Price = 1490,
                Type = MovieType.Fantasy,
                Length = "2 hours 4 minutes",
                RoomId = marosMozi1.Id
            };
            Movie movie10 = new Movie()
            {
                Id = 10,
                Title = "Nagykarácsony",
                Price = 1990,
                Type = MovieType.Romance,
                Length = "1 hour 48 minutes",
                RoomId = marosMozi2.Id
            };
            Movie movie11 = new Movie()
            {
                Id = 11,
                Title = "Venom: Let there be carnage",
                Price = 0,
                Type = MovieType.Action,
                Length = "1 hour 37 minutes",
                RoomId = sugarMozi2.Id
            };
            Movie movie12 = new Movie()
            {
                Id = 12,
                Title = "King Richard",
                Price = 1690,
                Type = MovieType.Drama,
                Length = "2 hours 18 minutes",
                RoomId = sugarMozi3.Id
            };

            //
            modelBuilder.Entity<Cinema>(entity =>
            {
                entity.HasMany(cinema => cinema.Rooms)
                    .WithOne(room => room.Cinema)
                    .HasForeignKey(room => room.CinemaId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            modelBuilder.Entity<Room>(entity =>
            {
                entity.HasMany(room => room.Movies)
                    .WithOne(movie => movie.Room)
                    .HasForeignKey(movie => movie.RoomId)
                    .OnDelete(DeleteBehavior.Cascade);
            });

            //
            modelBuilder.Entity<Cinema>().HasData(ccArena, ccMamut, marosMozi, sugarMozi);
            modelBuilder.Entity<Room>().HasData(ccArena1, ccArena2, ccArena3,
                ccMamut1, ccMamut2, ccMamut3, marosMozi1, marosMozi2, 
                sugarMozi1, sugarMozi2, sugarMozi3);
            modelBuilder.Entity<Movie>().HasData(movie1, movie2, movie3, movie4, movie5, movie6,
                movie7, movie8, movie9, movie10, movie11, movie12);
        }
    }
}
