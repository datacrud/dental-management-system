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
    public class DoctorService : BaseService<Doctor>, IDoctorService
    {
        private readonly IDoctorRepository _doctorRepository;

        public DoctorService(IDoctorRepository doctorRepository) : base(doctorRepository)
        {
            _doctorRepository = doctorRepository;
        }

    }
}
