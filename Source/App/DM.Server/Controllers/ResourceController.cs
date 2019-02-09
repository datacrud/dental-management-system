using System;
using System.Linq;
using System.Web.Http;
using DM.AuthServer.Models;
using DM.AuthServer.Service;

namespace DM.AuthServer.Controllers
{
    [Authorize(Roles = "SystemAdmin")]
    [RoutePrefix("api/Resource")]
    public class ResourceController : BaseController<SecurityModels.Resource>
    {
        private readonly IResourceService _service;

        public ResourceController(IResourceService service) :base(service)
        {
            _service = service;
        }


        [HttpGet]
        [Route("GetPrivateResources")]
        public IHttpActionResult GetPrivateResources()
        {
            var resources = _service.GetAll().Where(x => x.IsPublic == false).OrderBy(x => x.Name);

            return Ok(resources);
        }


        public override IHttpActionResult Post(SecurityModels.Resource model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            model.Id = Guid.NewGuid().ToString();
            var add = _service.Add(model);

            return Ok(add);
        }

    }
}
