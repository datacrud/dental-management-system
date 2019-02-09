using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;
using DM.RequestModels;

namespace DM.Repository
{
    public class InventoryRepository: BaseRepository<Inventory>, IInventoryRepository
    {
        private readonly DentalDbContext _db;

        public InventoryRepository(DentalDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Inventory> GetProductHistory(InventoryHistoryRequestModel model)
        {
            DateTime ending = model.DaysFilterId == 30 ? DateTime.Now.AddMonths(-1).ToLocalTime() : DateTime.Now.AddDays(-model.DaysFilterId).ToLocalTime();

            return _db.Inventories.Include(x => x.Status).Where(x=> x.ProductId == model.Id && x.LastUpdate >= ending).OrderByDescending(x=>x.LastUpdate).AsQueryable();
        }

        public IQueryable<Inventory> GetAllIncludeStatus()
        {
            return _db.Inventories.Include(x => x.Status).AsQueryable();
        }


        public IQueryable<Inventory> GetProductHistoryByDateRange(Guid productId, DateTime fromDate, DateTime toDate)
        {
            return
                _db.Inventories.Where(
                    x =>
                        x.ProductId == productId &&
                        (x.Created >= fromDate && x.Created <= toDate)).AsQueryable();
        }
    }
}
