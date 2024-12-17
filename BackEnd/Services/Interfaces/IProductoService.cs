using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IProductoService
    {
        List<ProductoDTO> Obtener();
        ProductoDTO Obtener(int id);
        bool Agregar(ProductoDTO producto);
        bool Editar(ProductoDTO producto);
        bool Eliminar(ProductoDTO producto);
    }
}
