using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Logic
{
    public interface ICinemaLogic
    {
        //crud
        void Create(Cinema cinema);
        IQueryable<Cinema> ReadAll();
        void Update(Cinema Cinema);
        void Delete(int id);
        //noncrud
        public double avgCinemaSize();
    }
}
