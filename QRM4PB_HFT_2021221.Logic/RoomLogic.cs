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
            if (room.RoomNumber > 0)
            {
                repo.Create(room);
            }
            else
            {
                throw new Exception("Room number is required");
            }
            
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

        //NON CRUD
        public IEnumerable<Cinema> CinemasThatHaveMovie()
        {
            return repo
                .ReadAll()
                .Where(x => x.Movies.Count() > 0)
                .Select(x => x.Cinema)
                .Distinct();
        }
        public IEnumerable<Room> RoomsThatHaveMovie()
        {
            return repo
                .ReadAll()
                .Where(x => x.Movies.Count() > 0)
                .Select(x => x)
                .Distinct();
        }

        public IEnumerable<Room> LeastIncome()
        {
            return repo
                 .ReadAll()
                 .Select(x => x)
                 .Where(x => x.Movie.Price == x.Movies.Min(c => c.Price ?? 0));
        }
    }
}
