using Infra.CrossCutting.Identity.Entities;
using Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.PostgreSQL.Mappings.IdentidadeSchema
{
    public class RoleEntityMapping : IEntityTypeConfiguration<Role>
    {
        public void Configure(EntityTypeBuilder<Role> entity)
        {
            entity.ToTable(TableConsts.IdentidadeSchema.Role, TableConsts.IdentidadeSchema.DefaultSchema);

            entity.HasIndex(e => e.NormalizedName)
                    .HasName("role_normalize_name_index")
                    .IsUnique();

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedNever();

            entity.Property(e => e.Name)
                .HasColumnName("name")
                .HasMaxLength(256);

            entity.Property(e => e.NormalizedName)
                .HasColumnName("normalized_name")
                .HasMaxLength(256);

            entity.Property(e => e.ConcurrencyStamp)
                .HasColumnName("concurrency_stamp");
        }
    }
}