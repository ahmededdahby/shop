using GestionDeMagazin.Models.Sales;

namespace GestionDeMagazin.Services.IServices;

public interface ISaleService
{

    Task<IEnumerable<Sale>> GetSales();
    Task<Sale> GetSale(int id);
    Task<Sale> AddSale(Sale sale);
}
