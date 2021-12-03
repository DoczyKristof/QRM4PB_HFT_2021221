using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QRM4PB_HFT_2021221.Logic;
using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRM4PB_HFT_202122.Endpoint.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class RoomController : ControllerBase
    {
        private IRoomLogic logic;

        public RoomController(IRoomLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("test")]
        public string Test() {

            return "Room controller test";
        }

        [HttpGet]
        public IEnumerable<Room> GetAll() {

            return logic.ReadAll();
        }

        [HttpPost]
        public void AddOne([FromBody] Room room) {

            logic.Create(room);        
        }

        [HttpPut]
        public void EditOne([FromBody] Room room)
        {
            logic.Update(room);
        }

        [HttpDelete("{Id}")]
        public void DeleteOne([FromRoute] int Id)
        {
            logic.Delete(Id);
        }
    }
}
