using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.ViewModels;

namespace DM.Service.Contacts
{
    public interface IPatientCreateService: IBaseService<Patient>
    {
        List<PatientViewModel> GetPatientViewModel();

        Patient GetPatientByCode(string code);
    }
}
