using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;
using DM.Service.Contacts;

namespace DM.Service
{
    public class PrescriptionService : BaseService<Prescription>, IPrescriptionService
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

        List<Prescription> IPrescriptionService.GetPatientHistory(Guid patientId)
        {
            var prescrption = _prescriptionRepository.GetAll()
                .Where(x=> x.PatientId == patientId)
                .Include(x => x.Patient)                
                .Include(x => x.Payments)
                .Include(x => x.Status)
                .Include(x => x.PatientMedicalServices)               
                .Include($"{nameof(Prescription.PatientMedicalServices)}.{nameof(PatientMedicalService.MedicalService)}")               
                .ToList();

            return prescrption;
        }
    }
}
