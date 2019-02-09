using System;
using System.Collections.Generic;
using System.Web.Http;
using DM.AuthServer.Service;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DM.AuthServer.Controllers
{
    [Authorize(Roles = "SystemAdmin, Admin")]
    [RoutePrefix("api/Role")]
    public class RoleController : BaseController<IdentityRole>
    {
        private readonly IRoleService _service;

        public RoleController(IRoleService service) :base(service)
        {
            _service = service;
        }

        
        public override IHttpActionResult Post(IdentityRole model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Id = Guid.NewGuid().ToString();
            var add = _service.Add(model);

            return Ok(add);
        }


    }
}
