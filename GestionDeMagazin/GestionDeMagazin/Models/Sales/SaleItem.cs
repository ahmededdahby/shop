using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDeMagazin.Models.Sales;

public class SaleItem
{
    public int SaleItemId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    // Other sale item details

    public decimal Subtotal => Quantity * UnitPrice;
    [ForeignKey("Sale")]
    public int SaleId { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public virtual Sale Sale { get; set; }
    public virtual Product Product { get; set; }
}
