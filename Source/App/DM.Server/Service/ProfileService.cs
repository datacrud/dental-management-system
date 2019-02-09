using System.Collections.Generic;
using System.Threading.Tasks;
using System.Web;
using DM.AuthServer.Models;
using DM.AuthServer.Repository;
using DM.Service;
using DM.Service.Contacts;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;

namespace DM.AuthServer.Service
{
    public interface IProfileService : IBaseService<ApplicationUser>
    {

        Models.ViewModels.ProfileViewModel GetUserProfile();
        Task<IdentityResult> UpdateProfile(Models.RequestModels.UserProfileUpdateRequestModel model);
        bool UpdatePassword(Models.RequestModels.ChangePasswordRequestModel model);
    }

    public class ProfileService : BaseService<ApplicationUser>, IProfileService
    {
        private readonly IProfileRepository _repository;
        private readonly IRoleRepository _roleRepository;
        private readonly ApplicationUserManager _manager;

        public ProfileService(IProfileRepository repository, IRoleRepository roleRepository): base(repository)
        {
            _repository = repository;
            _roleRepository = roleRepository;
            _manager = HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
        }



        //Profile Section
        public Models.ViewModels.ProfileViewModel GetUserProfile()
        {
            var user = _manager.FindById(HttpContext.Current.User.Identity.GetUserId());

            var viewModel = new Models.ViewModels.ProfileViewModel(user);

            IList<string> roles = _manager.GetRoles(user.Id);
            viewModel.RoleNames = roles;


            return viewModel;
        }


        public Task<IdentityResult> UpdateProfile(Models.RequestModels.UserProfileUpdateRequestModel model)
        {
            var user = _manager.FindById(HttpContext.Current.User.Identity.GetUserId());

            user.FirstName = model.FirstName;
            user.LastName = model.LastName;
            user.Email = model.Email;
            user.PhoneNumber = model.PhoneNumber;

            return _repository.UpdateProfile(user);
        }


        public bool UpdatePassword(Models.RequestModels.ChangePasswordRequestModel model)
        {
            var success = false;

            if (model.NewPassword != model.RetypePassword) return false;

            var user = _manager.FindById(HttpContext.Current.User.Identity.GetUserId());

            var verifyHashedPassword = new PasswordHasher().VerifyHashedPassword(user.PasswordHash, model.CurrentPassword);

            if (verifyHashedPassword == PasswordVerificationResult.Success)
            {
                success = _repository.UpdatePassword(model);
            }

            return success;
        }

    }
}