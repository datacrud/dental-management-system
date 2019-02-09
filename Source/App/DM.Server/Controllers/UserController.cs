using System.Web;
using System.Web.Http;
using DM.AuthServer.Models;
using DM.AuthServer.Service;
using Microsoft.AspNet.Identity;

namespace DM.AuthServer.Controllers
{
    [Authorize(Roles = "SystemAdmin, Admin")]
    [RoutePrefix("api/User")]
    public class UserController : BaseController<ApplicationUser>
    {
        private readonly IUserService _service;

        public UserController(IUserService service) : base(service)
        {
            _service = service;
        }
        

        //User Section
        [HttpGet]
        [Route("GetUsers")]
        public IHttpActionResult GetUsers()
        {
            return Ok(_service.GetUsers());
        }

        [HttpGet]
        [Route("GetUser")]
        public IHttpActionResult GetUser(string id)
        {
            return Ok(_service.GetUser(id));
        }


        [HttpPost]
        [Route("CreateUser")]
        public IHttpActionResult CreateUser(Models.RequestModels.UserCreateRequestModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
        
            return Ok(_service.CreateUser(model));
        }        


        [HttpPut]
        [Route("UpdateUser")]
        public IHttpActionResult UpdateUser(Models.RequestModels.UserCreateRequestModel model)
        {
            return Ok(_service.UpdateUser(model));
        }


        [HttpDelete]
        [Route("DeleteUser")]
        public IHttpActionResult DeleteUser(string id)
        {
            if (HttpContext.Current.User.Identity.GetUserId() == id)
                return BadRequest();

            return Ok(_service.DeleteUser(id));
        }
    }
}
