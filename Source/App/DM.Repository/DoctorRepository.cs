using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;

namespace DM.Repository
{
    public class DoctorRepository: BaseRepository<Doctor>, IDoctorRepository
    {
        private readonly DentalDbContext _db;

        public DoctorRepository(DentalDbContext db) : base(db)
        {
            _db = db;
        }
    }
}
