using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.AggregatesModel.PermissionAggregate.Dtos;
using Domain.SeedWork;

namespace Domain.AggregatesModel.PermissionAggregate
{
    public interface IPermissionRepository : IAggregateRootReposity<Permission>
    {
        Task<IEnumerable<PermissaoDto>> ListAll();
    }
}
