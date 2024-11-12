using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IProductoService
    {
        bool Agregar(ProductoDTO producto);
        bool Editar(ProductoDTO producto);
        bool Eliminar(ProductoDTO producto);
        ProductoDTO Obtener(int id);
        List<ProductoDTO> Obtener();
    }
}
