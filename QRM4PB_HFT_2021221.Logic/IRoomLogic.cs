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
        //crud
        Room ReadOne(int id);
        void Create(Room room);
        IQueryable<Room> ReadAll();
        void Update(Room room);
        void Delete(int id);

        //noncrud
        public IEnumerable<Cinema> CinemasThatHaveMovie();
        public IEnumerable<Room> RoomsThatHaveMovie();

    }
}
