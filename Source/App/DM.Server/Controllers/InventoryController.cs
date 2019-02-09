using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DM.Models;
using DM.RequestModels;
using DM.Service.Contacts;
using Newtonsoft.Json;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/Inventories")]
    public class InventoryController : ApiController
    {
        private readonly IInventoryService _inventoryService;

        public InventoryController(IInventoryService inventoryService)
        {
            _inventoryService = inventoryService;
        }


        [HttpGet]
        [Route("GetAll")]
        public IHttpActionResult Get()
        {
            List<Inventory> inventories = _inventoryService.GetAll();
            return Ok(inventories);
        }

        [HttpGet]
        [Route("GetById")]
        public IHttpActionResult Get(string request)
        {
            Inventory inventory = _inventoryService.GetById(Guid.Parse(request));
            return Ok(inventory);
        }

        [HttpGet]
        [Route("GetProductHistory")]
        public IHttpActionResult GetProductHistory(string request)
        {
            InventoryHistoryRequestModel model = JsonConvert.DeserializeObject<InventoryHistoryRequestModel>(request);
            List<Inventory> inventories = _inventoryService.GetProductHistory(model);
            return Ok(inventories);
        }

        [HttpGet]
        [Route("Search")]
        public IHttpActionResult Search(string from, string to, string productId)
        {
            Guid id = Guid.Parse(productId);
            DateTime start = DateTime.Parse(from);
            DateTime end = DateTime.Parse(to);

            List<Inventory> products = _inventoryService.GetAllIncludeStatus();

            return Ok(products.Where(x => x.ProductId == id && x.Created >= start && x.Created <= end).ToList());
        }

        [HttpPost]
        [Route("Create")]
        public IHttpActionResult Post(Inventory inventory)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            return Ok(_inventoryService.Add(inventory));
        }

        [HttpPut]
        [Route("Update")]
        public IHttpActionResult Put(Inventory inventory)
        {
            return Ok(_inventoryService.Edit(inventory));
        }

        [HttpDelete]
        [Route("Delete")]
        public IHttpActionResult Delete(string request)
        {
            return Ok(_inventoryService.Delete(Guid.Parse(request)));
        }
    }
}
