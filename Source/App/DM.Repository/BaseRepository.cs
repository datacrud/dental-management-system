using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;

namespace DM.Repository
{
    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity: class
    {
        protected DbContext DbContext;

        protected BaseRepository(DbContext db)
        {
            DbContext = db;
        }

        public virtual IQueryable<TEntity> GetAll()
        {
            return DbContext.Set<TEntity>().AsQueryable();
        }

        public virtual TEntity GetById(Guid id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public virtual TEntity GetById(string id)
        {
            return DbContext.Set<TEntity>().Find(id);
        }

        public virtual TEntity Add(TEntity entity)
        {
            return DbContext.Set<TEntity>().Add(entity);
        }

        public virtual EntityState Edit(TEntity entity)
        {
            return DbContext.Entry(entity).State = EntityState.Modified;
        }

        public virtual TEntity Delete(TEntity entity)
        {
            return DbContext.Set<TEntity>().Remove(entity);
        }        

        public virtual void Commit()
        {
            DbContext.SaveChanges();
        }
    }
}
