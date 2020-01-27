using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.PagedList;
using Core.UnitOfWork;
using Domain.AggregatesModel.UserAggregate;
using Domain.AggregatesModel.UserAggregate.Dtos;
using Infra.CrossCutting.Identity.Entities;
using Infra.EntityFramework.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AdministracaoUsuario.Infrastructure.Repositories
{
    public class UserRepository<TContext> : Repository<User>, IRepository<User>, IUserRepository
         where TContext : DbContext
    {
        public UserRepository(TContext dbContext) : base(dbContext) { }

        public Task<IPagedList<UserListDto>> GetPagedList(int pageIndex = 0, int pageSize = 20)
        {
            Expression<Func<User, UserListDto>> selector = user => new UserListDto
            {
                Id = user.Id,
                Nome = user.Name,
                Login = user.UserName,
                Email = user.Email,
                Telefone = user.UserName,
                DataFimDoBloqueio = user.LockoutEnd,
                ContadorErroSenha = user.AccessFailedCount
            };

            return GetPagedListAsync(selector: selector, pageIndex: pageIndex, pageSize: pageSize);
        }

        public Task<UserDetailsDto> GetDetails(Guid idUsuario)
        {
            Expression<Func<User, bool>> predicate = p => p.Id == idUsuario;

            Expression<Func<User, UserDetailsDto>> selector = user => new UserDetailsDto
            {
                Id = user.Id,
                Nome = user.Name,
                Login = user.UserName,
                Email = user.Email,
                ContadorErroSenha = user.AccessFailedCount,
                DataFimDoBloqueio = user.LockoutEnd,
                Telefone = user.PhoneNumber,
                Grupos = user.UserRoles.Select(x => x.Role.Name),
            };

            IIncludableQueryable<User, object> include(IQueryable<User> user) => user.Include(x => x.UserRoles).ThenInclude(t => t.Role);

            return GetFirstOrDefaultAsync(selector: selector, predicate: predicate, disableTracking: true, include: include);
        }
    }
}
