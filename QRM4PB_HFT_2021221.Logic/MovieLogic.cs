using QRM4PB_HFT_2021221.Models;
using QRM4PB_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Logic
{
    class MovieLogic
    {

        IMovieRepository repo;

        public MovieLogic(IMovieRepository repo)
        {
            this.repo = repo;
        }

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
