using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;
using DM.RequestModels;
using DM.Service.Contacts;

namespace DM.Service
{
    public class InventoryService: BaseService<Inventory>, IInventoryService
    {
        private readonly IInventoryRepository _inventoryRepository;

        public InventoryService(IInventoryRepository inventoryRepository): base(inventoryRepository)
        {
            _inventoryRepository = inventoryRepository;
        }

        public List<Inventory> GetProductHistory(InventoryHistoryRequestModel model)
        {
            return _inventoryRepository.GetProductHistory(model).ToList();
        }

        public List<Inventory> GetAllIncludeStatus()
        {
            return _inventoryRepository.GetAllIncludeStatus().ToList();
        }

        public List<Inventory> GetProductHistoryByDateRange(Guid productId, DateTime fromDate, DateTime toDate)
        {
            return _inventoryRepository.GetProductHistoryByDateRange(productId, fromDate.ToLocalTime().Date, toDate.ToLocalTime().Date.AddDays(1)).OrderBy(x=> x.LastUpdate).ToList();
        }
    }
}
