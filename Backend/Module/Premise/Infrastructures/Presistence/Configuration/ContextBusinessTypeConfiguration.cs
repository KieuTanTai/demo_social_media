using Backend.Module.Premise.Models.Business;
using Backend.Module.Premise.Models.Premise;
using Backend.Module.Premise.Models.Product;


// -------------------------------------------------------------------------- chua Xong ---------------------------------
namespace Backend.Module.Premise.Infrastructures.Persistence.Configuration
{
    public sealed class ContextBusinessTypeConfiguration : IEntityTypeConfiguration<BusinessTypeModel>
    {
        public void Configure(EntityTypeBuilder<BusinessTypeModel> entity)
        {
            entity.toTable("business_type");

            entity.HasKey(businessType => businessType.BusinessTypeId);
            
            entity.Property(businessType => businessType.BusinessTypeId)
                  .HasColumnName("business_type_id")
                  .ValueGeneratedOnAdd();

            entity.Property(businessType => businessType.Name)
                  .HasColumnName("business_type_name")
                  .HasMaxLength(50)
                  .IsRequired();
                
            entity.Property(businessType => businessType.Description)
                  .HasColumnName("business_type_description")
                   .HasMaxLength(255);

            entity.Property(businessType => businessType.IsActive)
                  .HasColumnName("business_type_is_active")
                  .HasDefaultValue(true)
                  .IsRequired();

            entity.Property(businessType => businessType.CreatedAt)
                .HasColumnName("business_type_created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.Property(businessType => businessType.UpdatedAt)
                .HasColumnName("business_type_updated_at")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            entity.HasMany(businessType => businessType.Types).WithMany().UsingEntity<PremiseBusinessTypeModel>(
                right => right.HasOne<Premise>().WithMany().HasForeignKey(premise => premise.PremiseId)
                    .OnDelete(DeleteBehavior.Restrict),
                left => left.HasOne<BusinessTypeModel>().WithMany().HasForeignKey(businessType => businessType.BusinessTypeId)
                    .OnDelete(DeleteBehavior.Restrict),
                join => {
                    join.ToTable("premise_business_type");
                    join.HasKey(premiseBusinessType => new
                    {
                        premiseBusinessType.businessTypeId,
                        premiseBusinessType.PremiseId
                    });

                    join.Property(premiseBusinessType => premiseBusinessType.PremiseId)
                        .HasColumnName("premise_id");

                    join.Property(premiseBusinessType => premiseBusinessType.BusinessTypeId)
                        .HasColumnName("business_type_id");

                });


            // ------------------------------------- chua xong--------------------------------------------
            entity.HasMany(businessType => businessType.ProductBusinessTypes).WithMany().UsingEntity<ProductBusinessTypeModel>(
                right => right.HasOne<WhitelistProductModel>().WithMany().HasForeignKey(productBusinessType => productBusinessType.ProductId)
                    .OnDelete(DeleteBehavior.Restrict),
                left => left.HasOne<BusinessTypeModel>().WithMany().HasForeignKey(productBusinessType => productBusinessType.BusinessTypeId)
                    .OnDelete(DeleteBehavior.Restrict),
                join => {
                    join.ToTable("product_business_type");
                    join.HasKey(productBusinessType => new
                    {
                        productBusinessType.ProductId,
                        productBusinessType.BusinessTypeId
                    });

                    join.Property(productBusinessType => productBusinessType.ProductId)
                        .HasColumnName("whitelist_product_id");

                    join.Property(productBusinessType => productBusinessType.BusinessTypeId)
                        .HasColumnName("business_type_id");
                }
            );

        }
    }
}
