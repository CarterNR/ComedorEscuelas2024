using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class EstudianteService : IEstudianteService
    {
        IUnidadDeTrabajo Unidad;
        public EstudianteService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.Unidad = unidadDeTrabajo;
        }

        #region
        Escuela Convertir(EscuelaDTO escuela)
        {
            return new Escuela
            {
                IdEscuela = escuela.IdEscuela,
                NombreEscuela = escuela.NombreEscuela,
                Estado = escuela.Estado
            };
        }

        EscuelaDTO Convertir(Escuela escuela)
        {
            return new EscuelaDTO
            {
                IdEscuela = escuela.IdEscuela,
                NombreEscuela = escuela.NombreEscuela,
                Estado = escuela.Estado
            };
        }
        #endregion


        public bool Agregar(EstudianteDTO estudiante)
        {
            Estudiante entity = Convertir(estudiante);
            Unidad.EstudianteDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(EstudianteDTO estudiante)
        {
            var entity = Convertir(estudiante);
            Unidad.EstudianteDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(EstudianteDTO estudiante)
        {
            var entity = Convertir(estudiante);
            Unidad.EstudianteDAL.Remove(entity);
            return Unidad.Complete();
        }

        public EstudianteDTO Obtener(int id)
        {
            return Convertir(Unidad.EstudianteDAL.Get(id));
        }

        public List<EstudianteDTO> Obtener()
        {
            List<EstudianteDTO> list = new List<EstudianteDTO>();
            var estudiantes = Unidad.EstudianteDAL.GetAll().ToList();

            foreach (var item in estudiantes)
            {
                list.Add(Convertir(item));
            }
            return list;
        }

    }
}
