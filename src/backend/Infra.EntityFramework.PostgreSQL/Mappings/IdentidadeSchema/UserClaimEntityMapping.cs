using Infra.CrossCutting.Identity.Entities;
using Infra.EntityFramework.PostgreSQL.Constants;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infra.EntityFramework.PostgreSQL.Mappings.IdentidadeSchema
{
    public class UserClaimEntityMapping : IEntityTypeConfiguration<UserClaim>
    {
        public void Configure(EntityTypeBuilder<UserClaim> entity)
        {
            entity.ToTable(TableConsts.IdentidadeSchema.UserClaim, TableConsts.IdentidadeSchema.DefaultSchema);

            entity.HasIndex(e => e.UserId)
                .HasName("user_claim_id_index");

            entity.HasOne(d => d.User)
                .WithMany(p => p.UserClaims)
                .HasForeignKey(d => d.UserId);

            entity.Property(e => e.Id)
                .HasColumnName("id")
                .IsRequired();

            entity.Property(e => e.UserId)
                .HasColumnName("user_id")
                .IsRequired();

            entity.Property(x => x.ClaimType)
               .HasColumnName("claim_type");

            entity.Property(x => x.ClaimValue)
               .HasColumnName("claim_value");
        }
    }
}