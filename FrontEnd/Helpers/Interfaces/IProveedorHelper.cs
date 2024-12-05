using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{

    public interface IProveedorHelper
    {
        List<ProveedorViewModel> GetProveedores();

        ProveedorViewModel GetProveedor(int? id);
        ProveedorViewModel Add(ProveedorViewModel proveedor);
        ProveedorViewModel Update(ProveedorViewModel proveedor);
        void Delete(int id);
    }
}