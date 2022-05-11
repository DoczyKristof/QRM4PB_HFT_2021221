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
    public class CinemaController : ControllerBase
    {
        private ICinemaLogic logic;
        IHubContext<SignalRHub> hub;

        public CinemaController(ICinemaLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet("test")]
        public string Test()
        {
            return "Cinema controller test";
        }

        [HttpGet]
        public IEnumerable<Cinema> GetAll()
        {
            return logic.ReadAll();
        }

        [HttpPost]
        public void AddOne([FromBody] Cinema cinema)
        {
            logic.Create(cinema);
            this.hub.Clients.All.SendAsync("CinemaCreated", cinema);
        }

        [HttpPut]
        public void EditOne([FromBody] Cinema cinema)
        {
            logic.Update(cinema);
            this.hub.Clients.All.SendAsync("CinemaUpdated", cinema);
        }

        [HttpDelete("{Id}")]
        public void DeleteOne([FromRoute] int Id)
        {
            var cinemaToDelete = this.logic.Read(Id);
            logic.Delete(Id);
            this.hub.Clients.All.SendAsync("CinemaDeleted", cinemaToDelete);
        }
    }
}
