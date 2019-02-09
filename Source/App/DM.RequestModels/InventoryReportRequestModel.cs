using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.RequestModels
{
    public class InventoryReportRequestModel
    {
        public DateTime From { get; set; }
        public DateTime To { get; set; }
        public Guid ProductId { get; set; }
        public int StatusId { get; set; }
        public int DaysFilterId { get; set; }
    }
}
