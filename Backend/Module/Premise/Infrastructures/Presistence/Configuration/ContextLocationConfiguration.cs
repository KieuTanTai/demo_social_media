using Backend.Module.Premise.Models.Premise;

namespace Backend.Module.Premise.Infrastructures.Persistence.Configuration
{
    public sealed class ContextLocationConfiguration : IEntityTypeConfiguration<LocationModel>
    {
        public void Configure(EntityTypeBuilder<LocationModel> entity)
        {
            entity.toTable("location");

            entity.HasKey(location => location.LocationId);
            
            entity.Property(location => location.LocationId)
                  .HasColumnName("location_id")
                  .ValueGeneratedOnAdd();

            entity.Property(location => location.Address)
                  .HasColumnName("location_address")
                  .HasConversion<string>()
                  .IsRequired();

            entity.Property(location => location.CreatedAt)
                .HasColumnName("location_created_at")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAdd();

            entity.Property(location => location.UpdatedAt)
                .HasColumnName("location_updated_at")
                .HasColumnType("timestamp")
                .HasDefaultValueSql("CURRENT_TIMESTAMP")
                .ValueGeneratedOnAddOrUpdate();
        }
    }
}
