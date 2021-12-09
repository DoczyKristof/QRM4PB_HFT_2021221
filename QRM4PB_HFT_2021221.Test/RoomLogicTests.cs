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
    class RoomLogicTests
    {
        IRoomLogic logic;

        [SetUp]
        public void SetUp()
        {
            Mock<IRoomRepository> mock = new Mock<IRoomRepository>();

            Cinema ccArena = new Cinema() { Id = 1, Name = "Cinema City Aréna"};
            
            Movie movie = new Movie() {
                Id = 1,
                Title = "Marvel's Eternals",
                Price = 2500,
                Type = MovieType.Action,
                Length = new TimeSpan(2, 37, 0),
                RoomId = 1
            };

            mock
                .Setup(x => x.ReadAll())
                .Returns(new List<Room>
                {
                    new Room()
                    {
                        Id = 1, CinemaId = ccArena.Id, RoomNumber = 1, Movies = { movie }
                    },
                    new Room()
                    {
                        Id = 2, CinemaId = ccArena.Id, RoomNumber = 2
                    }
                }.AsQueryable()) ;
            logic = new RoomLogic(mock.Object);
        }

        [Test]
        public void CheckReadAll()
        {
            int count = logic.ReadAll().Count();
            Assert.That(count == 2);
        }
        
        [Test]
        public void CheckCreate()
        {
            Room room = new Room()
            {
                
            };
            Assert.Throws(typeof(Exception), () => logic.Create(room));
        }


        [Test]
        public void CheckRoomsThatHaveMovie()
        {
            List<Room> list = logic.RoomsThatHaveMovie().ToList();
            Assert.That(list[0].RoomNumber.Equals(1));
        }
    }
}
