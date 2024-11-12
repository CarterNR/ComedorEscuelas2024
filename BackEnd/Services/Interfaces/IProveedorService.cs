using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IProveedorService
    {
        bool Agregar(ProveedorDTO proveedor);
        bool Editar(ProveedorDTO proveedor);
        bool Eliminar(ProveedorDTO proveedor);
        ProveedorDTO Obtener(int id);
        List<ProveedorDTO> Obtener();

    }
}
