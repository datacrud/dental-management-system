using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;


namespace DM.Repository
{
    public class PatientMedicalServiceRepository : BaseRepository<PatientMedicalService>, IPatientMedicalServiceRepository
    {
        protected DentalDbContext db = new DentalDbContext();

        public PatientMedicalServiceRepository(DentalDbContext db) : base(db)
        {

        }


        public IEnumerable<PatientMedicalService> AddList(List<PatientMedicalService> patientMedicalServices)
        {
            foreach (PatientMedicalService service in patientMedicalServices)
            {
                var list = db.PatientMedicalServices.Where(x => x.PrescriptionId == service.PrescriptionId).ToList();

                db.PatientMedicalServices.RemoveRange(list);
                break;
            }
            db.PatientMedicalServices.AddRange(patientMedicalServices);
                  
            db.SaveChanges();       
            return patientMedicalServices;
        }
    }
}
