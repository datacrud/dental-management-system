using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;

namespace DM.Repository
{
    public class PaymentRepository: BaseRepository<Payment>, IPaymentRepository
    {
        private readonly DentalDbContext _db;

        public PaymentRepository(DentalDbContext db) : base(db)
        {
            _db = db;
        }


        public IQueryable<Payment> GetByPrescriptionId(Guid prescriptionId)
        {
            return _db.Payments.Where(x => x.PrescriptionId == prescriptionId).AsQueryable();
        }

        public IQueryable<Payment> GetByDateRange(DateTime fromDate, DateTime toDate)
        {
            return _db.Payments.Where(x => x.Created >= fromDate && x.Created <= toDate).AsQueryable();
        }
    }
}
