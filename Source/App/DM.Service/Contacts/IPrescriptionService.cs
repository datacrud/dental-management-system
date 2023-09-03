using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;

namespace DM.Service.Contacts
{
    public interface IPrescriptionService: IBaseService<Prescription>
    {
        List<Prescription> GetPatientCurrentPrescription(Guid patientId);
        List<Prescription> GetPatientHistory(Guid patientId);
    }
}
