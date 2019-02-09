using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository;
using DM.Repository.Contacts;
using DM.Service.Contacts;
using DM.ViewModels;

namespace DM.Service
{
    public class PatientMedicalServiceService : BaseService<PatientMedicalService>, IPatientMedicalServiceService
    {
        private readonly IPatientMedicalServiceRepository _patientMedicalServiceRepository;

        public PatientMedicalServiceService(IPatientMedicalServiceRepository repository) : base(repository)
        {
            _patientMedicalServiceRepository = repository;
        }


        public bool AddList(List<PatientMedicalService> patientMedicalServices)
        {
            bool response = true;
            try
            {
                IEnumerable<PatientMedicalService> addList = _patientMedicalServiceRepository.AddList(patientMedicalServices);
            }
            catch (Exception exception)
            {
                response = false;
            }
            return response;
        }

        public List<PatientMedicalService> GetByPrescriptionId(Guid id)
        {
            return  _patientMedicalServiceRepository.GetAll().Where(x => x.PrescriptionId == id).ToList();
        }
    }
}
