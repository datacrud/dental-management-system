using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.ViewModels
{
    public class PatientGridViewModel
    {
        public Guid Id { get; set; }

        public string Code { get; set; }

        public string Name { get; set; }

        public int Age { get; set; }

        public DateTime LastVisitingDate { get; set; }

        public double TotalPayable { get; set; }

        public double TotalPaid { get; set; }

        public double TotalDue { get; set; }
    }
}
