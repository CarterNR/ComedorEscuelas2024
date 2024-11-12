using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class ProveedorService : IProveedorService
    {
        IUnidadDeTrabajo Unidad;
        SisComedorContext context;
        public ProveedorService(IUnidadDeTrabajo unidadDeTrabajo, SisComedorContext context)
        {
            this.Unidad = unidadDeTrabajo;
            this.context = context;
        }


        #region
        Proveedore Convertir(ProveedorDTO proveedor)
        {
            return new Proveedore
            {
                IdProveedor = proveedor.IdProveedor,
                NombreProveedor = proveedor.NombreProveedor,
                Telefono = proveedor.Telefono,
                CorreoElectronico = proveedor.CorreoElectronico,
                Direccion = proveedor.Direccion,
                Estado = proveedor.Estado,
                IdEscuela = proveedor.IdEscuela

            };
        }

        ProveedorDTO Convertir(Proveedore proveedor)
        {
            return new ProveedorDTO
            {
                IdProveedor = proveedor.IdProveedor,
                NombreProveedor = proveedor.NombreProveedor,
                Telefono = proveedor.Telefono,
                CorreoElectronico = proveedor.CorreoElectronico,
                Direccion = proveedor.Direccion,
                Estado = proveedor.Estado,
                IdEscuela = proveedor.IdEscuela
            };
        }
        #endregion




        public bool Agregar(ProveedorDTO proveedor)
        {
            Proveedore entity = Convertir(proveedor);
            Unidad.ProveedorDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(ProveedorDTO proveedor)
        {
            var entity = Convertir(proveedor);
            Unidad.ProveedorDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(ProveedorDTO proveedor)
        {
            var entity = Convertir(proveedor);
            Unidad.ProveedorDAL.Remove(entity);
            return Unidad.Complete();
        }

        public ProveedorDTO Obtener(int id)
        {
            return Convertir(Unidad.ProveedorDAL.Get(id));
        }

        public List<ProveedorDTO> Obtener()
        {
            List<ProveedorDTO> list = new List<ProveedorDTO>();
            var proveedores = Unidad.ProveedorDAL.GetAll()
                .Join(
                    context.Escuelas,
                    proveedor => proveedor.IdEscuela,
                    escuela => escuela.IdEscuela,
                    (proveedor, escuela) => new
                    {
                        IdProveedor = proveedor.IdProveedor,
                        NombreProveedor = proveedor.NombreProveedor,
                        Telefono = proveedor.Telefono,
                        CorreoElectronico = proveedor.CorreoElectronico,
                        Direccion = proveedor.Direccion,
                        Estado = proveedor.Estado,
                        IdEscuela = proveedor.IdEscuela,
                        NombreEscuela = escuela.NombreEscuela // Asumiendo que existe esta propiedad
                    })
                .ToList();

            foreach (var item in proveedores)
            {
                var proveedorDTO = new ProveedorDTO
                {
                    IdProveedor = item.IdProveedor,
                    NombreProveedor = item.NombreProveedor,
                    Telefono = item.Telefono,
                    CorreoElectronico = item.CorreoElectronico,
                    Direccion = item.Direccion,
                    Estado = item.Estado,
                    IdEscuela = item.IdEscuela,
                    NombreEscuela = item.NombreEscuela // Necesitarás agregar esta propiedad a tu DTO
                };
                list.Add(proveedorDTO);
            }
            return list;
        }

        
    }
}
