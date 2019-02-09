using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Models
{
    public class Appointment : BaseModel
    {
        public string Code { get; set; }

        [Required]
        [DataType(DataType.Text)]
        [StringLength(40, MinimumLength = 2)]
        public string PatientNameOrId { get; set; }

        [Required]
        public int Age { get; set; }

        public string Phone { get; set; }

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public DateTime Time { get; set; }

        [Required]
        public DateTime Created { get; set; }

        [Required]
        public DateTime LastUpdate { get; set; }

        [Required]
        public Guid DoctorId { get; set; }

        [Required]
        public int StatusId { get; set; }

        [ForeignKey("DoctorId")]
        public virtual Doctor Doctor { get; set; }

        [ForeignKey("StatusId")]
        public Status Status { get; set; }
    }
}
