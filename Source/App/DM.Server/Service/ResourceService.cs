using DM.AuthServer.Models;
using DM.AuthServer.Repository;
using DM.Service;
using DM.Service.Contacts;

namespace DM.AuthServer.Service
{
    public interface IResourceService :IBaseService<SecurityModels.Resource>
    {
    }


    public class ResourceService: BaseService<SecurityModels.Resource>, IResourceService
    {
        private readonly IResourceRepository _repository;

        public ResourceService(IResourceRepository repository) : base(repository)
        {
            _repository = repository;
        }
     

    }
}