﻿using Microsoft.AspNetCore.Http;
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
    public class CinemaController : ControllerBase
    {
        private ICinemaLogic logic;

        public CinemaController(ICinemaLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("test")]
        public string Test() {

            return "Cinema controller test";
        }

        [HttpGet]
        public IEnumerable<Cinema> GetAll() {

            return logic.ReadAll();
        }

        [HttpPost]
        public void AddOne([FromBody] Cinema cinema) {

            logic.Create(cinema);        
        }

        [HttpPut]
        public void EditOne([FromBody] Cinema cinema)
        {
            logic.Update(cinema);
        }

        [HttpDelete("{Id}")]
        public void DeleteOne([FromRoute] int Id)
        {
            logic.Delete(Id);
        }
    }
}
