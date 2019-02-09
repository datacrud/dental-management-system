using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;

namespace DM.Service.Contacts
{
    public interface IPaymentService: IBaseService<Payment>
    {
        List<Payment> GetByPrescriptionId(Guid prescriptionId);
        List<Payment> GetByDateRange(DateTime fromDate, DateTime toDate);
    }
}
