using System;
using System.Web.Http;
using DM.Models;
using DM.Service.Contacts;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/Payments")]
    public class PaymentController : ApiController
    {
        private readonly IPaymentService _paymentService;

        public PaymentController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }


        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult Get()
        {
            return Ok(_paymentService.GetAll());
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(string request)
        {
            return Ok(_paymentService.GetById(Guid.Parse(request)));
        }

        [HttpGet]
        [Route("GetByPrescriptionId")]
        public IHttpActionResult GetByPrescriptionId(string request)
        {
            return Ok(_paymentService.GetByPrescriptionId(Guid.Parse(request)));
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Post(Payment payment)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            return Ok(_paymentService.Add(payment));
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Put(Payment payment)
        {
            return Ok(_paymentService.Edit(payment));
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(string request)
        {
            return Ok(_paymentService.Delete(Guid.Parse(request)));
        }
    }
}
