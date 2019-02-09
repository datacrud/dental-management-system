using System;
using System.Data.Entity;
using System.Linq;
using DM.AuthServer.Models;
using DM.Repository;
using DM.Repository.Contacts;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DM.AuthServer.Repository
{
    public interface IRoleRepository : IBaseRepository<IdentityRole>
    {        
    }

    public class RoleRepository : BaseRepository<IdentityRole>, IRoleRepository
    {
        private readonly ApplicationDbContext _db;

        public RoleRepository(ApplicationDbContext db) : base(db)
        {
            _db = db;
        }


        //public override IdentityRole GetById(string id)
        //{
        //    var role = _db.Roles.Find(id);

        //    return role;
        //}


        //public override IQueryable<IdentityRole> GetAll()
        //{
        //    return _db.Roles.AsQueryable();
        //}
    }
}