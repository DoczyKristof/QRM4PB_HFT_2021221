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
            Console.WriteLine("Note: the first query you choose could be slow.");
            Console.WriteLine("\n\nLoading...");
            for (int i = 0; i < 50; i++)
            {
                Console.Write("#");
                System.Threading.Thread.Sleep(160);
            }

            //System.Threading.Thread.Sleep(8000);

            RestService service = new RestService("http://localhost:20463");
           
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
                        var movieTheatre = service.Get<Cinema>("Cinema");
                        foreach (var room in rooms)
                        {
                            Console.WriteLine(
                                movieTheatre.
                                Where(x => x.Id == room.CinemaId).
                                Select(x => x.Name).FirstOrDefault() +
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
                        var movieTheatre = service.Get<Cinema>("Cinema");
                        foreach (var room in RoomsThatHaveMovie)
                        {
                            Console.WriteLine(
                                movieTheatre.
                                Where(x => x.Id == room.CinemaId).
                                Select(x => x.Name).FirstOrDefault() +
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

            //Create method
            void Create(string type)
            {
                switch (type)
                {
                    case "cinema":
                        Console.Clear();
                        Console.WriteLine("Creating a cinema:\n");
                        Console.WriteLine("The name of cinema: ");
                        string cinemaName = Console.ReadLine();
                        Cinema newCinema = new Cinema() { Name = cinemaName };
                        service.Post(newCinema, "Cinema");
                        break;
                    case "room":
                        Console.Clear();
                        Console.WriteLine("Creating a room:\n");
                        var cinemas = service.Get<Cinema>("Cinema");
                        foreach (var cinema in cinemas)
                        {
                            Console.WriteLine("{0} - {1}", cinema.Id, cinema.Name);
                        }
                        Console.WriteLine("\nWhich cinema is getting extended? (choose a number)");
                        int cinemaId = int.Parse(Console.ReadLine());
                        Console.WriteLine("\nThe room number: ");
                        int roomNumber = int.Parse(Console.ReadLine());
                        Room newRoom = new Room() { CinemaId = cinemaId, RoomNumber = roomNumber };
                        service.Post(newRoom, "Room");
                        break;
                    case "movie":
                        Console.Clear();
                        Console.WriteLine("Creating a Movie:\n");
                        //listing rooms
                        var rooms = service.Get<Room>("Room");
                        var movieTheatre = service.Get<Cinema>("Cinema");
                        foreach (var room in rooms)
                        {
                            Console.WriteLine(
                                movieTheatre.
                                Where(x => x.Id == room.CinemaId).
                                Select(x => x.Name).FirstOrDefault() +
                                ": Roomnumber: " + room.RoomNumber + "| Id: " + room.Id + " |");
                        }

                        Console.WriteLine("\nWhich room plays the movie? (choose an id)");
                        int roomId = int.Parse(Console.ReadLine());
                        Console.WriteLine("Title: ");
                        string title = Console.ReadLine();
                        Console.WriteLine("Price: ");
                        int price = int.Parse(Console.ReadLine());
                        Console.WriteLine("Lenght: (format example: 2:18) ");
                        string rawLength = Console.ReadLine();
                        string length = String.Format("{0} hour(s) {1} minute(s)", rawLength.Split(':')[0], rawLength.Split(':')[1]);
                        int index = 0;
                        foreach (MovieType movietype in (MovieType[])Enum.GetValues(typeof(MovieType)))
                        {
                            Console.WriteLine("{0} - {1}", index++, movietype);
                        }
                        Console.WriteLine("What type is it? (choose an id)");
                        int mtype = int.Parse(Console.ReadLine());
                        MovieType movieType = (MovieType)mtype;
                        Movie newMovie = new Movie() { RoomId = roomId,
                            Title = title, Price = price, Length = length,
                            Type = movieType};
                        var chosenRoom = rooms.Where(x => x.Id == roomId).FirstOrDefault();
                        chosenRoom.Movies.Add(newMovie);
                        service.Post(newMovie , "Movie");
                        break;
                    default:
                        Console.WriteLine("Error!");
                        break;
                }
            }
            
            //delete menu
            void Delete(string type)
            {
                switch (type)
                {
                    case "cinema":
                        Console.Clear();
                        Console.WriteLine("Deleting a cinema:\n");
                        var cinemas = service.Get<Cinema>("Cinema");
                        foreach (var cinema in cinemas)
                        {
                            Console.WriteLine("{0} - {1}", cinema.Id, cinema.Name);
                        }
                        Console.WriteLine("\nWhich one to delete? (choose a number)");
                        int deleteCinema = int.Parse(Console.ReadLine());
                        service.Delete(deleteCinema, "Cinema");
                        break;
                    case "room":
                        Console.Clear();
                        Console.WriteLine("Deleting a room:\n");
                        var rooms = service.Get<Room>("Room");
                        var movieTheatre = service.Get<Cinema>("Cinema");
                        foreach (var room in rooms)
                        {
                            Console.WriteLine(
                                movieTheatre.
                                Where(x => x.Id == room.CinemaId).
                                Select(x => x.Name).FirstOrDefault() +
                                ": Roomnumber: " + room.RoomNumber + "| Id: " + room.Id + " |");
                        }
                        Console.WriteLine("\nWhich one to delete? (choose an id)");
                        int deleteRoom = int.Parse(Console.ReadLine());
                        service.Delete(deleteRoom, "Room");
                        break;
                    case "movie":
                        Console.Clear();
                        Console.WriteLine("Deleting a movie:\n");
                        var movies = service.Get<Movie>("Movie");
                        foreach (var movie in movies)
                        {
                            Console.WriteLine("{0} - {1}", movie.Id, movie.Title);
                        }
                        Console.WriteLine("\nWhich one to delete? (choose an id)");
                        int deleteMovie = int.Parse(Console.ReadLine());
                        service.Delete(deleteMovie, "Movie");
                        break;
                    default:
                        Console.WriteLine("Error!");
                        break;
                }
            }


            //the menu
            //list menu
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

            //non-crud menu
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

            //sub crud menus
            //delete menu
            var DeleteMenu = new ConsoleMenu(args, level: 2)
                .Add("Cinema", () => Delete("cinema"))
                .Add("Room", () => Delete("room"))
                .Add("Movie", () => Delete("movie"))
                .Add("Back", ConsoleMenu.Close)
                .Configure(config =>
                {
                    config.Selector = "--> ";
                    config.EnableFilter = false;
                    config.Title = "What to delete?";
                    config.EnableBreadcrumb = true;
                    config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
                });
            //create menu
            var CreateMenu = new ConsoleMenu(args, level: 2)
               .Add("Cinema", () => Create("cinema"))
               .Add("Room", () => Create("room"))
               .Add("Movie", () => Create("movie"))
               .Add("Back", ConsoleMenu.Close)
               .Configure(config =>
               {
                   config.Selector = "--> ";
                   config.EnableFilter = false;
                   config.Title = "What to create?";
                   config.EnableBreadcrumb = true;
                   config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
               });

            //crud menu
            var CrudMenu = new ConsoleMenu(args, level: 1)
               .Add("Create", () => CreateMenu.Show())
               .Add("Read", () => listMenu.Show())
               .Add("Update", () => NonCrud(3))
               .Add("Delete", () => DeleteMenu.Show())
               .Add("Back", ConsoleMenu.Close)
               .Configure(config =>
               {
                   config.Selector = "--> ";
                   config.EnableFilter = false;
                   config.Title = "What would you like to do?" ;
                   config.EnableBreadcrumb = true;
                   config.WriteBreadcrumbAction = titles => Console.WriteLine(string.Join(" / ", titles));
               });

            //main menu
            var mainMenu = new ConsoleMenu(args, level: 0)
             .Add("Lists", () => listMenu.Show())
             .Add("Non-CRUD Methods", () => nonCrudMenu.Show())
             .Add("CRUD Methods", () => CrudMenu.Show())
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
