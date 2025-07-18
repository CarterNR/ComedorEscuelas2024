using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IEstudianteHelper
    {
        List<EstudianteViewModel> GetEstudiantes();

        EstudianteViewModel GetEstudiante(int? id);
        EstudianteViewModel Add(EstudianteViewModel estudiante);
        EstudianteViewModel Update(EstudianteViewModel estudiante);
        void Delete(int id);

        EstudianteViewModel GetEstudiantePorUsuario(int idUsuario);
        EstudianteViewModel GetEstudiantePorCedula(string cedula);


    }
}