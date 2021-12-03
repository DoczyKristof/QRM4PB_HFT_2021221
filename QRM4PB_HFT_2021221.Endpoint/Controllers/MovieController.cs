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
    public class MovieController : ControllerBase
    {
        private IMovieLogic logic;

        public MovieController(IMovieLogic logic)
        {
            this.logic = logic;
        }

        [HttpGet("test")]
        public string Test() {

            return "Movie controller test";
        }

        [HttpGet]
        public IEnumerable<Movie> GetAll() {

            return logic.ReadAll();
        }

        [HttpPost]
        public void AddOne([FromBody] Movie movie) {

            logic.Create(movie);        
        }

        [HttpPut]
        public void EditOne([FromBody] Movie movie)
        {
            logic.Update(movie);
        }

        [HttpDelete("{Id}")]
        public void DeleteOne([FromRoute] int Id)
        {
            logic.Delete(Id);
        }
    }
}
