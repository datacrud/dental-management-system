using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Hosting;
using DM.AuthServer.Models;
using DM.AuthServer.Repository;
using DM.Service;
using DM.Service.Contacts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;

namespace DM.AuthServer.Service
{
    public interface IRoleService : IBaseService<IdentityRole>
    {
        
    }



    public class RoleService : BaseService<IdentityRole>, IRoleService
    {
        private readonly IRoleRepository _repository;

        public RoleService(IRoleRepository repository) :base (repository)
        {
            _repository = repository;
        }

        public override List<IdentityRole> GetAll()
        {
            var roles = _repository.GetAll().ToList();

            var isInRole = HttpContext.Current.User.IsInRole("SystemAdmin");
            if (isInRole) return roles;

            foreach (var role in roles.Where(role => role.Name.ToLower() == "SystemAdmin".ToLower()))
            {
                roles.Remove(role);
                break;
            }

            return roles;
        }
    }
}