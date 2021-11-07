using QRM4PB_HFT_2021221.Data;
using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Repository
{
    class CinemaRepository
    {

        CinemaDbContext context;

        public CinemaRepository(CinemaDbContext context)
        {
            this.context = context;
        }

        public void Create(Cinema cinema)
        {
            context.Cinemas.Add(cinema);
            context.SaveChanges();
        }


        public Cinema ReadOne(int id)
        {
            return context
                .Cinemas
                .FirstOrDefault(c => c.Id == id);
        }

        public IQueryable<Cinema> ReadAll()
        {
            return context.Cinemas;
        }

        public void Update(Cinema cinema)
        {
            Cinema old = ReadOne(cinema.Id);

            old.Name = cinema.Name;
            old.Rooms = cinema.Rooms;

            context.SaveChanges();
        }

        public void Delete(int id)
        {
            context.Cinemas.Remove(ReadOne(id));
            context.SaveChanges();
        }
    }
}
