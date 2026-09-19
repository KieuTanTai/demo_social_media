using Backend.Module.Premise.Models.Premise;

namespace Backend.Module.Premise.Infrastructures.Persistence.Configuration
{
    public sealed class ContextPremiseBusinessTypeConfiguration : IEntityTypeConfiguration<PremiseBusinessTypeModel>
    {
        public void Configure(EntityTypeBuilder<PremiseBusinessTypeModel> entity)
        {
            entity.toTable("premise_business_type");

            entity.HasKey(premiseBusinessType => new {
                premiseBusinessType.PremiseId,
                premiseBusinessType.BusinessTypeId,
            });
            
            entity.Property(premiseBusinessType => premiseBusinessType.PremiseId)
                  .HasColumnName("premise_id")
                  .IsRequired();

            entity.Property(premiseBusinessType => premiseBusinessType.BusinessTypeId)
                  .HasColumnName("business_type_id")
                  .IsRequired();

            entity.HasOne<PremiseModel>()
                .WithMany()
                .HasForeignKey(premiseBusinessType => premiseBusinessType.PremiseId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.HasOne<PremiseModel>()
                .WithMany()
                .HasForeignKey(premiseBusinessType => premiseBusinessType.BusinessTypeId)
                .OnDelete(DeleteBehavior.Restrict);    
        }
    }
}
