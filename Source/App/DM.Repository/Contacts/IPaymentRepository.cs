using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;

namespace DM.Repository.Contacts
{
    public interface IPaymentRepository: IBaseRepository<Payment>
    {
        IQueryable<Payment> GetByPrescriptionId(Guid prescriptionId);
        IQueryable<Payment> GetByDateRange(DateTime fromDate, DateTime toDate);
    }
}
