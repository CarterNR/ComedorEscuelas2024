using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IEscuelaService
    {
        bool Agregar(EscuelaDTO escuela);
        bool Editar(EscuelaDTO escuela);
        bool Eliminar(EscuelaDTO escuela);
        EscuelaDTO Obtener(int id);
        List<EscuelaDTO> Obtener();
    }
}