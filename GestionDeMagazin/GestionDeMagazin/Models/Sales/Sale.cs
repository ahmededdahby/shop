namespace GestionDeMagazin.Models.Sales;

public class Sale
{
    public int SaleId { get; set; }
    public DateTime SaleDate { get; set; }
    public virtual List<SaleItem> SaleItems { get; set; }

    public decimal TotalAmount => SaleItems?.Sum(item => item.Quantity * item.UnitPrice) ?? 0;
}
