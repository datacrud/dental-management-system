using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DM.Models;
using DM.Service;
using DM.Service.Contacts;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/MedicalInfo")]
    public class MedicalInfoController : ApiController
    {
        private readonly IMedicalInfoService _medicalnfoService;

        public MedicalInfoController(IMedicalInfoService medicalnfoService)
        {
            _medicalnfoService = medicalnfoService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult Get()
        {
            List<MedicalInfo> services = new List<MedicalInfo>(_medicalnfoService.GetAll().OrderByDescending(x=> x.LastUpdate));
            return Ok(services);
        }

        [HttpGet]
        [Route("GetAllOrderByName")]
        public IHttpActionResult GetAllOrderByName()
        {
            List<MedicalInfo> services = new List<MedicalInfo>(_medicalnfoService.GetAll().OrderBy(x=> x.Name));
            return Ok(services);
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(string request)
        {
            return Ok(_medicalnfoService.GetById(Guid.Parse(request)));
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Post(MedicalInfo medicalService)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_medicalnfoService.Add(medicalService));
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Put(MedicalInfo medicalService)
        {
            return Ok(_medicalnfoService.Edit(medicalService));
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(string request)
        {
            return Ok(_medicalnfoService.Delete(Guid.Parse(request)));
        }




        [HttpGet]
        [Route("GetPatientMedicalInfos")]
        public IHttpActionResult GetPatientMedicalInfos(string patientId)
        {
            return Ok(_medicalnfoService.GetPatientMedicalInfos(patientId));
        }
        
        [HttpPost]
        [Route("SavePatientMedicalInfos")]
        public IHttpActionResult SavePatientMedicalInfos(List<PatientMedicalInfo> patientMedicalInfos)
        {
            _medicalnfoService.SavePatientMedicalInfos(patientMedicalInfos);
            return Ok();
        }

    }
}
