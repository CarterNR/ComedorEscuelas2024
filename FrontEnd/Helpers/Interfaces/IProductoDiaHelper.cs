using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IProductoDiaHelper
    {
        List<ProductoDiaViewModel> GetProductosDia();

        ProductoDiaViewModel GetProductoDia(int? id);
        ProductoDiaViewModel Add(ProductoDiaViewModel productodia);
        ProductoDiaViewModel Update(ProductoDiaViewModel productodia);
        void Delete(int id);
    }
}