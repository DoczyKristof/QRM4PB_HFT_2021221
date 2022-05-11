﻿using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Logic
{
    public interface IMovieLogic
    {
        //crud
        Movie ReadOne(int id);
        void Create(Movie movie);
        IQueryable<Movie> ReadAll();
        void Update(Movie movie);
        void Delete(int id);

        //noncrud
        public IEnumerable<Room> LeastIncome();
        int AverageMoviePrice();

        public IEnumerable<KeyValuePair<MovieType, double>>
            AveragePricesByTypes();

        public IEnumerable<KeyValuePair<MovieType, int>>
            NumOfMoviesInTypes();
    }
}
