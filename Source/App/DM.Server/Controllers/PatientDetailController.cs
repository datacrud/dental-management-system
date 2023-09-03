using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DM.Models;
using DM.Service.Contacts;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/PatientDetails")]
    public class PatientDetailController : ApiController
    {
        private readonly IPatientMedicalServiceService _patientMedicalServiceService;
        private readonly IMedicalServiceService _medicalServiceService;

        public PatientDetailController(IPatientMedicalServiceService patientMedicalServiceService, IMedicalServiceService medicalServiceService)
        {
            _patientMedicalServiceService = patientMedicalServiceService;
            _medicalServiceService = medicalServiceService;
        }

        [HttpGet]
        [Route("GetPatientPrescriptionMedicalServices")]
        public IHttpActionResult GetPatientPrescriptionMedicalServices(string request)
        {
            List<PatientMedicalService> patientMedicalServices = _patientMedicalServiceService.GetByPrescriptionId(Guid.Parse(request));
            List<MedicalService> medicalServices = patientMedicalServices.Select(service => _medicalServiceService.GetById(service.MedicalServiceId)).ToList();

            medicalServices.ForEach(x =>
            {
                x.Quantity = patientMedicalServices.First(y => y.MedicalServiceId == x.Id).Quantity;
            });

            return Ok(medicalServices.OrderBy(x=> x.Name));
        }

    }
}
