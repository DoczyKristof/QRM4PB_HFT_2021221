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
        //public RestCollection<IEnumerable<Cinema>> CinemasThatHaveMovie { get; set; }
        //public RestCollection<IEnumerable<Room>> RoomsThatHaveMovie { get; set; }
        //public RestCollection<IEnumerable<KeyValuePair<MovieType, double>>> AveragePricesByTypes { get; set; }
        //public RestCollection<IEnumerable<KeyValuePair<MovieType, int>>> NumOfMoviesInTypes { get; set; }


        public NonCrudWindowViewModel()
        {
            if (!IsInDesignMode)
            {
                //CinemasThatHaveMovie = new RestCollection<IEnumerable<Cinema>>("http://localhost:20463/stat/", "CinemasThatHaveMovie");
                //RoomsThatHaveMovie = new RestCollection<IEnumerable<Room>>("http://localhost:20463/stat/", "RoomsThatHaveMovie");
                //AveragePricesByTypes = new RestCollection<IEnumerable<KeyValuePair<MovieType, double>>>("http://localhost:20463/stat/", "AveragePricesByTypes");
                //NumOfMoviesInTypes = new RestCollection<IEnumerable<KeyValuePair<MovieType, int>>>("http://localhost:20463/stat/", "NumOfMoviesInTypes");
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