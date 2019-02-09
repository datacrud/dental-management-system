using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using DM.Models;
using DM.RequestModels;
using DM.Service.Contacts;
using DM.ViewModels;
using Newtonsoft.Json;

namespace DM.AuthServer.Controllers
{
    [Authorize]
    [RoutePrefix("api/InventoryReports")]
    public class InventoryReportController : ApiController
    {
        private readonly IProductService _productService;
        private readonly IInventoryService _inventoryService;

        public InventoryReportController(IProductService productService, IInventoryService inventoryService)
        {
            _productService = productService;
            _inventoryService = inventoryService;
        }


        [HttpGet]
        [Route("GetReport")]
        public IHttpActionResult GetReport(string request)
        {
            InventoryReportRequestModel model = JsonConvert.DeserializeObject<InventoryReportRequestModel>(request);

            List<InventoryReportViewModel> inventoryReports = new List<InventoryReportViewModel>();

            List<Product> products = _productService.GetAll();
            foreach (var product in products)
            {
                int received = 0;
                int shipped = 0;
                int onHand = 0;

                List<Inventory> inventories = _inventoryService.GetProductHistoryByDateRange(product.Id, model.From, model.To);                

                if (inventories.Count == 0)
                {
                    onHand = GetOnHand(model, product, onHand);
                }

                foreach (Inventory inventory in inventories)
                {
                    if(inventory.StatusId == 3)
                    {
                        received += inventory.ReceivedOrShippedQuantity;
                        onHand = inventory.OnHand + inventory.ReceivedOrShippedQuantity;
                    }

                    else if (inventory.StatusId == 4)
                    {
                        shipped += inventory.ReceivedOrShippedQuantity;
                        onHand = inventory.OnHand - inventory.ReceivedOrShippedQuantity;
                    }
                    
                }                

                inventoryReports.Add(new InventoryReportViewModel()
                {
                    Name = product.Name,
                    Received = received,
                    Shipped = shipped,
                    OnHand = onHand
                });
            }

            return Ok(inventoryReports);
        }

        private int GetOnHand(InventoryReportRequestModel model, Product product, int onHand)
        {
            var searchOnHandOnPreviousMonth = _inventoryService.GetProductHistoryByDateRange(product.Id, model.From.AddMonths(1), model.To).LastOrDefault();

            if (searchOnHandOnPreviousMonth != null)
            {
                if (searchOnHandOnPreviousMonth.StatusId == 3)
                    onHand = searchOnHandOnPreviousMonth.OnHand + searchOnHandOnPreviousMonth.ReceivedOrShippedQuantity;

                else if (searchOnHandOnPreviousMonth.StatusId == 4)
                    onHand = searchOnHandOnPreviousMonth.OnHand - searchOnHandOnPreviousMonth.ReceivedOrShippedQuantity;
            }

            else
            {
                var searchOnHandOnNextMonth = _inventoryService.GetProductHistoryByDateRange(product.Id, model.From, model.To.AddMonths(1)).FirstOrDefault();

                if (searchOnHandOnNextMonth != null)
                {
                    if (searchOnHandOnNextMonth.StatusId == 3)
                        onHand = searchOnHandOnNextMonth.OnHand + searchOnHandOnNextMonth.ReceivedOrShippedQuantity;

                    else if (searchOnHandOnNextMonth.StatusId == 4)
                        onHand = searchOnHandOnNextMonth.OnHand - searchOnHandOnNextMonth.ReceivedOrShippedQuantity;
                }
                else
                {
                    onHand = product.OnHand;
                }
            }

            

            return onHand;
        }
    }
}
