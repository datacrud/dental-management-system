using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;

namespace DM.Service.Contacts
{
    public interface IMedicalInfoService : IBaseService<MedicalInfo>
    {
        List<MedicalInfo> GetPatientMedicalInfos(string patientId);
        void SavePatientMedicalInfos(List<PatientMedicalInfo> patientMedicalInfos);
    }
}
