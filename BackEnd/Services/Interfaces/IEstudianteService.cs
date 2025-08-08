using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IEstudianteService
    {
        bool Agregar(EstudianteDTO estudiante);
        bool Editar(EstudianteDtoo estudiante);
        bool Eliminar(EstudianteDTO estudiante);
        EstudianteDTO Obtener(int id);
        List<EstudianteDTO> Obtener();
        EstudianteDTO ObtenerPorIdUsuario(int idUsuario);





    }
}