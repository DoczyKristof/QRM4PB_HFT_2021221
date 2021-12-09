using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Logic
{
    public interface IRoomLogic
    {
        void Create(Room room);
        IQueryable<Room> ReadAll();
        void Update(Room room);
        void Delete(int id);

        //non crud
        public IEnumerable<Room> LeastIncome();
        public IEnumerable<Cinema> CinemasThatHaveMovie();
        public IEnumerable<Room> RoomsThatHaveMovie();

    }
}
