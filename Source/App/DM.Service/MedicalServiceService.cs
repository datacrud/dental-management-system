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
    public class MedicalServiceService: BaseService<MedicalService>, IMedicalServiceService
    {
        private readonly IMedicalServiceRepository _medicalServiceRepository;

        public MedicalServiceService(IMedicalServiceRepository medicalServiceRepository): base(medicalServiceRepository)
        {
            _medicalServiceRepository = medicalServiceRepository;
        }


    }
}
