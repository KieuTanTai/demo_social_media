using Backend.Module.Premise.Models.Business;
using Backend.Module.Premise.Models.Premise;
using Backend.Module.Premise.Models.Product;

namespace Backend.Module.Premise.Infrastructures.Persistence.Configuration
{
    public sealed class ContextProductBusinessTypeModelConfiguration : IEntityTypeConfiguration<ProductBusinessTypeModel>
    {
        public void Configure(EntityTypeBuilder<ProductBusinessTypeModel> entity)
        {
            entity.toTable("product_business_type");

            entity.HasKey(productBusinessType => new{
                productBusinessType.ProductId,
                productBusinessType.BusinessTypeId
            });
            
            entity.Property(productBusinessType => productBusinessType.ProductId)
                  .HasColumnName("product_id")
                  .ValueGeneratedOnAdd();

            entity.Property(productBusinessType => productBusinessType.BusinessTypeId)
                  .HasColumnName("product_business_type_id")
                  .HasMaxLength(50)
                  .IsRequired();

            entity.HasOne<WhitelistProductModel>()
                  .WithMany()
                  .HasForeignKey(productBusinessType => productBusinessType.ProductId)
                  .OnDelete(DeleteBehavior.Restrict);  

            entity.HasOne<BusinessTypeModel>()
                  .WithMany()
                  .HasForeignKey(productBusinessType => productBusinessType.BusinessTypeId)
                  .OnDelete(DeleteBehavior.Restrict);  

        }
    }
}
