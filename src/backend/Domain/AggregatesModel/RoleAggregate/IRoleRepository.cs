using System;
using System.Threading.Tasks;
using Core.PagedList;
using Domain.AggregatesModel.RoleAggregate.Dtos;
using Domain.SeedWork;
using Infra.CrossCutting.Identity.Entities;

namespace Domain.AggregatesModel.RoleAggregate
{
    public interface IRoleRepository : IAggregateRootReposity<Role>
    {
        Task<IPagedList<RoleListDto>> GetPagedList(int pageIndex = 0, int pageSize = 20);

        Task<RoleDetailsDto> Get(Guid idGrupo);
    }
}
