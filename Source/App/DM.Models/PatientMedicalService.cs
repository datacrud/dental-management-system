using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Models
{
    public class PatientMedicalService: BaseModel
    {
        [Required]
        public Guid PatientId { get; set; }

        [Required]
        public Guid PrescriptionId { get; set; }

        [Required]
        public Guid MedicalServiceId { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }


        [ForeignKey("PatientId")]
        public virtual Patient Patient { get; set; }

        [ForeignKey("PrescriptionId")]
        public virtual Prescription Prescription { get; set; }

        [ForeignKey("MedicalServiceId")]
        public virtual MedicalService MedicalService { get; set; }
    }
}
