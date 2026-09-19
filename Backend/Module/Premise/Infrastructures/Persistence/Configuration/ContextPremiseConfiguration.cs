using Premise.Models.Premise;

namespace Premise.Infrastructures.Presistence.Configuration
{
    public sealed class ContextPremiseConfiguration : IEntityTypeConfiguration<PremiseModel>
    {
        public void Configure(EntityTypeBuilder<PremiseModel> entity)
        {
            entity.toTable("premise");

            entity.HasKey(premise => premise.PremiseId);
            
            entity.Property(premise => premise.PremiseId)
                  .HasColumnName("premise_id")
                  .ValueGeneratedOnAdd();

            entity.Property(premise => premise.Name)
                  .HasColumnName("premise_name")
                  .HasMaxLength(50)
                  .IsRequired();

            entity.Property(premise => premise.Status)
                  .HasColumnName("premise_status")
                  .HasConversion<string>()
                  .HasMaxLength(50);

            entity.Property(premise => premise.Position)
                  .HasColumnName("premise_position")
                  .HasConversion<int>()
                  .IsRequired();
                  
            entity.Property(premise => premise.Floor)
                  .HasColumnName("premise_floor")
                  .HasConversion<int>()
                  .IsRequired();

            entity.Property(premise => premise.Area)
                  .HasColumnName("premise_area")
                  .HasMaxLength(10)
                  .IsRequired();

            entity.Property(premise => premise.Description)
                  .HasColumnName("premise_description")
                  .HasMaxLength(255)
                  .IsRequired();

            entity.Property(premise => premise.CreatedAt)
                .HasColumnName("premise_created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.Property(premise => premise.UpdatedAt)
                .HasColumnName("premise_updated_at")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
