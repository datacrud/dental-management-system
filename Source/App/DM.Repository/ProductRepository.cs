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
    public class ProductRepository: BaseRepository<Product>, IProductRepository
    {
        protected DentalDbContext db = new DentalDbContext();

        public ProductRepository(DentalDbContext db) : base(db)
        {
        }

        public IQueryable<Product> GetProductsIncludeStatus()
        {
            return db.Products.Include(x=> x.Status).AsQueryable();
        } 
    }
}
