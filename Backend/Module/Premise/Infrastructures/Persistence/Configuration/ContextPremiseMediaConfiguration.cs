using Premise.Models.Premise;

namespace Premise.Infrastructures.Presistence.Configuration
{
    public sealed class ContextPremiseMediaConfiguration : IEntityTypeConfiguration<PremiseMediaModel>
    {
        public void Configure(EntityTypeBuilder<PremiseMediaModel> entity)
        {
            entity.toTable("premise_media");

            entity.HasKey(premiseMedia => premiseMedia.PremiseMediaId);
            
            entity.Property(premiseMedia => premiseMedia.PremiseMediaId)
                  .HasColumnName("premise_media_id")
                  .ValueGeneratedOnAdd();

            entity.Property(premiseMedia => premiseMedia.Image)
                  .HasColumnName("premise_media_image")
                  .HasConversion<string>()
                  .IsRequired();

            entity.Property(premiseMedia => premiseMedia.PremiseId)
                  .HasColumnName("premise_id")
                  .IsRequired();

            entity.HasOne<PremiseModel>()
                .WithMany()
                .HasForeignKey(premiseMedia => premiseMedia.PremiseId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
