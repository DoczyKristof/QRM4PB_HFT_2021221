using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Logic
{
    interface IMovieLogic
    {
        void Create(Movie movie);
        IQueryable<Movie> ReadAll();
        void Update(Movie movie);
        void Delete(int id);


        double AverageMoviePrice();

        IEnumerable<KeyValuePair<MovieType, double>>
            AveragePricesByTypes();
    }
}
