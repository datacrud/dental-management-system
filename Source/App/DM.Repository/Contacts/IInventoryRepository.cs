using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.RequestModels;

namespace DM.Repository.Contacts
{
    public interface IInventoryRepository: IBaseRepository<Inventory>
    {
        IQueryable<Inventory> GetProductHistory(InventoryHistoryRequestModel request);
        IQueryable<Inventory> GetAllIncludeStatus();
        IQueryable<Inventory> GetProductHistoryByDateRange(Guid productId, DateTime fromDate, DateTime toDate);
    }
}
