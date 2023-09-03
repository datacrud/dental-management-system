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
    public class MedicalInfoService: BaseService<MedicalInfo>, IMedicalInfoService
    {
        private readonly IMedicalInfoRepository _medicalServiceRepository;
        private readonly IPatientMedicalInfoRepository _patientMedicalInfoRepository;

        public MedicalInfoService(IMedicalInfoRepository medicalServiceRepository, IPatientMedicalInfoRepository patientMedicalInfoRepository) : base(medicalServiceRepository)
        {
            _medicalServiceRepository = medicalServiceRepository;
            _patientMedicalInfoRepository = patientMedicalInfoRepository;
        }

        public List<MedicalInfo> GetPatientMedicalInfos(string patientId)
        {
            List<MedicalInfo> medicalInfos = _medicalServiceRepository.GetAll().ToList();
            
            Guid guid = Guid.Parse(patientId);
            List<PatientMedicalInfo> patientMedicalInfos = _patientMedicalInfoRepository.GetAll().Where(x => x.PatientId == guid).ToList();

            foreach (var item in medicalInfos)
            {
                item.IsChecked = patientMedicalInfos.FirstOrDefault(x => x.MedicalInfoId == item.Id) != null ? true: false;
            }
            
            return medicalInfos;
        }

        public void SavePatientMedicalInfos(List<PatientMedicalInfo> patientMedicalInfos)
        {
            var patientId = patientMedicalInfos.First().PatientId;
            var currentPatientMedicalInfos = _patientMedicalInfoRepository.GetAll().Where(x => x.PatientId == patientId).ToList();

            if(currentPatientMedicalInfos.Any()) {  
                foreach (var item in currentPatientMedicalInfos)
                {
                    _patientMedicalInfoRepository.Delete(item);
                }

                _patientMedicalInfoRepository.Commit();
            }

            foreach (var item in patientMedicalInfos)
            {
                _patientMedicalInfoRepository.Add(item);
            }

            _patientMedicalInfoRepository.Commit();    
        }
    }
}
