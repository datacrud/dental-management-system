using System.Collections.Generic;
using System.Web.Http;
using DM.Models;
using DM.RequestModels;
using DM.Service.Contacts;
using Newtonsoft.Json;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/PatientReports")]
    public class PatientReportController : ApiController
    {
        private readonly IPaymentService _paymentService;

        public PatientReportController(IPaymentService paymentService)
        {
            _paymentService = paymentService;
        }



        [HttpGet]
        [Route("GetPatientPaymentReport")]
        public IHttpActionResult GetPatientPaymentReport(string request)
        {
            PatientReportRequestModel requestModel = JsonConvert.DeserializeObject<PatientReportRequestModel>(request);

            List<Payment> payments = _paymentService.GetByDateRange(requestModel.From, requestModel.To);

            return Ok(payments);
        }

    }
}
