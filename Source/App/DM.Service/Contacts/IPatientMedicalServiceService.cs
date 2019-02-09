using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;

namespace DM.Service.Contacts
{
    public interface IPatientMedicalServiceService: IBaseService<PatientMedicalService>
    {
        bool AddList(List<PatientMedicalService> patientMedicalServices);
        List<PatientMedicalService> GetByPrescriptionId(Guid id);
    }
}
