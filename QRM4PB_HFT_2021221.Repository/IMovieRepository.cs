using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Repository
{
    public interface IMovieRepository
    {
        void Create(Movie movie);
        Movie ReadOne(int id);
        IQueryable<Movie> ReadAll();
        void Update(Movie movie);
        void Delete(int id);
    }
}
