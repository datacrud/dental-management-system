using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DM.RequestModels
{
    public class PatientSearchRequestModel
    {
        public string SearchKey { get; set; }
        public int FilterId { get; set; }
    }
}
