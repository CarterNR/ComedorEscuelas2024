using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        IUnidadDeTrabajo Unidad;
        SisComedorContext context;
        public UsuarioService(IUnidadDeTrabajo unidadDeTrabajo, SisComedorContext context)
        {
            this.Unidad = unidadDeTrabajo;
            this.context = context;
        }


        #region
        Usuario Convertir(UsuarioDTO usuario)
        {
            return new Usuario
            {
                IdUsuario = usuario.IdUsuario,
                NombreCompleto = usuario.NombreCompleto,
                IdTipoCedula = usuario.IdTipoCedula,
                Cedula = usuario.Cedula,
                Telefono = usuario.Telefono,
                Direccion = usuario.Direccion,
                CorreoElectronico = usuario.CorreoElectronico,
                Clave = usuario.Clave,
                Estado = usuario.Estado,
                IdEscuela = usuario.IdEscuela,
                IdRol = usuario.IdRol
            };
        }

        UsuarioDTO Convertir(Usuario usuario)
        {
            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                NombreCompleto = usuario.NombreCompleto,
                IdTipoCedula = usuario.IdTipoCedula,
                Cedula = usuario.Cedula,
                Telefono = usuario.Telefono,
                Direccion = usuario.Direccion,
                CorreoElectronico = usuario.CorreoElectronico,
                Clave = usuario.Clave,
                Estado = usuario.Estado,
                IdEscuela = usuario.IdEscuela,
                IdRol = usuario.IdRol
            };
        }
        #endregion





        public bool Agregar(UsuarioDTO usuario)
        {
            Usuario entity = Convertir(usuario);
            Unidad.UsuarioDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(UsuarioDTO usuario)
        {
            var entity = Convertir(usuario);
            Unidad.UsuarioDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(UsuarioDTO usuario)
        {
            var entity = Convertir(usuario);
            Unidad.UsuarioDAL.Remove(entity);
            return Unidad.Complete();
        }

        public UsuarioDTO Obtener(int id)
        {
            return Convertir(Unidad.UsuarioDAL.Get(id));
        }

        public List<UsuarioDTO> Obtener()
        {
            List<UsuarioDTO> list = new List<UsuarioDTO>();
            var usuarios = Unidad.UsuarioDAL.GetAll()
                .Join(
                    context.Roles,
                    usuario => usuario.IdRol,
                    rol => rol.IdRol,
                    (usuario, rol) => new
                    {
                        Usuario = usuario,
                        Rol = rol
                    })
                .Join(
                    context.TipoCedulas,
                    joined => joined.Usuario.IdTipoCedula,  // Aquí está la corrección
                    tipoCedula => tipoCedula.IdTipoCedula,
                    (joined, tipoCedula) => new
                    {
                        Usuario = joined.Usuario,
                        Rol = joined.Rol,
                        TipoCedula = tipoCedula
                    })
                .Join(
                    context.Escuelas,
                    joined => joined.Usuario.IdEscuela,
                    escuela => escuela.IdEscuela,
                    (joined, escuela) => new
                    {
                        IdUsuario = joined.Usuario.IdUsuario,
                        NombreCompleto = joined.Usuario.NombreCompleto,
                        IdTipoCedula = joined.Usuario.IdTipoCedula,
                        TipoCedula1 = joined.TipoCedula.TipoCedula1,
                        Cedula = joined.Usuario.Cedula,
                        Telefono = joined.Usuario.Telefono,
                        Direccion = joined.Usuario.Direccion,
                        CorreoElectronico = joined.Usuario.CorreoElectronico,
                        Clave = joined.Usuario.Clave,
                        Estado = joined.Usuario.Estado,
                        IdEscuela = joined.Usuario.IdEscuela,
                        NombreEscuela = escuela.NombreEscuela,
                        IdRol = joined.Usuario.IdRol,
                        NombreRol = joined.Rol.NombreRol
                    })
                .ToList();

            foreach (var item in usuarios)
            {
                var usuarioDTO = new UsuarioDTO
                {
                    IdUsuario = item.IdUsuario,
                    NombreCompleto = item.NombreCompleto,
                    IdTipoCedula = item.IdTipoCedula,
                    TipoCedula1 = item.TipoCedula1,
                    Cedula = item.Cedula,
                    Telefono = item.Telefono,
                    Direccion = item.Direccion,
                    CorreoElectronico = item.CorreoElectronico,
                    Clave = item.Clave,
                    Estado = item.Estado,
                    IdEscuela = item.IdEscuela,
                    NombreEscuela = item.NombreEscuela,
                    IdRol = item.IdRol,
                    NombreRol = item.NombreRol,
                };
                list.Add(usuarioDTO);
            }
            return list;
        }
    }
}
