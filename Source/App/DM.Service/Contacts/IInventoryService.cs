using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.RequestModels;

namespace DM.Service.Contacts
{
    public interface IInventoryService: IBaseService<Inventory>
    {
        List<Inventory> GetProductHistory(InventoryHistoryRequestModel model);
        List<Inventory> GetAllIncludeStatus();
        List<Inventory> GetProductHistoryByDateRange(Guid produuctId, DateTime fromDate, DateTime toDate);
    }
}
