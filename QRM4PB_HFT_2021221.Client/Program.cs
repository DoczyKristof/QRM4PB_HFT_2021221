using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QRM4PB_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(5000);

            RestService service = new RestService("http://localhost:20463");
           
            var cinemas = service.Get<Cinema>("Cinema");
            var movies = service.Get<Movie>("Movie");
            var rooms = service.Get<Room>("Room");

            //roomlogic
            var RoomWithLeastIncome = service.GetSingle<Room>("stat/RoomWithLeastIncome");
            var CinemasThatHaveMovie = service.Get<Cinema>("stat/CinemasThatHaveMovie");
            var RoomsThatHaveMovie = service.Get<Room>("stat/RoomsThatHaveMovie");
            //movielogic
            var averageMoviePrice = service.GetSingle<double>("stat/AverageMoviePrice");
            var AveragePricesByTypes = service.Get<KeyValuePair<MovieType, double>>
                ("stat/AveragePricesByTypes");
            var NumOfMoviesInTypes = service.Get<KeyValuePair<MovieType, int>>
                ("stat/NumOfMoviesInTypes");
            //cinemalogic
            var AvgCinemaSize = service.GetSingle<int>("stat/AvgCinemaSize");
            ;
        }
    }
}
