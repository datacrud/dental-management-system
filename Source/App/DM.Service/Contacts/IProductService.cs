using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.ViewModels;

namespace DM.Service.Contacts
{
    public interface IProductService: IBaseService<Product>
    {
        List<Product> GetProductsIncludeStatus();
        List<ProductViewModel> GetProducts();
        List<ProductsNameViewModel> GetProductsName();        
    }
}
