using QRM4PB_HFT_2021221.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QRM4PB_HFT_2021221.Repository
{
    interface ICinemaRepository
    {
        void Create(Cinema cinema);
        Cinema ReadOne(int id);
        IQueryable<Cinema> ReadAll();
        void Update(Cinema cinema);
        void Delete(int id);
    }
}
