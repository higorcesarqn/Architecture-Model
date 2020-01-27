using System;
using System.Threading.Tasks;
using Core.PagedList;
using Domain.AggregatesModel.UserAggregate.Dtos;
using Domain.SeedWork;
using Infra.CrossCutting.Identity.Entities;

namespace Domain.AggregatesModel.UserAggregate
{
    public interface IUserRepository : IAggregateRootReposity<User>
    {
        Task<IPagedList<UserListDto>> GetPagedList(int pageIndex = 0, int pageSize = 20);
        Task<UserDetailsDto> GetDetails(Guid idUsuario);
    }
}