using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IProductoHelper
    {
        List<ProductoViewModel> GetProductos();
        ProductoViewModel GetProducto(int? id);
        ProductoViewModel Add(ProductoViewModel producto);
        ProductoViewModel Update(ProductoViewModel producto);
        void Delete(int id);
    }
}