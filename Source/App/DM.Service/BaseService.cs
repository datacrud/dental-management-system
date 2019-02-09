using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using DM.Repository;
using DM.Repository.Contacts;
using DM.ResponseModels;
using DM.Service.Contacts;

namespace DM.Service
{
    public class BaseService<TEntity> : IBaseService<TEntity> where TEntity: class
    {
        protected  IBaseRepository<TEntity> Repository;

        protected BaseService(IBaseRepository<TEntity> repository)
        {
            Repository = repository;
        }

        public virtual List<TEntity> GetAll()
        {
            return Repository.GetAll().ToList();
        }

        public virtual TEntity GetById(Guid id)
        {
            return Repository.GetById(id);
        }

        public TEntity GetById(string id)
        {
            return Repository.GetById(id);
        }


        public virtual bool Add(TEntity entity)
        {
            bool response = true;

            try
            {
                Repository.Add(entity);
                Repository.Commit();                
            }
            catch (Exception exception)
            {
                response = false;
            }

            return response;
        }

        public virtual bool Edit(TEntity entity)
        {
            bool response = true;

            try
            {                
                Repository.Edit(entity);
                Repository.Commit();
            }
            catch (Exception exception)
            {
                response = false;
            }

            return response;
        }

        public virtual bool Delete(Guid id)
        {
            bool response = true;

            try
            {
                Repository.Delete(Repository.GetById(id));
                Repository.Commit();
            }
            catch (Exception exception)
            {
                response = false;
            }

            return response;
        }

        public bool Delete(string id)
        {
            bool response = true;

            try
            {
                Repository.Delete(Repository.GetById(id));
                Repository.Commit();
            }
            catch (Exception exception)
            {
                response = false;
            }

            return response;
        }
    }
}
