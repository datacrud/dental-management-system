using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;
using DM.Service.Contacts;

namespace DM.Service
{
    public class PrescriptionService:BaseService<Prescription>, IPrescriptionService
    {
        private readonly IPrescriptionRepository _prescriptionRepository;

        public PrescriptionService(IPrescriptionRepository prescriptionRepository) : base(prescriptionRepository)
        {
            _prescriptionRepository = prescriptionRepository;
        }

        public List<Prescription> GetPatientCurrentPrescription(Guid patientId)
        {
            return _prescriptionRepository.GetPatientCurrentPrescription(patientId).OrderBy(x=>x.Code).ToList();
        }
    }
}
