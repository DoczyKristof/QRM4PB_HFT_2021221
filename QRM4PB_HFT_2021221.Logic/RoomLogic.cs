using QRM4PB_HFT_2021221.Repository;
using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Logic
{
    public class RoomLogic : IRoomLogic
    {
        IRoomRepository repo;

        public RoomLogic(IRoomRepository repo)
        {
            this.repo = repo;
        }

        //CRUD
        public void Create(Room room)
        {
            repo.Create(room);
        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public IQueryable<Room> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Room room)
        {
            repo.Update(room);
        }
    }
}
