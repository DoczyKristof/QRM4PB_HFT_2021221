using Moq;
using NUnit.Framework;
using QRM4PB_HFT_2021221.Logic;
using QRM4PB_HFT_2021221.Models;
using QRM4PB_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Test
{
    [TestFixture]
    class MovieLogicTests
    {
        IMovieLogic logic;

        [SetUp]
        public void SetUp()
        {
            Mock<IMovieRepository> mock = new Mock<IMovieRepository>();

            Room room1 = new Room() { RoomNumber = 107 };
            Room room2 = new Room() { RoomNumber = 100 };

            mock
                .Setup(x => x.ReadAll())
                .Returns(new List<Movie>
                {
                    new Movie(){ 
                        Type = MovieType.Comedy,
                        Price = 1500,
                        Room = room1
                    },
                    new Movie(){
                        Type = MovieType.Action,
                        Price = 3500,
                        Room = room1
                    }
                }.AsQueryable());
            logic = new MovieLogic(mock.Object);
        }

        [Test]
        public void CheckAvgPrice()
        {
            double avg = logic.AverageMoviePrice();
            Assert.That(avg, Is.EqualTo(2500));
        }

        [Test]
        public void CheckCreate()
        {
        }

        [Test]
        public void CheckDelete()
        {

        }
    }
}
