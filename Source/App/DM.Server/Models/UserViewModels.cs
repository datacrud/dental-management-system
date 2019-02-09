using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;

namespace DM.AuthServer.Models
{
    public class UserViewModels
    {
        public class UserInfoViewModel
        {
            public UserInfoViewModel(ApplicationUser model)
            {
                Id = model.Id;
                FirstName = model.FirstName;
                LastName = model.LastName;
                Email = model.Email;
                PhoneNumber = model.PhoneNumber;
                UserName = model.UserName;
                Roles = model.Roles;
            }

            public string Id { get; set; }

            public string FirstName { get; set; }

            public string LastName { get; set; }

            public string Email { get; set; }

            public string PhoneNumber { get; set; }

            public string UserName { get; set; }             

            public ICollection<IdentityUserRole> Roles { get; set; }

            public ICollection<string> RoleNames { get; set; }
        }

    }
}