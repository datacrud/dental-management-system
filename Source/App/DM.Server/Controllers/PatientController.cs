using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DM.Models;
using DM.RequestModels;
using DM.Service.Contacts;
using DM.ViewModels;
using Newtonsoft.Json;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/Patients")]
    public class PatientController : ApiController
    {
        private readonly IPatientCreateService _patientCreateService;
        private readonly IPrescriptionService _prescriptionService;

        public PatientController(IPatientCreateService patientCreateService, IPrescriptionService prescriptionService)
        {
            _patientCreateService = patientCreateService;
            _prescriptionService = prescriptionService;
        }


        [HttpGet]
        [Route("GetGridList")]
        public IHttpActionResult Get()
        {
            List<PatientGridViewModel> gridViewModels = new List<PatientGridViewModel>();

            List<Patient> patients = _patientCreateService.GetAll();
            foreach (Patient patient in patients)
            {
                Prescription prescription = _prescriptionService.GetPatientCurrentPrescription(patient.Id).Last();

                PatientGridViewModel gridViewModel = new PatientGridViewModel()
                {
                    Id = patient.Id,
                    Code = patient.Code,
                    Name = patient.Name,
                    Phone = patient.Phone,
                    Age = patient.Age,
                    LastVisitingDate = prescription.LastUpdate,
                    TotalPayable = prescription.TotalPayable,
                    TotalPaid = prescription.TotalPaid,
                    TotalDue = prescription.TotalDue
                };

                gridViewModels.Add(gridViewModel);
            }

            return Ok(gridViewModels.OrderByDescending(x=> x.LastVisitingDate).Take(100));
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search(string request)
        {
            PatientSearchRequestModel model = JsonConvert.DeserializeObject<PatientSearchRequestModel>(request);

            List<PatientGridViewModel> gridViewModels = new List<PatientGridViewModel>();

            List<Patient> patients = _patientCreateService.GetAll();

            foreach (Patient patient in patients)
            {
                Prescription prescription = _prescriptionService.GetPatientCurrentPrescription(patient.Id).Last();

                PatientGridViewModel gridViewModel = new PatientGridViewModel()
                {
                    Id = patient.Id,
                    Code = patient.Code,
                    Name = patient.Name,
                    Age = patient.Age,
                    LastVisitingDate = prescription.LastUpdate,
                    TotalPayable = prescription.TotalPayable,
                    TotalPaid = prescription.TotalPaid,
                    TotalDue = prescription.TotalDue
                };

                gridViewModels.Add(gridViewModel);
            }

            var key = model.SearchKey.ToUpper();

            IEnumerable<PatientGridViewModel> enumerable = null;
            if (model.FilterId == 0)
            {
                enumerable = gridViewModels.Where(x => x.Code.ToUpper().Contains(key) || x.Name.ToUpper().Contains(key) || x.Phone.Contains(key));
            }

            else if(model.FilterId == 1)
            {
                enumerable = gridViewModels.Where(x => (x.Code.ToUpper().Contains(key) || x.Name.ToUpper().Contains(key) || x.Phone.Contains(key)) && Math.Abs(x.TotalDue) > 0 );
            }

            else if (model.FilterId == 2)
            {
                enumerable = gridViewModels.Where(x => (x.Code.ToUpper().Contains(key) || x.Name.ToUpper().Contains(key) || x.Phone.Contains(key)) && Math.Abs(x.TotalDue) <= 0);
            }

            return Ok(enumerable);
        }

    }
}
