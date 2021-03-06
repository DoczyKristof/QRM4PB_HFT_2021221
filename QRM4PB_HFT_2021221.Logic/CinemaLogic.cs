using QRM4PB_HFT_2021221.Models;
using QRM4PB_HFT_2021221.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Logic
{
    public class CinemaLogic : ICinemaLogic
    {

        ICinemaRepository repo;

        public CinemaLogic(ICinemaRepository repo)
        {
            this.repo = repo;
        }

        //CRUD
        public void Create(Cinema cinema)
        {
            if (cinema.Name != null)
            {
                repo.Create(cinema);
            }
            else
            {
                throw new Exception("Name is required");
            }

        }
        public void Delete(int id)
        {
            repo.Delete(id);
        }
        public IQueryable<Cinema> ReadAll()
        {
            return repo.ReadAll();
        }
        public void Update(Cinema cinema)
        {
            repo.Update(cinema);
        }
        
        //non crud
        public int avgCinemaSize()
        {
            return (int)Math.Round(
                repo
                .ReadAll()
                .Average(x => x.Rooms.Count())
                );
        }

        public Cinema Read(int id)
        {
            return repo.ReadOne(id);
        }
    }
}
