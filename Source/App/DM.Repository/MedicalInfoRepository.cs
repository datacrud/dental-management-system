using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;


namespace DM.Repository
{
    public class MedicalInfoRepository : BaseRepository<MedicalInfo>, IMedicalInfoRepository
    {
        protected DentalDbContext db = new DentalDbContext();

        public MedicalInfoRepository(DentalDbContext db) : base(db)
        {

        }

        
    }
}
