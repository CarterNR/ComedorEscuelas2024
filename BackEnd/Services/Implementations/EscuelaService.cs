using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class EscuelaService : IEscuelaService
    {
        IUnidadDeTrabajo Unidad;
        SisComedorContext context;

        public EscuelaService(IUnidadDeTrabajo unidadDeTrabajo, SisComedorContext context)
        {
            this.Unidad = unidadDeTrabajo;
            this.context = context;
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


        public bool Agregar(EscuelaDTO escuela)
        {
            Escuela entity = Convertir(escuela);
            Unidad.EscuelaDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(EscuelaDTO escuela)
        {
            var entity = Convertir(escuela);
            Unidad.EscuelaDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(EscuelaDTO escuela)
        {
            var entity = Convertir(escuela);
            Unidad.EscuelaDAL.Remove(entity);
            return Unidad.Complete();
        }

        public EscuelaDTO Obtener(int id)
        {
            return Convertir(Unidad.EscuelaDAL.Get(id));
        }

        public List<EscuelaDTO> Obtener()
        {
            List<EscuelaDTO> list = new List<EscuelaDTO>();
            var escuelas = Unidad.EscuelaDAL.GetAll().ToList();

            foreach (var item in escuelas)
            {
                list.Add(Convertir(item));
            }
            return list;
        }

    }
}
