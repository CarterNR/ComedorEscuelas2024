using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class TipoCedulaService : ITipoCedulaService
    {
        IUnidadDeTrabajo Unidad;
        public TipoCedulaService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.Unidad = unidadDeTrabajo;
        }

        #region
        TipoCedula Convertir(TipoCedulaDTO tipoCedula)
        {
            return new TipoCedula
            {
                IdTipoCedula = tipoCedula.IdTipoCedula,
                TipoCedula1 = tipoCedula.TipoCedula1
            };
        }

        TipoCedulaDTO Convertir(TipoCedula tipoCedula)
        {
            return new TipoCedulaDTO
            {
                IdTipoCedula = tipoCedula.IdTipoCedula,
                TipoCedula1 = tipoCedula.TipoCedula1
            };
        }
        #endregion




        public bool Agregar(TipoCedulaDTO tipoCedula)
        {
            TipoCedula entity = Convertir(tipoCedula);
            Unidad.TipoCedulaDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(TipoCedulaDTO tipoCedula)
        {
            var entity = Convertir(tipoCedula);
            Unidad.TipoCedulaDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(TipoCedulaDTO tipoCedula)
        {
            var entity = Convertir(tipoCedula);
            Unidad.TipoCedulaDAL.Remove(entity);
            return Unidad.Complete();
        }

        public TipoCedulaDTO Obtener(int id)
        {
            return Convertir(Unidad.TipoCedulaDAL.Get(id));
        }

        public List<TipoCedulaDTO> Obtener()
        {
            List<TipoCedulaDTO> list = new List<TipoCedulaDTO>();
            var tiposCedula = Unidad.TipoCedulaDAL.GetAll().ToList();

            foreach (var item in tiposCedula)
            {
                list.Add(Convertir(item));
            }
            return list;
        }
    }
}
