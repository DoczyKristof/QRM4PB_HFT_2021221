using QRM4PB_HFT_2021221.Data;
using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Repository
{
    public class RoomRepository
    {
        CinemaDbContext context;

        public RoomRepository(CinemaDbContext context)
        {
            this.context = context;
        }

        public void Create(Room room)
        {
            context.Rooms.Add(room);
            context.SaveChanges();
        }


        public Room ReadOne(int id)
        {
            return context
                .Rooms
                .FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<Room> ReadAll()
        {
            return context.Rooms;
        }

        public void Update(Room room)
        {
            Room old = ReadOne(room.Id);

            old.RoomNumber = room.RoomNumber;
            old.CinemaId = room.CinemaId;
            old.Movie = room.Movie;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Rooms.Remove(ReadOne(id));
            context.SaveChanges();
        }
    }
}
