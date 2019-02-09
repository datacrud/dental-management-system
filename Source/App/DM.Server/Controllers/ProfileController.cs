using System.Web.Http;
using DM.AuthServer.Models;
using DM.AuthServer.Service;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/Profile")]
    public class ProfileController : BaseController<ApplicationUser>
    {
        private readonly IProfileService _service;

        public ProfileController(IProfileService service) : base(service)
        {
            _service = service;
        }

        //Profile Section
        [HttpGet]
        [Route("UserProfile")]
        public IHttpActionResult GetUserProfile()
        {
            var model = _service.GetUserProfile();
            return Ok(model);
        }

        [HttpPost]
        [Route("UpdateProfile")]
        public IHttpActionResult UpdateProfile(Models.RequestModels.UserProfileUpdateRequestModel model)
        {
            return Ok(_service.UpdateProfile(model));
        }

        [HttpPost]
        [Route("UpdatePassword")]
        public IHttpActionResult UpdatePassword(Models.RequestModels.ChangePasswordRequestModel model)
        {
            return Ok(_service.UpdatePassword(model));
        }
    }
}
