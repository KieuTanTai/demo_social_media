using Premise.Models.Intermediary;
using Premise.Models.Premise;

namespace Premise.Infrastructures.Presistence.Configuration
{
    public sealed class ContextContractAndPremiseIntermediaryConfiguration : IEntityTypeConfiguration<RentedPremiseModel>
    {
        public void Configure(EntityTypeBuilder<RentedPremiseModel> entity)
        {
            entity.toTable("rented_premise");

            entity.HasKey(rentedPremise => new {
                rentedPremise.ContractId,
                rentedPremise.PremiseId,
            });
            
            entity.Property(rentedPremise => rentedPremise.ContractId)
                  .HasColumnName("rentedPremise_id")
                  .ValueGeneratedOnAdd();

            entity.Property(rentedPremise => rentedPremise.PremiseId)
                  .HasColumnName("rentedPremise_address")
                  .HasConversion<string>()
                  .IsRequired();

            entity.HasOne<PremiseModel>()
                  .WithMany()
                  .HasForeignKey(rentedPremise => rentedPremise.PremiseId)
                  .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
