using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DM.Models;
using DM.RequestModels;
using DM.Service.Contacts;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/PatientCreate")]
    public class PatientCreateController : ApiController
    {
        private readonly IPatientCreateService _patientCreateService;
        private readonly IPrescriptionService _prescriptionService;

        public PatientCreateController(IPatientCreateService patientCreateService, IPrescriptionService prescriptionService)
        {
            _patientCreateService = patientCreateService;
            _prescriptionService = prescriptionService;
        }


        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult Get()
        {
            List<Patient> patients = new List<Patient>(_patientCreateService.GetAll());
            return Ok(patients);
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(string request)
        {
            Patient patient = _patientCreateService.GetById(Guid.Parse(request));
            return Ok(patient);
        }

        [HttpGet]
        [Route("GetPatientByCode")]
        public IHttpActionResult GetPatientByCode(string request)
        {
            return Ok(_patientCreateService.GetPatientByCode(request));
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Post(Patient patient)
        {
            patient.Code = HelperRequestModel.GetThisPatientCode((_patientCreateService.GetPatientViewModel().Count() + 1).ToString());

            if (!ModelState.IsValid)
                return BadRequest();

            bool add = _patientCreateService.Add(patient);

            if (add)
            {
                if (_prescriptionService.GetAll().FirstOrDefault(x=> x.PatientId == patient.Id) == null)
                {                    
                    string prescriptionCode = HelperRequestModel.GenerateBillCode(patient.Code, (0 + 1).ToString());

                    Prescription prescription = new Prescription()
                    {
                        Code = prescriptionCode,
                        PatientId = patient.Id,
                        StatusId = 5,
                        Created = DateTime.Now.ToLocalTime(),
                        LastUpdate = DateTime.Now.ToLocalTime()
                    };

                    _prescriptionService.Add(prescription);
                }
            }

            return Ok(patient.Id);
        }        

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Put(Patient patient)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            return Ok(_patientCreateService.Edit(patient));
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(string request)
        {
            return Ok(_patientCreateService.Delete(Guid.Parse(request)));
        }

        

    }
}
