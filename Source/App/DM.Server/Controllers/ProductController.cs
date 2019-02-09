using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DM.Models;
using DM.Service.Contacts;
using DM.ViewModels;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/Products")]
    public class ProductController : ApiController
    {
        private readonly IProductService _productService;


        public ProductController(IProductService productService)
        {            
            _productService = productService;
        }

        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult Get()
        {
            List<Product> products = _productService.GetAll();
            return Ok(products);
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(string request)
        {
            Product product = _productService.GetById(Guid.Parse(request));
            return Ok(product);
        }

        [HttpGet]
        [Route("GetProductsIncludeStatus")]
        public IHttpActionResult GetProductsIncludeStatus()
        {
            List<Product> products = _productService.GetProductsIncludeStatus();
            return Ok(products.Take(100));
        }        

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search(string request)
        {
            var key = request.ToUpper();
            List<Product> products = _productService.GetProductsIncludeStatus();
            return Ok(products.Where(x => x.Code.ToUpper().Contains(key) || x.Name.ToUpper().Contains(key)));
        }

        [HttpGet]
        [Route("Filter")]
        public IHttpActionResult Filter(string request)
        {
            var key = Convert.ToInt16(request);
            List<Product> products = _productService.GetProductsIncludeStatus();
            IEnumerable<Product> enumerable;

            if (key == 1 || key == 2)
            {
                enumerable = products.Where(x => x.StatusId == key);
            }                
            else
            {
                enumerable = products;
            }            

            return Ok(enumerable);
        }

        [HttpGet]
        [Route("SearchProduct")]
        public IHttpActionResult SearchProduct(string request)
        {
            var key = request.ToUpper();
            List<Product> products = _productService.GetAll();
            return Ok(products.Where(x => x.Code.ToUpper().Contains(key) || x.Name.ToUpper().Contains(key)));
        }        

        [HttpGet]
        [Route("GetProducts")]
        public IHttpActionResult GetProducts()
        {
            List<ProductViewModel> products = _productService.GetProducts();
            return Ok(products);
        }

        [HttpGet]
        [Route("GetProductsName")]
        public IHttpActionResult GetProductsName()
        {
            List<ProductsNameViewModel> products = _productService.GetProductsName();
            return Ok(products);
        }        

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Post(Product product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_productService.Add(product));
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Put(Product product)
        {
            return Ok(_productService.Edit(product));
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(string request)
        {
            return Ok(_productService.Delete(Guid.Parse(request)));
        }
    }
}