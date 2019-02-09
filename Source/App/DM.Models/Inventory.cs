using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Models
{
    public class Inventory: BaseModel
    {
        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public string CashMemoNo { get; set; }

        [Required]
        public int OnHand { get; set; }

        [Required]
        public int ReceivedOrShippedQuantity { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
    }
}