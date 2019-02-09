using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;
using DM.ViewModels;

namespace DM.Repository
{
    public class PatientCreateRepository: BaseRepository<Patient>, IPatientCreateRepository
    {
        private readonly DentalDbContext _db;

        public PatientCreateRepository(DentalDbContext db) : base(db)
        {
            _db = db;
        }


        public Patient GetPatientByCode(string codeToUpperCase)
        {
            return _db.Patients.FirstOrDefault(x => x.Code.ToUpper() == codeToUpperCase);
        }
    }
}
