using Infra.CrossCutting.Identity.Entities;
using Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.PostgreSQL.Mappings.IdentidadeSchema
{
    public class RoleClaimsEntityMapping : IEntityTypeConfiguration<RoleClaim>
    {
        public void Configure(EntityTypeBuilder<RoleClaim> entity)
        {
            entity.ToTable(TableConsts.IdentidadeSchema.RoleClaims, TableConsts.IdentidadeSchema.DefaultSchema);

            entity.HasIndex(e => e.RoleId)
                .HasName("role_claims_id_index");

            entity.HasOne(d => d.Role)
                    .WithMany(p => p.RoleClaims)
                    .HasForeignKey(d => d.RoleId);

            entity.Property(e => e.RoleId)
                .HasColumnName("role_id")
                .IsRequired();

            entity.Property(x => x.Id)
               .HasColumnName("id")
               .IsRequired();

            entity.Property(x => x.ClaimType)
               .HasColumnName("claim_type");

            entity.Property(x => x.ClaimValue)
               .HasColumnName("claim_value");
        }
    }
}