using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Models
{
    public class Patient: BaseModel
    {
        //[Required]
        [DataType(DataType.Text)]        
        [StringLength(8, MinimumLength = 7)]
        [Index("IX_Code", IsUnique = true)]
        //[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Code { get; set; }

        [Required]
        [StringLength(30, ErrorMessage = "Name maximum can be 30 length", MinimumLength = 3)]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [DataType(DataType.Text)]
        public string Address { get; set; }

        public string Gender { get; set; }

        public string Note { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }      

        public ICollection<Prescription> Prescriptions { set; get; } 
        //public ICollection<PatientService> PatientServices { get; set; }
    }


    public enum Gender
    {
        Male =1,
        Female = 2,
        Others = 3
    }
}
