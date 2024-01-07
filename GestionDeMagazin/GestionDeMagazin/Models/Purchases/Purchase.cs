namespace GestionDeMagazin.Models.Purchases;

public class Purchase
{
    public int PurchaseId { get; set; }
    public DateTime PurchaseDate { get; set; }
    public virtual List<PurchaseItem> PurchaseItems { get; set; }
}
