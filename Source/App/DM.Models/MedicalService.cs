using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Models
{
    public class MedicalService: BaseModel
    {
        
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        [Index("IX_Code",IsUnique = true)]
        public int Code { get; set; }

        [Required]
        [StringLength(50, ErrorMessage = "Name maximum can be 50 length",MinimumLength = 2)]
        [Index("IX_Name", IsUnique = true)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public string Charge { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }


        [NotMapped] public int Quantity { get; set; } = 1;
        [NotMapped] public int TotalCharge { get => Convert.ToInt32(Charge) * Quantity; set => TotalCharge = value; }

    }
}
