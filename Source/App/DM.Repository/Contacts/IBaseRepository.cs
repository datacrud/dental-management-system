using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Repository.Contacts
{
    public interface IBaseRepository<T> where T: class
    {
        IQueryable<T> GetAll();
        T GetById(Guid id);
        T GetById(string id);
        T Add(T entity);
        EntityState Edit(T entity);
        T Delete(T entity);        

        void Commit();
    }
}
