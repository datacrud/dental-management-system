using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DM.Models;
using DM.Service.Contacts;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/MedicalServices")]
    public class MedicalServiceController : ApiController
    {
        private readonly IMedicalServiceService _medicalServiceService;

        public MedicalServiceController(IMedicalServiceService medicalServiceService)
        {
            _medicalServiceService = medicalServiceService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult Get()
        {
            List<MedicalService> services = new List<MedicalService>(_medicalServiceService.GetAll().OrderByDescending(x=> x.LastUpdate));
            return Ok(services);
        }

        [HttpGet]
        [Route("GetAllOrderByName")]
        public IHttpActionResult GetAllOrderByName()
        {
            List<MedicalService> services = new List<MedicalService>(_medicalServiceService.GetAll().OrderBy(x=> x.Name));
            return Ok(services);
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(string request)
        {
            return Ok(_medicalServiceService.GetById(Guid.Parse(request)));
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Post(MedicalService medicalService)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_medicalServiceService.Add(medicalService));
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Put(MedicalService medicalService)
        {
            return Ok(_medicalServiceService.Edit(medicalService));
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(string request)
        {
            return Ok(_medicalServiceService.Delete(Guid.Parse(request)));
        }

    }
}
