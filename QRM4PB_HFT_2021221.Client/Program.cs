using QRM4PB_HFT_2021221.Models;
using System;
using System.Linq;

namespace QRM4PB_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            System.Threading.Thread.Sleep(10000);

            RestService service = new RestService("http://localhost:20463");
           
            var cinemas = service.Get<Cinema>("Cinema");
            var movies = service.Get<Movie>("Movie");
            var rooms = service.Get<Room>("Room");

            var noncrud = service.GetSingle<double>("stat/AverageMoviePrice");
            ;
        }
    }
}
