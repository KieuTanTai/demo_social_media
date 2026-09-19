using Backend.Module.Premise.Models.Business;

namespace Backend.Module.Premise.Models.Product
{
    public class WhitelistProductModel
{
    public Guid ProductId { get; set; }

    public string Name { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime UpdatedDate { get; set; }


    // Navigation
    public ICollection<ProductBusinessTypeModel> ProductBusinessTypes { get; set; }
        = new List<ProductBusinessTypeModel>();
}
}