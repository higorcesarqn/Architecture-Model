using Infra.CrossCutting.Identity.Entities;
using Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.PostgreSQL.Mappings.IdentidadeSchema
{
    public class UserEntityMapping : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> entity)
        {
            entity.ToTable(TableConsts.IdentidadeSchema.User, TableConsts.IdentidadeSchema.DefaultSchema);

            entity.HasKey(x => x.Id);

            entity.HasIndex(x => x.NormalizedUserName)
                .HasName("user_normalized_name_index")
                .IsUnique();

            entity.HasIndex(e => e.NormalizedEmail)
                    .HasName("email_index");

            entity.Property(x => x.Id)
                .HasColumnName("id")
                .IsRequired()
                .ValueGeneratedNever();

            entity.Property(x => x.AccessFailedCount)
                .HasColumnName("access_failed_count")
                .IsRequired();

            entity.Property(x => x.ConcurrencyStamp)
                .IsConcurrencyToken()
                .HasColumnName("concurrency_stamp");

            entity.Property(x => x.Email)
                .HasMaxLength(256)
                .HasColumnName("email");

            entity.Property(x => x.EmailConfirmed)
                .HasColumnName("email_confirmed")
                .IsRequired();

            entity.Property(x => x.LockoutEnabled)
                .HasColumnName("lockout_enabled")
                .IsRequired();

            entity.Property(x => x.LockoutEnd)
                .HasColumnName("lockout_end")
                .HasColumnType("timestamp with time zone");

            entity.Property(x => x.NormalizedEmail)
                .HasMaxLength(256)
                .HasColumnName("normalized_email");

            entity.Property(x => x.NormalizedUserName)
                .HasMaxLength(30)
                .HasColumnName("normalized_username");

            entity.Property(x => x.PasswordHash)
                .HasColumnName("password_hash");

            entity.Property(x => x.PhoneNumber)
                .HasColumnName("phone_number");

            entity.Property(x => x.PhoneNumberConfirmed)
                .HasColumnName("phone_number_confirmed")
                .IsRequired();

            entity.Property(x => x.SecurityStamp)
                .HasColumnName("security_stamp");

            entity.Property(x => x.TwoFactorEnabled)
                .HasColumnName("two_factor_enabled")
                .IsRequired();

            entity.Property(x => x.UserName)
                .HasMaxLength(30)
                .HasColumnName("username");

            entity.Property(x => x.Name)
                .HasMaxLength(60)
                .HasColumnName("name");
        }
    }
}