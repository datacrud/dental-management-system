using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;
using DM.Service.Contacts;

namespace DM.Service
{
    public class PaymentService: BaseService<Payment>, IPaymentService
    {
        private readonly IPaymentRepository _paymentRepository;

        public PaymentService(IPaymentRepository paymentRepository) : base(paymentRepository)
        {
            _paymentRepository = paymentRepository;
        }

        public List<Payment> GetByPrescriptionId(Guid prescriptionId)
        {
            return _paymentRepository.GetByPrescriptionId(prescriptionId).ToList();
        }

        public List<Payment> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            return _paymentRepository.GetByDateRange(fromDate.ToLocalTime().Date, toDate.ToLocalTime().Date.AddDays(1)).ToList();
        }
    }
}
