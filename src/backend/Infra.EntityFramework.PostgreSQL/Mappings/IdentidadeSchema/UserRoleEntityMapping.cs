using Infra.CrossCutting.Identity.Entities;
using Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.PostgreSQL.Mappings.IdentidadeSchema
{
    public class UserRoleEntityMapping : IEntityTypeConfiguration<UserRole>
    {
        public void Configure(EntityTypeBuilder<UserRole> entity)
        {
            entity.ToTable(TableConsts.IdentidadeSchema.UserRole, TableConsts.IdentidadeSchema.DefaultSchema);

            entity.HasKey(e => new { e.UserId, e.RoleId });

            entity.HasIndex(e => e.RoleId)
                .HasName("user_role_id_index");

            entity.HasOne(d => d.Role)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.RoleId);

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserRoles)
                .HasForeignKey(d => d.UserId);

            entity.Property(x => x.UserId)
                .HasColumnName("user_id");

            entity.Property(x => x.RoleId)
                .HasColumnName("role_id");
        }
    }
}