using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Repository
{
    public interface IRoomRepository
    {
        void Create(Room room);
        Room ReadOne(int id); 
        IQueryable<Room> ReadAll();
        void Update(Room room);
        void Delete(int id);
    }
}
