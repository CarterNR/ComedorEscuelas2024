using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class RolService : IRolService
    {
        IUnidadDeTrabajo Unidad;
        public RolService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            this.Unidad = unidadDeTrabajo;
        }

        #region
        Rol Convertir(RolDTO rol)
        {
            return new Rol
            {
                IdRol = rol.IdRol,
                NombreRol = rol.NombreRol
            };
        }

        RolDTO Convertir(Rol rol)
        {
            return new RolDTO
            {
                IdRol = rol.IdRol,
                NombreRol = rol.NombreRol
            };
        }
        #endregion




        public bool Agregar(RolDTO rol)
        {
            Rol entity = Convertir(rol);
            Unidad.RolDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(RolDTO rol)
        {
            var entity = Convertir(rol);
            Unidad.RolDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(RolDTO rol)
        {
            var entity = Convertir(rol);
            Unidad.RolDAL.Remove(entity);
            return Unidad.Complete();
        }

        public RolDTO Obtener(int id)
        {
            return Convertir(Unidad.RolDAL.Get(id));
        }

        public List<RolDTO> Obtener()
        {
            List<RolDTO> list = new List<RolDTO>();
            var roles = Unidad.RolDAL.GetAll().ToList();

            foreach (var item in roles)
            {
                list.Add(Convertir(item));
            }
            return list;
        }
    }
}
