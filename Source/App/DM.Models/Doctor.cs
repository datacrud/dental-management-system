using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.Models
{
    public class Doctor :BaseModel
    {
        public string Code { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public DateTime Created { get; set; }

        public DateTime LastUpdate { get; set; }


        public virtual ICollection<Appointment> Appointments { get; set; } 
    }
}
