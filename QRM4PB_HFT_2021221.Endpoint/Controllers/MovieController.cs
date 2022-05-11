using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using QRM4PB_HFT_2021221.Endpoint.Services;
using QRM4PB_HFT_2021221.Logic;
using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace QRM4PB_HFT_2021221.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        private IMovieLogic logic;
        IHubContext<SignalRHub> hub;

        public MovieController(IMovieLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet("test")]
        public string Test()
        {
            return "Movie controller test";
        }

        [HttpGet]
        public IEnumerable<Movie> GetAll()
        {
            return logic.ReadAll();
        }

        [HttpPost]
        public void AddOne([FromBody] Movie movie)
        {
            logic.Create(movie);
            this.hub.Clients.All.SendAsync("MovieCreated", movie);
        }

        [HttpPut]
        public void EditOne([FromBody] Movie movie)
        {
            logic.Update(movie);
            this.hub.Clients.All.SendAsync("MovieUpdated", movie);
        }

        [HttpDelete("{Id}")]
        public void DeleteOne([FromRoute] int Id)
        {
            var movieToDelete = this.logic.ReadOne(Id);
            logic.Delete(Id);
            this.hub.Clients.All.SendAsync("MovieDeleted", movieToDelete);
        }
    }
}
