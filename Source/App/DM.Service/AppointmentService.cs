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
    public class AppointmentService : BaseService<Appointment>, IAppointmentService
    {
        private readonly IAppointmentRepository _appointmentRepository;

        public AppointmentService(IAppointmentRepository appointmentRepository) : base(appointmentRepository)
        {
            _appointmentRepository = appointmentRepository;
        }

        public List<Appointment> GetByDate(DateTime date)
        {
            return  _appointmentRepository.GetByDate(date.Date).ToList();
        }

        public List<Appointment> Search(string request)
        {
            return _appointmentRepository.GetAll()
                .Where(x => x.Code.ToUpper().Contains(request) || x.PatientNameOrId.ToUpper().Contains(request))
                .ToList();
        }
    }
}
