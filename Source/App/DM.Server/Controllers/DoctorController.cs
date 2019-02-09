using System;
using System.Web.Http;
using DM.Service.Contacts;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/Doctors")]
    public class DoctorController : ApiController
    {
        private readonly IDoctorService _doctorService;

        public DoctorController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }


        [Route("GetAll")]
        public IHttpActionResult Get()
        {
            return Ok(_doctorService.GetAll());
        }


        [Route("GetById")]
        public IHttpActionResult Get(string request)
        {
            return Ok(_doctorService.GetById(Guid.Parse(request)));
        }

    }
}
