using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace GestionDeMagazin.Models.Purchases;

public class PurchaseItem
{
    [Key]
    public int PurchaseItemId { get; set; }
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }

    [ForeignKey("Purchase")]
    public int PurchaseId { get; set; }
    [ForeignKey("Product")]
    public int ProductId { get; set; }

    public virtual Purchase Purchase { get; set; }

    public virtual Product Product { get; set; }
}
