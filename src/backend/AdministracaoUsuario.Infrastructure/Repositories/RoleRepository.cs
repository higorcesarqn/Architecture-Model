using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.PagedList;
using Core.UnitOfWork;
using Domain.AggregatesModel.RoleAggregate;
using Domain.AggregatesModel.RoleAggregate.Dtos;
using Infra.CrossCutting.Identity.Entities;
using Infra.EntityFramework.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace AdministracaoUsuario.Infrastructure.Repositories
{
    public class RoleRepository<TContext> : Repository<Role>, IRepository<Role>, IRoleRepository
         where TContext : DbContext
    {
        public RoleRepository(TContext dbContext) : base(dbContext) { }


        public Task<IPagedList<RoleListDto>> GetPagedList(int pageIndex = 0, int pageSize = 20)
        {
            Expression<Func<Role, RoleListDto>> selector = x => new RoleListDto { Id = x.Id, Nome = x.Name };

            return GetPagedListAsync(selector: selector, pageIndex: pageIndex, pageSize: pageSize);
        }

        public Task<RoleDetailsDto> Get(Guid idGrupo)
        {
            Expression<Func<Role, bool>> predicate = p => p.Id == idGrupo;

            Expression<Func<Role, RoleDetailsDto>> selector = s => new RoleDetailsDto
            {
                Id = s.Id,
                Nome = s.Name,
                Permissoes = s.RoleClaims.Select(x => x.ClaimValue)
            };

            IIncludableQueryable<Role, object> include(IQueryable<Role> user) => user.Include(x => x.RoleClaims);

            return GetFirstOrDefaultAsync(selector: selector, predicate: predicate, include: include);
        }
    }
}
