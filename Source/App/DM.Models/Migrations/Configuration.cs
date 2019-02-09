using System.Collections.Generic;

namespace DM.Models.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<DentalDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(DentalDbContext context)
        {
            if (!context.Statuses.Any())
            {
                AddStatus(context);
            }

            if (!context.Doctors.Any())
            {
                AddDoctor(context);
            }

        }

        private static void AddDoctor(DentalDbContext context)
        {
            context.Doctors.Add(new Doctor()
            {
                Id = Guid.Parse("9b6ba3ad-c9be-e511-9bf4-402cf40f4b2f"),
                Code = "DR001",
                Name = "Dental Doctor",
                Phone = "01911xxxxxx",
                Created = DateTime.Now,
                LastUpdate = DateTime.Now
            });

            context.SaveChanges();
        }

        private static void AddStatus(DentalDbContext context)
        {
            List<Status> statuses = new List<Status>()
                {
                    new Status() { Name = "In Stock" },
                    new Status() { Name = "Out Of Stock" },
                    new Status() { Name = "Received" },
                    new Status() { Name = "Shipped" },
                    new Status() { Name = "Active" },
                    new Status() { Name = "Closed" },
                    new Status() { Name = "Appointed" },
                    new Status() { Name = "Visited" },
                };

            context.Statuses.AddRange(statuses);
            context.SaveChanges();
        }
    }
}
