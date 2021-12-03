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
            RestService service = new RestService("http://localhost:20463");
            var result1 = service.Get<Cinema>("/Cinema");
        }
    }
}
