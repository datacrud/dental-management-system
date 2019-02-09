using System;
using System.Linq;
using System.Web.Http;
using DM.Models;
using DM.RequestModels;
using DM.Service.Contacts;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/Appointments")]
    public class AppointmentController : ApiController
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }



        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult Get()
        {
            return Ok(_appointmentService.GetAll());
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(string request)
        {
            return Ok(_appointmentService.GetById(Guid.Parse(request)));
        }

        [HttpGet]
        [Route("GetByDate")]
        public IHttpActionResult GetByDate(string request)
        {
            return Ok(_appointmentService.GetByDate(DateTime.Parse(request)));
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Post(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            int count = _appointmentService.GetAll().Count();

            appointment.Code = HelperRequestModel.GenerateAppointmentCode((count + 1).ToString());
            //appointment.Date = appointment.Date.ToLocalTime();
            //appointment.Time = appointment.Time.ToLocalTime();

            return Ok(_appointmentService.Add(appointment));
        }


        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Put(Appointment appointment)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            //appointment.Date = appointment.Date.ToLocalTime();
            //appointment.Time = appointment.Time.ToLocalTime();

            return Ok(_appointmentService.Edit(appointment));
        }

        [HttpPut]
        [Route("UpdateStatus")]
        public IHttpActionResult UpdateStatus(string request, int statusId)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            Appointment appointment = _appointmentService.GetById(Guid.Parse(request));
            appointment.StatusId = statusId;
            return Ok(_appointmentService.Edit(appointment));
        }


        [HttpPut]
        [Route("Delete")]
        public IHttpActionResult Delete(string request)
        {
            return Ok(_appointmentService.Delete(Guid.Parse(request)));
        }


        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search(string request)
        {
            return Ok(_appointmentService.Search(request.ToUpper()));
        }


    }
}
