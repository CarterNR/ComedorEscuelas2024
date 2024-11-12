using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IRolService
    {
        bool Agregar(RolDTO rol);
        bool Editar(RolDTO rol);
        bool Eliminar(RolDTO rol);
        RolDTO Obtener(int id);
        List<RolDTO> Obtener();
    }
}
