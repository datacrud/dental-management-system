using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository;
using DM.Repository.Contacts;
using DM.Service.Contacts;
using DM.ViewModels;

namespace DM.Service
{
    public class ProductService : BaseService<Product>, IProductService
    {
        private readonly IProductRepository _productRepository;

        public ProductService(IProductRepository productRepository) : base(productRepository)
        {
            _productRepository = productRepository;
        }


        public List<Product> GetProductsIncludeStatus()
        {
            IQueryable<Product> products = _productRepository.GetProductsIncludeStatus();
            return products.ToList();
        } 

        public List<ProductViewModel> GetProducts()
        {
            IQueryable<Product> products = _productRepository.GetAll();

            IQueryable<ProductViewModel> productViewModel = products.Select(x=> new ProductViewModel()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name,
                StartingInventory = x.StartingInventory,
                MinimumRequired = x.MinimumRequired,
                UnitPrice = x.UnitPrice,
                SalePrice = x.SalePrice,
                Created = x.Created,
                LastUpdate = x.LastUpdate
            }).OrderByDescending(x=> x.LastUpdate);

            return productViewModel.ToList();
        }



        public List<ProductsNameViewModel> GetProductsName()
        {
            IQueryable<Product> products = _productRepository.GetAll();

            IQueryable<ProductsNameViewModel> productListViewModel = products.Select(x => new ProductsNameViewModel()
            {
                Id = x.Id,
                Code = x.Code,
                Name = x.Name
            }).OrderBy(x=> x.Name);

            return productListViewModel.ToList();
        }
    }
}
