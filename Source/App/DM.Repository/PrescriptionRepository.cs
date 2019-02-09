using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;

namespace DM.Repository
{
    public class PrescriptionRepository: BaseRepository<Prescription>, IPrescriptionRepository
    {
        protected DentalDbContext db = new DentalDbContext();

        public PrescriptionRepository(DentalDbContext db) : base(db)
        {
            
        }

        public IQueryable<Prescription> GetPatientCurrentPrescription(Guid patientId)
        {
            return db.Prescriptions.Where(x => x.PatientId == patientId).AsQueryable();
        }
    }
}
