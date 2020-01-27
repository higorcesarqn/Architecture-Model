using Domain.AggregatesModel.PermissionAggregate;
using Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.PostgreSQL.Mappings.IdentidadeSchema
{
    public class PermissionEntityMapping : IEntityTypeConfiguration<Permission>
    {
        public void Configure(EntityTypeBuilder<Permission> entity)
        {
            entity.ToTable(TableConsts.IdentidadeSchema.Permission, TableConsts.IdentidadeSchema.DefaultSchema);

            entity.HasKey(k => k.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd()
                .IsRequired();

            entity.Property(p => p.Title)
                .HasColumnName("title")
                .HasMaxLength(256)
                .IsRequired(false);

            entity.Property(x => x.Name)
                .HasColumnName("name")
                .HasMaxLength(50)
                .IsRequired();
        }
    }
}
