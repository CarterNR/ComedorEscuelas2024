using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IEstudianteService
    {
        bool Agregar(EstudianteDTO estudiante);
        bool Editar(EstudianteDTO estudiante);
        void Eliminar(int id);
        EstudianteDTO Obtener(int id);
        List<EstudianteDTO> Obtener();
        EstudianteDTO ObtenerPorIdUsuario(int idUsuario);



    }
}