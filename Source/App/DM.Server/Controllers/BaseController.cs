using System.Net;
using System.Web.Http;
using DM.Service.Contacts;

namespace DM.AuthServer.Controllers
{
    public interface IBaseController<T> where T: class
    {
        IHttpActionResult Get();
        IHttpActionResult Get(string request);
        IHttpActionResult Post(T entity);
        IHttpActionResult Put(T entity);
        IHttpActionResult Delete(string request);
    }

    [Authorize]
    public class BaseController<TEntity> : ApiController, IBaseController<TEntity> where TEntity : class 
    {

        protected IBaseService<TEntity> Service;

        protected BaseController(IBaseService<TEntity> service)
        {
            Service = service;
        }


        public virtual IHttpActionResult Get()
        {
            var entities = Service.GetAll();

            return Ok(entities);
        }


        public virtual IHttpActionResult Get(string request)
        {
            var entity = Service.GetById(request);

            return Ok(entity);
        }


        public virtual IHttpActionResult Post(TEntity entity)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var add = Service.Add(entity);

            return Ok(add);
        }


        public virtual IHttpActionResult Put(TEntity model)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var edit = Service.Edit(model);

            return Ok(edit);
        }


        public virtual IHttpActionResult Delete(string request)
        {
            var delete = Service.Delete(request);

            return Ok(delete);
        }
    }
}
