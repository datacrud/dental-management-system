using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;

namespace DM.Repository.Contacts
{
    public interface IPrescriptionRepository: IBaseRepository<Prescription>
    {
        IQueryable<Prescription> GetPatientCurrentPrescription(Guid patientId);
    }
}
