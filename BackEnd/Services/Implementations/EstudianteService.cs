using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;
using QRCoder;

namespace BackEnd.Services.Implementations
{
    public class EstudianteService : IEstudianteService
    {
        private readonly IUnidadDeTrabajo _unidad;
        private readonly SisComedorContext _context;

        public EstudianteService(IUnidadDeTrabajo unidadDeTrabajo, SisComedorContext context)
        {
            _unidad = unidadDeTrabajo;
            _context = context;
        }

        #region Convertidores

        private Estudiante Convertir(EstudianteDTO estudiante)
        {
            return new Estudiante
            {
                IdEstudiante = estudiante.IdEstudiante,
                Nombre = estudiante.Nombre,
                Cedula = estudiante.Cedula,
                IdEscuela = estudiante.IdEscuela,
                TiquetesRestantes = estudiante.TiquetesRestantes,
                IdUsuario = estudiante.IdUsuario,
                RutaQR = estudiante.RutaQR,
                NombreUsuario = estudiante.NombreUsuario,
                Clave = estudiante.Clave,
                FechaUltimoRebajo = estudiante.FechaUltimoRebajo
            };
        }

        private EstudianteDTO Convertir(Estudiante estudiante)
        {
            return new EstudianteDTO
            {
                IdEstudiante = estudiante.IdEstudiante,
                Nombre = estudiante.Nombre,
                Cedula = estudiante.Cedula,
                IdEscuela = estudiante.IdEscuela,
                TiquetesRestantes = estudiante.TiquetesRestantes,
                IdUsuario = estudiante.IdUsuario,
                RutaQR = estudiante.RutaQR,
                NombreUsuario = estudiante.NombreUsuario,
                Clave = estudiante.Clave,
                FechaUltimoRebajo = estudiante.FechaUltimoRebajo
            };
        }

        #endregion

        public bool Agregar(EstudianteDTO estudiante)
        {
            var entity = Convertir(estudiante);
            _unidad.EstudianteDAL.Add(entity);
            return _unidad.Complete();
        }

        public bool Editar(EstudianteDTO estudiante)
        {
            var entity = Convertir(estudiante);
            _unidad.EstudianteDAL.Update(entity);
            return _unidad.Complete();
        }

        public void Eliminar(int id)
        {
            var estudiante = _unidad.EstudianteDAL.Get(id);
            if (estudiante != null)
            {
                _unidad.EstudianteDAL.Remove(estudiante);
                _unidad.Complete();
            }
        }

        public EstudianteDTO Obtener(int id)
        {
            var entidad = _unidad.EstudianteDAL.Get(id);
            return entidad != null ? Convertir(entidad) : null;
        }

        public List<EstudianteDTO> Obtener()
        {
            return _unidad.EstudianteDAL.GetAll()
                .Select(e => Convertir(e))
                .ToList();
        }


        public EstudianteDTO ObtenerPorIdUsuario(int idUsuario)
        {
            var estudiante = _context.Estudiantes.FirstOrDefault(e => e.IdUsuario == idUsuario);

            if (estudiante == null) return null;

            return new EstudianteDTO
            {
                IdEstudiante = estudiante.IdEstudiante,
                Nombre = estudiante.Nombre,
                Cedula = estudiante.Cedula,
                IdEscuela = estudiante.IdEscuela,
                IdUsuario = estudiante.IdUsuario,
                TiquetesRestantes = estudiante.TiquetesRestantes,
                Clave = estudiante.Clave,
                NombreUsuario = estudiante.NombreUsuario,
                RutaQR = estudiante.RutaQR,
                FechaUltimoRebajo = estudiante.FechaUltimoRebajo
            };
        }




        private Estudiante ConvertirAEntidad(EstudianteDTO estudianteDTO)
        {
            // Aquí implementas la lógica para convertir el DTO a la entidad Estudiante
            return new Estudiante
            {
                // Mapeo de las propiedades de EstudianteDTO a Estudiante
                IdEstudiante = estudianteDTO.IdEstudiante,
                Cedula = estudianteDTO.Cedula,
                Nombre = estudianteDTO.Nombre,
                RutaQR = estudianteDTO.RutaQR,
                FechaUltimoRebajo = estudianteDTO.FechaUltimoRebajo
            };
        }

    }
}
