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
    public class RoomController : ControllerBase
    {
        private IRoomLogic logic;
        IHubContext<SignalRHub> hub;

        public RoomController(IRoomLogic logic, IHubContext<SignalRHub> hub)
        {
            this.logic = logic;
            this.hub = hub;
        }

        [HttpGet("test")]
        public string Test()
        {
            return "Room controller test";
        }

        [HttpGet]
        public IEnumerable<Room> GetAll()
        {
            return logic.ReadAll();
        }

        [HttpPost]
        public void AddOne([FromBody] Room room)
        {
            logic.Create(room);
            this.hub.Clients.All.SendAsync("RoomCreated", room);
        }

        [HttpPut]
        public void EditOne([FromBody] Room room)
        {
            logic.Update(room);
            this.hub.Clients.All.SendAsync("RoomUpdated", room);
        }

        [HttpDelete("{Id}")]
        public void DeleteOne([FromRoute] int Id)
        {
            var roomToDelete = this.logic.ReadOne(Id);
            logic.Delete(Id);
            this.hub.Clients.All.SendAsync("RoomUpdated", roomToDelete);
        }
    }
}
