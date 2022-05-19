using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace QRM4PB_HFT_2021221.WPFClient
{
    class NonCrudWindowViewModel
    {
        public List<Cinema> CinemasThatHaveMovie { get; }
        public List<Room> RoomsThatHaveMovie { get; }
        public List<KeyValuePair<MovieType, double>> AveragePricesByTypes { get; }
        public List<KeyValuePair<MovieType, int>> NumOfMoviesInTypes { get; }
        public int AvgMoviePrice { get; }


        public NonCrudWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                AvgMoviePrice = new RestService("http://localhost:20463/stat/", "AverageMoviePrice").GetSingle<int>("AverageMoviePrice");
                CinemasThatHaveMovie = new RestService("http://localhost:20463/stat/", "CinemasThatHaveMovie").Get<Cinema>("CinemasThatHaveMovie");
                RoomsThatHaveMovie = new RestService("http://localhost:20463/stat/", "RoomsThatHaveMovie").Get<Room>("RoomsThatHaveMovie");
                AveragePricesByTypes = new RestService("http://localhost:20463/stat/", "RoomsThatHaveMovie").Get<KeyValuePair<MovieType, double>>("AveragePricesByTypes");
                NumOfMoviesInTypes = new RestService("http://localhost:20463/stat/", "NumOfMoviesInTypes").Get<KeyValuePair<MovieType, int>>("NumOfMoviesInTypes");
            }
        }


        public static bool IsInDesignMode
        {
            get
            {
                var prop = DesignerProperties.IsInDesignModeProperty;
                return (bool)DependencyPropertyDescriptor.FromProperty(prop, typeof(FrameworkElement)).Metadata.DefaultValue;
            }
        }
    }
}