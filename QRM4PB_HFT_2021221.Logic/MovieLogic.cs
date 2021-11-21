using QRM4PB_HFT_2021221.Models;
using QRM4PB_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Logic
{
    public class MovieLogic
    {

        IMovieRepository repo;

        public MovieLogic(IMovieRepository repo)
        {
            this.repo = repo;
        }
        //NON CRUD
        public double AveragePrice()
        {
            return repo
                .ReadAll()
                .Average(x => x.Price) ?? 0;
        }

        public IEnumerable<KeyValuePair<MovieType, double>> AveragePricesByTypes()
        {
            return repo
                .ReadAll()
                .GroupBy(x => x.Type)
                .Select(x => new KeyValuePair<MovieType, double>(
                   x.Key, x.Average(c => c.Price) ?? 0));
        }

        /*
        public IEnumerable<KeyValuePair<MovieType, int>> NumOfMoviesInTypes()
        {
            return repo
                .ReadAll()
                .GroupBy(x => x.Type)
                .Select(x => new KeyValuePair<MovieType, int>(
                   x.Key, *count titles*));
        }
        */
        //CRUD
        public void Create(Movie movie)
        {
            repo.Create(movie);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public IQueryable<Movie> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Movie movie)
        {
            repo.Update(movie);
        }
    }
}
