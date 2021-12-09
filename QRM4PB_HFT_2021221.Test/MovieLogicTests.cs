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
                        RoomId = room1.Id
                    },
                    new Movie(){
                        Type = MovieType.Comedy,
                        Price = 2500,
                        RoomId = room1.Id
                    },
                    new Movie(){
                        Type = MovieType.Action,
                        Price = 3500,
                        RoomId = room1.Id
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
            Movie movie = new Movie()
            {
                Id = 3,
                Type = MovieType.Fantasy,
                Price = 620,
                RoomId = 2
            };       
            Assert.Throws(typeof(Exception), () => logic.Create(movie));
        }

        [Test]
        public void CheckReadAll()
        {
            int count = logic.ReadAll().Count();
            Assert.That(count == 3);
        }

        [Test]
        public void CheckAveragePricesByTypes()
        {
            var result = logic.AveragePricesByTypes().ToList();
            Assert.That(result[0].Key.Equals(MovieType.Comedy) && result[0].Value.Equals(2000));
        }
    }
}
