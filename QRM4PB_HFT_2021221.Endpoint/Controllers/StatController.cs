using Microsoft.AspNetCore.Mvc;
using QRM4PB_HFT_2021221.Logic;
using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QRM4PB_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class StatController : ControllerBase
    {
        IRoomLogic roomLogic;
        IMovieLogic movieLogic;
        ICinemaLogic cinemaLogic;
        public StatController(
            IRoomLogic roomLogic, 
            IMovieLogic movieLogic,
            ICinemaLogic cinemaLogic)
        {
            this.roomLogic = roomLogic;
            this.movieLogic = movieLogic;
            this.cinemaLogic = cinemaLogic;
        }

        //roomlogic
        [HttpGet]
        public IEnumerable<Room> RoomWithLeastIncome()
        {
            return roomLogic.LeastIncome();
        }

        [HttpGet]
        public IEnumerable<Cinema> CinemasThatHaveMovie()
        {
            return roomLogic.CinemasThatHaveMovie();
        }

        [HttpGet]
        public IEnumerable<Room> RoomsThatHaveMovie()
        {
            return roomLogic.RoomsThatHaveMovie();
        }

        //movielogic
        [HttpGet]
        public double AverageMoviePrice()
        {
            return movieLogic.AverageMoviePrice();
        }
        
        [HttpGet]
        public IEnumerable<KeyValuePair<MovieType, double>>
            AveragePricesByTypes()
        {
            return movieLogic.AveragePricesByTypes();
        }

        [HttpGet]
        public IEnumerable<KeyValuePair<MovieType, int>>
            NumOfMoviesInTypes()
        {
            return movieLogic.NumOfMoviesInTypes();
        }

        //cinemalogic
        public int AvgCinemaSize()
        {
            return cinemaLogic.avgCinemaSize();
        }
    }
}
