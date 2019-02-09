using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DM.Models;
using DM.Repository.Contacts;

namespace DM.Repository
{
    public class AppointmentRepository: BaseRepository<Appointment>, IAppointmentRepository
    {
        private readonly DentalDbContext _db;

        public AppointmentRepository(DentalDbContext db) : base(db)
        {
            _db = db;
        }

        public IQueryable<Appointment> GetByDate(DateTime date)
        {
            var endDate = date.AddDays(1);

            return
                _db.Appointments.Include(x => x.Status)
                    .Where(x => (x.Date >= date && x.Date <= endDate) && x.StatusId == 7)
                    .OrderBy(x => x.Time)
                    .AsQueryable();
        }
    }
}
