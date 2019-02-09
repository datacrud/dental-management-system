using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;
using DM.Service.Contacts;
using DM.ViewModels;

namespace DM.Service
{
    public class PatientCreateService: BaseService<Patient>, IPatientCreateService
    {
        private readonly IPatientCreateRepository _patientCreateRepository;

        public PatientCreateService(IPatientCreateRepository patientCreateRepository) : base(patientCreateRepository)
        {
            _patientCreateRepository = patientCreateRepository;
        }

        public List<PatientViewModel> GetPatientViewModel()
        {
            List<PatientViewModel> patientViewModels = _patientCreateRepository.GetAll().Select(x => new PatientViewModel()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                Age = x.Age
            }).OrderBy(x=> x.Code).ToList();

            return patientViewModels;
        }

        public Patient GetPatientByCode(string code)
        {
            return _patientCreateRepository.GetPatientByCode(code.ToUpper());
        }
    }
}
