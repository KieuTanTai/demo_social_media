namespace Backend.Module.Contract.Models.Invoice
{
    public class InvoiceDetailModel
{
    public int InvoiceDetailId { get; set; }

    public Guid InvoiceId { get; set; }

    public Guid PremiseId { get; set; }

    public decimal RentalPrice { get; set; }

    public decimal ElectricityFee { get; set; }

    public decimal WaterFee { get; set; }

    public decimal GarbageFee { get; set; }

    public decimal TotalAmount { get; set; }


    // Navigation
    public MonthlyInvoiceModel Invoice { get; set; } = null!;}
}