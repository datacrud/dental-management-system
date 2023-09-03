using System;
using System.Collections.Generic;
using System.Web.Http;
using DM.Models;
using DM.Service.Contacts;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/PatientMedicalServices")]
    public class PatientMedicalServiceController : ApiController
    {
        private readonly IPatientMedicalServiceService _patientMedicalServiceService;

        public PatientMedicalServiceController(IPatientMedicalServiceService patientMedicalServiceService)
        {
            _patientMedicalServiceService = patientMedicalServiceService;
        }


        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult Get()
        {
            return Ok(_patientMedicalServiceService.GetAll());
        }

        [HttpGet]
        [Route("GetByPrescriptionId")]
        public IHttpActionResult GetByPrescriptionId(string request)
        {
            var response = _patientMedicalServiceService.GetByPrescriptionId(Guid.Parse(request));
            return Ok(response);
        }

        [HttpPost]
        [Route("CreateList")]
        public IHttpActionResult Post(List<PatientMedicalService> patientMedicalServices)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_patientMedicalServiceService.AddList(patientMedicalServices));
        }
    }
}
