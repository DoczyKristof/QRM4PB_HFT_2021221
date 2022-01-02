using ConsoleTools;
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
            
            //nw
            //var RoomWithLeastIncome = service.GetSingle<Room>("stat/RoomWithLeastIncome");
            //var AvgCinemaSize = service.GetSingle<int>("stat/AvgCinemaSize");
            
            //methods i need
            void theEndThing()
            {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.WriteLine("\nHit enter to get back to the Menu");
                Console.ResetColor();
                Console.ReadLine();
            }
            void List(string listWhat)
            {             
                switch (listWhat)
                {
                    case "cinema":
                        var cinemas = service.Get<Cinema>("Cinema");
                        foreach (var cinema in cinemas)
                        {
                            Console.WriteLine(cinema.Name);
                        }
                        break;
                    case "movie":
                        var movies = service.Get<Movie>("Movie");
                        foreach (var movie in movies)
                        {
                            Console.WriteLine("{0} ({1}Ft) [{2}, {3}]",
                                movie.Title, movie.Price, movie.Type, movie.Length);
                        }
                        break;
                    case "room":
                        var rooms = service.Get<Room>("Room");
                        foreach (var room in rooms)
                        {
                            Console.WriteLine(
                                "Cinema Id: " + room.CinemaId +
                                ": Roomnumber: " + room.RoomNumber +
                                " (" + room.Movies.FirstOrDefault()?.Title + ")");
                        }
                        break;
                    default:
                        Console.WriteLine("Error!");
                        break;
                }
                theEndThing();
            }
            void NonCrud(int num)
            {
                switch (num)
                {
                    case 1:
                        var CinemasThatHaveMovie = service.Get<Cinema>("stat/CinemasThatHaveMovie");
                        foreach (var cinema in CinemasThatHaveMovie)
                        {
                            Console.WriteLine(cinema.Name);
                        }
                        break;
                    case 2:
                        var RoomsThatHaveMovie = service.Get<Room>("stat/RoomsThatHaveMovie");
                        foreach (var room in RoomsThatHaveMovie)
                        {
                            Console.WriteLine(
                                "Cinema Id: " + room.CinemaId +
                                ": Roomnumber: " + room.RoomNumber +
                                " (" + room.Movies.FirstOrDefault().Title + ")");
                        }
                        break;
                    case 3:
                        var averageMoviePrice = service.GetSingle<int>("stat/AverageMoviePrice");
                        Console.WriteLine("The average price for a movie is " 
                            + averageMoviePrice + "Ft");
                        break;
                    case 4:
                        var AveragePricesByTypes = service.Get<KeyValuePair<MovieType, double>>
                                ("stat/AveragePricesByTypes");
                        foreach (var item in AveragePricesByTypes)
                        {
                            Console.WriteLine(item.Key + ": " + Math.Round(item.Value) + "Ft");
                        }
                        break;
                    case 5:
                        var NumOfMoviesInTypes = service.Get<KeyValuePair<MovieType, int>>
                            ("stat/NumOfMoviesInTypes");
                        foreach (var item in NumOfMoviesInTypes)
                        {
                            Console.WriteLine(item.Key + ": " + item.Value + " movie(s)");
                        }
                        break;
                    default:
                        Console.WriteLine("Error!");
                        break;
                }
                theEndThing();
            }

            //the menu
            var listMenu = new ConsoleMenu(args, level: 1)
                      .Add("Cinemas", () => List("cinema"))
                      .Add("Movies", () => List("movie"))
                      .Add("Rooms", () => List("room"))
                      .Add("Back", ConsoleMenu.Close)
                      .Configure(config =>
                      {
                          config.Selector = "--> ";
                          config.EnableFilter = false;
                          config.Title = "What to list?";
                          config.EnableBreadcrumb = true;
                          config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                      });

            var nonCrudMenu = new ConsoleMenu(args, level: 1)
               .Add("Cinemas that have movie", () => NonCrud(1))
               .Add("Rooms that have movie", () => NonCrud(2))
               .Add("Average movie price", () => NonCrud(3))
               .Add("Average prices by types", () => NonCrud(4))
               .Add("Num of movies in types", () => NonCrud(5))
               .Add("Back", ConsoleMenu.Close)
               .Configure(config =>
               {
                   config.Selector = "--> ";
                   config.EnableFilter = false;
                   config.Title = "Non-CRUD queries";
                   config.EnableBreadcrumb = true;
                   config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
               });

            var mainMenu = new ConsoleMenu(args, level: 0)
             .Add("Lists", () => listMenu.Show())
             .Add("Non-CRUD Methods", () => nonCrudMenu.Show())
             .Add("CRUD Methods", () => Console.WriteLine("Not yet implemented"))
             .Add("Exit", () => Environment.Exit(0))
             .Configure(config =>
             {
                 config.Selector = "--> ";
                 config.EnableFilter = false;
                 config.Title = "Main menu";
                 config.EnableWriteTitle = false;
                 config.EnableBreadcrumb = true;
             });

            mainMenu.Show();
        }
    }
}
