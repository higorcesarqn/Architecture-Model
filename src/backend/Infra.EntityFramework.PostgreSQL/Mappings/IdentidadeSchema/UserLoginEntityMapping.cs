using Infra.CrossCutting.Identity.Entities;
using Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.PostgreSQL.Mappings.IdentidadeSchema
{
    public class UserLoginEntityMapping : IEntityTypeConfiguration<UserLogin>
    {
        public void Configure(EntityTypeBuilder<UserLogin> entity)
        {
            entity.ToTable(TableConsts.IdentidadeSchema.UserLogin, TableConsts.IdentidadeSchema.DefaultSchema);

            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.HasIndex(e => e.UserId)
                .HasName("user_login_id_index");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserLogins)
                .HasForeignKey(d => d.UserId);

            entity.Property(x => x.LoginProvider)
                .HasColumnName("login_provider")
                .IsRequired();

            entity.Property(x => x.ProviderKey)
                .HasColumnName("provider_key")
                .IsRequired();

            entity.Property(x => x.ProviderDisplayName)
                .HasColumnName("provider_display_name");

            entity.Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();
        }
    }
}