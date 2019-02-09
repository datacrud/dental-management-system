using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;

namespace DM.Repository.Contacts
{
    public interface IProductRepository: IBaseRepository<Product>
    {
        IQueryable<Product> GetProductsIncludeStatus();
    }
}
