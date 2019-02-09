using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Models
{
    public class Product :BaseModel
    {
        //[Required]
        [StringLength(10,MinimumLength = 0)]
        [DataType(DataType.Text)]
        //[Index("IX_Code", IsUnique = true)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Code { get; set; }

        [Required]
        [StringLength(40,ErrorMessage = "Product name can be maximum 40 length", MinimumLength = 1)]
        [DataType(DataType.Text)]
        [Index("IX_Name",IsUnique = true)]
        public string Name { get; set; }

        [Required]
        public int StartingInventory { get; set; }

        [Required]
        public int Received { get; set; }

        [Required]
        public int Shipped { get; set; }

        [Required]
        public int OnHand { get; set; }

        public int MinimumRequired { get; set; }

        [DataType(DataType.Currency)]
        public double UnitPrice { get; set; }

        [DataType(DataType.Currency)]
        public double SalePrice { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime Created { get; set; }

        [Required]
        [DataType(DataType.DateTime)]
        public DateTime LastUpdate { get; set; }

        [Required]
        public int StatusId { get; set; }


        public virtual  ICollection<Inventory> Inventories { get; set; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
    }
}
