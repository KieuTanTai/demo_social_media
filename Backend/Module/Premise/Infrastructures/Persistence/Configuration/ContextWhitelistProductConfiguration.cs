using Premise.Models.Business;
using Premise.Models.Product;


// --------------------------------------------- chua xong ------------------------------------------------------
namespace Premise.Infrastructures.Presistence.Configuration
{
    public sealed class ContextWhitelistProductConfiguration : IEntityTypeConfiguration<WhitelistProductModel>
    {
        public void Configure(EntityTypeBuilder<WhitelistProductModel> entity)
        {
            entity.toTable("whitelist_product");

            entity.HasKey(whitelistProduct => whitelistProduct.ProductId);
            
            entity.Property(whitelistProduct => whitelistProduct.ProductId)
                  .HasColumnName("whitelist_product_id")
                  .ValueGeneratedOnAdd();

            entity.Property(whitelistProduct => whitelistProduct.Name)
                  .HasColumnName("whitelist_product_name")
                  .HasMaxLength(50)
                  .IsRequired();
                
            entity.Property(whitelistProduct => whitelistProduct.Description)
                  .HasColumnName("whitelist_product_description")
                   .HasMaxLength(255);

            entity.Property(whitelistProduct => whitelistProduct.CreatedAt)
                .HasColumnName("whitelist_product_created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.Property(whitelistProduct => whitelistProduct.UpdatedAt)
                .HasColumnName("whitelist_product_updated_at")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();

            entity.HasMany(whitelistProduct => whitelistProduct.ProductBusinessTypes).WithMany().UsingEntity<ProductBusinessTypeModel>(
                right => right.HasOne<BusinessTypeModel>().WithMany().HasForeignKey(productBusinessType => productBusinessType.BusinessTypeId)
                    .OnDelete(DeleteBehavior.Restrict),
                left => left.HasOne<WhitelistProductModel>().WithMany().HasForeignKey(productBusinessType => productBusinessType.ProductId)
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
