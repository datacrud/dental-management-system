using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Models
{
    public class Prescription: BaseModel
    {
        //[Required]
        [DataType(DataType.Text)]
        [StringLength(18, MinimumLength = 12)]
        [Index("IX_Code",IsUnique = true)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Code { get; set; }

        [Required]
        public Guid PatientId { get; set; }

        public double TotalCharge { get; set; }

        public double DiscountPercent { get; set; }

        public double DiscountAmount { get; set; }
        public double FixedDiscount { get; set; } = 0;
        public double TotalDiscountAmount => DiscountAmount + FixedDiscount;
        public double TotalPayable { get; set; }

        public double TotalPaid { get; set; }

        public double TotalDue { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("PatientId")]
        public virtual Patient Patient { set; get; }

        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }


        public virtual ICollection<PatientMedicalService> PatientMedicalServices { get; set; } 

        public ICollection<Payment> Payments { set; get; } 
    }
}
