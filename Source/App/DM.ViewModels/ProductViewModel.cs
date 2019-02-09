using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.ViewModels
{
    public class ProductViewModel
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Name { get; set; }
        public int StartingInventory { get; set; }
        public int MinimumRequired { get; set; }
        public double UnitPrice { get; set; }
        public double SalePrice { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastUpdate { get; set; }
    }
}
