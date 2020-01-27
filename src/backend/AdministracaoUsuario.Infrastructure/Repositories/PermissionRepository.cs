using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Core.UnitOfWork;
using Domain.AggregatesModel.PermissionAggregate;
using Domain.AggregatesModel.PermissionAggregate.Dtos;
using Infra.EntityFramework.UnitOfWork;
using Microsoft.EntityFrameworkCore;

namespace AdministracaoUsuario.Infrastructure.Repositories
{
    public class PermissionRepository<TContext> : Repository<Permission>, IPermissionRepository, IRepository<Permission>
        where TContext : DbContext
    {
        public PermissionRepository(TContext context) : base(context) { }

        public async Task<IEnumerable<PermissaoDto>> ListAll()
        {
            Expression<Func<Permission, PermissaoDto>> selector = s
                => new PermissaoDto
                {
                    Descricao = s.Description,
                    Nome = s.Name,
                    Titulo = s.Title
                };

            return await GetAll(selector).ToListAsync();
        }
    }
}
