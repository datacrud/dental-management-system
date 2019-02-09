using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.ResponseModels;
using DM.ViewModels;

namespace DM.Service.Contacts
{
    public interface IBaseService<T> where T: class
    {
        List<T> GetAll();
        T GetById(Guid id);
        T GetById(string id);
        bool Add(T entity);
        bool Edit(T entity);
        bool Delete(Guid id);
        bool Delete(string id);
    }
}
