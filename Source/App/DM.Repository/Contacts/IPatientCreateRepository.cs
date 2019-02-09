using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.ViewModels;

namespace DM.Repository.Contacts
{
    public interface IPatientCreateRepository: IBaseRepository<Patient>
    {
        Patient GetPatientByCode(string codeToUpperCase);
    }
}
