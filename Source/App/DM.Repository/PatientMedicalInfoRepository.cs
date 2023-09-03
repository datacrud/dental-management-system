using DM.Models;
using DM.Repository.Contacts;


namespace DM.Repository
{
    public class PatientMedicalInfoRepository : BaseRepository<PatientMedicalInfo>, IPatientMedicalInfoRepository
    {
        protected DentalDbContext db = new DentalDbContext();

        public PatientMedicalInfoRepository(DentalDbContext db) : base(db)
        {

        }

        
    }
}
