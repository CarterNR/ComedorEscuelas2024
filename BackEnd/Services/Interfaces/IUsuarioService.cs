using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IUsuarioService
    {
        bool Agregar(UsuarioDTO usuario);
        bool Editar(UsuarioDTO usuario);
        bool Eliminar(UsuarioDTO usuario);
        UsuarioDTO Obtener(int id);
        List<UsuarioDTO> Obtener();
    }
}
