using Backend.Module.Premise.Models.Business;

namespace Backend.Module.Premise.Models.Product
{
    public class ProductBusinessTypeModel
{
    public Guid ProductId { get; set; }

    public Guid BusinessTypeId { get; set; }


    // Navigation
    public WhitelistProductModel Product { get; set; } = null!;

    public BusinessTypeModel BusinessType { get; set; } = null!;
}
}