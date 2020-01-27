using Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.Extensions
{
    public static class MapHelper
    {
        public static void ConfigureCreateAtAndUpdteAt<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : Entity
        {
            entity.Property(e => e.CreatedAt)
                .HasColumnName("data_inclusao")
                .HasDefaultValueSql("Now()");

            entity.Property(e => e.UpdatedAt)
                .IsRequired(false)
                .HasColumnName("data_atualizacao");
        }

        public static void IgnoreInclusao<TEntity>(this EntityTypeBuilder<TEntity> entity) where TEntity : Entity
        {
            entity.Ignore(e => e.CreatedAt);
            entity.Ignore(e => e.UpdatedAt);
        }

        public static void ConfigureKey<TEntity>(this EntityTypeBuilder<TEntity> entity, string columnsName) where TEntity : Entity
        {
            entity.HasKey(x => x.Id);
            entity.Property(x => x.Id)
                .HasDefaultValueSql("gen_random_uuid()");

            entity.Property(e => e.Id)
               .HasColumnName(columnsName);
        }
    }
}
