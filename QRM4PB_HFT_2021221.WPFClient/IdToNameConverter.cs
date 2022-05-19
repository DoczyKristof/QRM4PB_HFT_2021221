using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace QRM4PB_HFT_2021221.WPFClient
{
    class IdToNameConverter : IValueConverter
    {
        //public RestCollection<Cinema> Cinemas;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
            //Cinemas = new RestCollection<Cinema>("http://localhost:20463/", "cinema", "hub");
            //int id = (int)value;
            //return Cinemas.FirstOrDefault(x => x.Id == id).Name;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
