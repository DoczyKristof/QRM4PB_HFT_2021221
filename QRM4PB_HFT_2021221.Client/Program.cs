using QRM4PB_HFT_2021221.Data;
using QRM4PB_HFT_2021221.Logic;
using QRM4PB_HFT_2021221.Models;
using QRM4PB_HFT_2021221.Repository;
using System;
using System.Linq;

namespace QRM4PB_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CinemaDbContext cc = new CinemaDbContext();
            MovieRepository repository = new MovieRepository(cc);
            MovieLogic logic = new MovieLogic(repository);

            //Console.WriteLine(logic.AverageMoviePrice());

            //var anya = logic.AveragePricesByTypes().ToList();
            //foreach (var item in anya)
            //{
            //    Console.WriteLine(item);
            //}

            Movie film = new Movie()
            {
                Price = 69,
                Title = "kuki",
                RoomId = 1
            };
            Movie film2 = new Movie()
            {
                Id = 13,
                Price = 69420,
                Title = "kuki kaki",
                RoomId = 1
            };

            logic.Create(film);

            var xd = logic.ReadAll().ToList();

            logic.Update(film2);

            xd = logic.ReadAll().ToList();

            logic.Delete(13);

            xd = logic.ReadAll().ToList();

            ;



            /*
             var res1 = cc.Cinemas.ToList();

             var res2 = cc.Cinemas.Find(1).Name;

             var res3 = cc.Movies
                 .Where(x => x.Type == Models.MovieType.Action)
                 .Average(x => x.Price);
             */
        }
    }
}
