using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DM.Models
{
    public class MedicalInfo: BaseModel
    {
                
        [Required]
        [StringLength(50, ErrorMessage = "Name maximum can be 50 length",MinimumLength = 2)]
        [Index("IX_Name", IsUnique = true)]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        [NotMapped] public bool IsChecked { get; set; }
    }
}
