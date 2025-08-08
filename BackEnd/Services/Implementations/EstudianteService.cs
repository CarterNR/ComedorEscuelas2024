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
        IUnidadDeTrabajo Unidad;
        SisComedorContext context;

        public EstudianteService(IUnidadDeTrabajo unidadDeTrabajo, SisComedorContext context)
        {
            this.Unidad = unidadDeTrabajo;
            this.context = context;
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
            Unidad.EstudianteDAL.Add(entity);
            return Unidad.Complete();
        }

       


        public bool Editar(EstudianteDtoo estudiante)
        {
            var entidadExistente = context.Estudiantes.FirstOrDefault(e => e.IdUsuario == estudiante.IdUsuario);

            if (entidadExistente == null)
                return false;

            // Solo actualizamos los campos permitidos
            entidadExistente.Nombre = estudiante.Nombre.ToUpper();
            entidadExistente.Cedula = estudiante.Cedula;
            entidadExistente.IdEscuela = estudiante.IdEscuela;
            entidadExistente.TiquetesRestantes = estudiante.TiquetesRestantes;
            entidadExistente.NombreUsuario = estudiante.NombreUsuario;
            entidadExistente.Clave = estudiante.Clave;

            Unidad.EstudianteDAL.Update(entidadExistente);
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
            var entidad = Unidad.EstudianteDAL.Get(id);
            return entidad != null ? Convertir(entidad) : null;
        }

        public List<EstudianteDTO> Obtener()
        {
            return Unidad.EstudianteDAL.GetAll()
                .Select(e => Convertir(e))
                .ToList();
        }


        public EstudianteDTO ObtenerPorIdUsuario(int idUsuario)
        {
            var estudiante = context.Estudiantes.FirstOrDefault(e => e.IdUsuario == idUsuario);

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

                Nombre = estudianteDTO .Nombre,


                RutaQR = estudianteDTO.RutaQR,
                FechaUltimoRebajo = estudianteDTO.FechaUltimoRebajo
            };
        }

    }
}
