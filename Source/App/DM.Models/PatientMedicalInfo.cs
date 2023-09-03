using System;

namespace DM.Models
{
    public class PatientMedicalInfo : BaseModel
    {
        public Guid PatientId { get; set; }
        public Guid MedicalInfoId { get; set; }
    }
}
