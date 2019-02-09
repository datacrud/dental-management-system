using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.ViewModels
{
    public class InventoryReportViewModel
    {
        public string Name { get; set; }
        public int Received { get; set; }
        public int Shipped { get; set; }
        public int OnHand { get; set; }
    }
}
