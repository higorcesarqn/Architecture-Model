using System;
using Infra.CrossCutting.Identity.Entities;
using Infra.EntityFramework.Extensions;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Infra.EntityFramework.PostgreSQL
{
    public class PostgreSqlContext : IdentityDbContext<User, Role, Guid,
        UserClaim, UserRole, UserLogin, RoleClaim, UserToken>
    {
        public PostgreSqlContext(DbContextOptions<PostgreSqlContext> options) :
         base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
               .HasPostgresExtension("pgcrypto");

            modelBuilder.ApplyAllConfigurationsFromCurrentAssembly(typeof(Mappings.IdentidadeSchema.PermissionEntityMapping).Assembly);
            modelBuilder.ApplySnakeCaseInColumnName();

        }
    }
}
