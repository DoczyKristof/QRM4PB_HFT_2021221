using QRM4PB_HFT_2021221.Data;
using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Repository
{
    public class MovieRepository : IMovieRepository
    {
        CinemaDbContext context;

        public MovieRepository(CinemaDbContext context)
        {
            this.context = context;
        }

        public void Create(Movie movie)
        {
            context.Movies.Add(movie);
            context.SaveChanges();
        }


        public Movie ReadOne(int id)
        {
            return context
                .Movies
                .FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<Movie> ReadAll()
        {
            return context.Movies;
        }

        public void Update(Movie movie)
        {
            Movie old = ReadOne(movie.Id);

            old.Title = movie.Title;
            old.Price = movie.Price;
            old.Length = movie.Length;
            old.Type = movie.Type;
            old.RoomId = movie.RoomId;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Movies.Remove(ReadOne(id));
            context.SaveChanges();
        }
    }
}
