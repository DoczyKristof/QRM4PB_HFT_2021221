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
    class CinemaLogicTest
    {
        ICinemaLogic logic;

        [SetUp]
        public void SetUp()
        {
            Mock<ICinemaRepository> mock = new Mock<ICinemaRepository>();

            mock
                .Setup(x => x.ReadAll())
                .Returns(new List<Cinema>
                {
                    new Cinema()
                    {
                        Name = "Cinema"
                    },
                    new Cinema()
                    {
                        Name = "AnotherCinema"
                    }
                }.AsQueryable());

            logic = new CinemaLogic(mock.Object);
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
            Cinema cinema = new Cinema()
            {
                
            };
            Assert.Throws(typeof(Exception), () => logic.Create(cinema));
        }
    }
}
