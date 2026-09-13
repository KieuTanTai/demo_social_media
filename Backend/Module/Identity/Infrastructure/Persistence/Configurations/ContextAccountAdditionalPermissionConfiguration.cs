using Identity.Models.Account;
using Identity.Models.Permission;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Identity.Infrastructure.Persistence.Configurations
{
    public sealed class ContextAccountAdditionalPermissionConfiguration
        : IEntityTypeConfiguration<AccountAdditionalPermissionModel>
    {
        public void Configure(EntityTypeBuilder<AccountAdditionalPermissionModel> entity)
        {
            entity.ToTable("account_additional_permission");

            entity.HasKey(accountPermission => new
            {
                accountPermission.AccountId,
                accountPermission.PermissionId
            });

            entity.Property(accountPermission => accountPermission.AccountId)
                .HasColumnName("account_id")
                .IsRequired();

            entity.Property(accountPermission => accountPermission.PermissionId)
                .HasColumnName("permission_id")
                .IsRequired();

            entity.Property(accountPermission => accountPermission.AssignedAt)
                .HasColumnName("assigned_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.HasIndex(accountPermission => accountPermission.PermissionId)
                .HasDatabaseName("idx_account_additional_permission_permission_id");

            entity.HasOne<AccountModel>()
                .WithMany()
                .HasForeignKey(accountPermission => accountPermission.AccountId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<PermissionModel>()
                .WithMany()
                .HasForeignKey(accountPermission => accountPermission.PermissionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
