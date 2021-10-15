using QRM4PB_HFT_2021221.Data;
using System;
using System.Linq;

namespace QRM4PB_HFT_2021221.Client
{
    class Program
    {
        static void Main(string[] args)
        {
            CinemaDbContext cc = new CinemaDbContext();

            var res1 = cc.Cinemas.ToList();

            var res2 = cc.Cinemas.Find(3).Name;
            ;
        }
    }
}
