using QRCoder;
using System.Drawing;
using System.Drawing.Imaging;
using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using System.Runtime.ConstrainedExecution;
using Microsoft.EntityFrameworkCore;
using BCrypt.Net;

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
        private Usuario Convertir(UsuarioDTO usuario)
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
                IdRol = usuario.IdRol,
                NombreUsuario = usuario.NombreUsuario
            };
        }

        private UsuarioDTO Convertir(Usuario usuario)
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
                IdRol = usuario.IdRol,
                NombreUsuario = usuario.NombreUsuario

            };
        }
        #endregion


        public UsuarioDTO ObtenerPorCredenciales(string nombreUsuario, string clave)
        {
            var usuario = context.Usuarios.FirstOrDefault(u =>
                u.NombreUsuario == nombreUsuario);

            if (usuario == null)
                return null;

            // Verifica la clave hasheada
            bool claveValida = BCrypt.Net.BCrypt.Verify(clave, usuario.Clave);
            if (!claveValida)
                return null;

            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                NombreUsuario = usuario.NombreUsuario,
                Clave = usuario.Clave,
                IdRol = usuario.IdRol,
                NombreCompleto = usuario.NombreCompleto,
                Estado = usuario.Estado
            };
        }


       


        public bool Agregar(UsuarioDTO usuario)
        {
            Usuario entity = Convertir(usuario);
            entity.Clave = BCrypt.Net.BCrypt.HashPassword(usuario.Clave);

            Unidad.UsuarioDAL.Add(entity);
            var resultado = Unidad.Complete(); // Guarda el usuario

            if (resultado)
            {
                // Verifica si es rol "Estudiante" (puedes usar el ID o comparar con nombre)
                var rol = context.Roles.FirstOrDefault(r => r.IdRol == usuario.IdRol);

                if (rol != null && rol.NombreRol.ToLower() == "estudiante")
                {
                    // Buscar el ID del usuario recién insertado si aún no se tiene
                    var nuevoUsuario = context.Usuarios.FirstOrDefault(u => u.NombreUsuario == usuario.NombreUsuario);

                    if (nuevoUsuario != null)
                    {
                        byte[] codigoQR = GenerarCodigoQR(nuevoUsuario.Cedula);

                        Estudiante estudiante = new Estudiante
                        {
                            Nombre = nuevoUsuario.NombreCompleto,
                            Cedula = nuevoUsuario.Cedula,
                            IdEscuela = nuevoUsuario.IdEscuela,
                            IdUsuario = nuevoUsuario.IdUsuario,
                            TiquetesRestantes = 5,
                            NombreUsuario = nuevoUsuario.NombreUsuario,
                            Clave = nuevoUsuario.Clave, // ⚠️ Idealmente cifrado
                            RutaQR = codigoQR, // Asignar la ruta del QR generado
                            FechaUltimoRebajo = DateTime.Now.Date // Establece la fecha del último rebajo al momento de crear el estudiante
                        };

                        context.Estudiantes.Add(estudiante);
                        context.SaveChanges();
                    }
                }
            }

            return resultado;
        }


        private byte[] GenerarCodigoQR(string cedula)
        {
            string datosQR = $"http://192.168.0.17:5153/Visual/Escanear/{cedula}";

            using (var qrGenerator = new QRCodeGenerator())
            {
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(datosQR, QRCodeGenerator.ECCLevel.Q);
                using (var qrCode = new PngByteQRCode(qrCodeData))
                {
                    return qrCode.GetGraphic(20);
                }
            }
        }



        public bool Editar(UsuarioDTO usuarioEditado)
        {
            var usuarioOriginal = Unidad.UsuarioDAL.Get(usuarioEditado.IdUsuario);
            if (usuarioOriginal == null)
                throw new Exception("No se encontró el usuario original.");

            bool cambioARolEstudiante = usuarioOriginal.IdRol != usuarioEditado.IdRol && usuarioEditado.IdRol == 3;
            bool dejoDeSerEstudiante = usuarioOriginal.IdRol == 3 && usuarioEditado.IdRol != 3;

            bool estabainactivo = usuarioOriginal.Estado == false && usuarioEditado.Estado == true && usuarioOriginal.IdRol == 3 && 
                usuarioEditado.IdRol == 3 && dejoDeSerEstudiante == false && cambioARolEstudiante == false;

            bool noCambioClave = usuarioOriginal.Clave == usuarioEditado.Clave;

            var claveEncryptada = usuarioEditado.Clave;

            if (noCambioClave != true)
            {
                claveEncryptada = BCrypt.Net.BCrypt.HashPassword(usuarioEditado.Clave);
            }


            // Aplicar cambios directamente al usuarioOriginal
            usuarioOriginal.NombreCompleto = usuarioEditado.NombreCompleto.ToUpper();
            usuarioOriginal.IdTipoCedula = usuarioEditado.IdTipoCedula;
            usuarioOriginal.Cedula = usuarioEditado.Cedula;
            usuarioOriginal.Clave = claveEncryptada;
            usuarioOriginal.Telefono = usuarioEditado.Telefono;
            usuarioOriginal.Direccion = usuarioEditado.Direccion;
            usuarioOriginal.CorreoElectronico = usuarioEditado.CorreoElectronico;
            usuarioOriginal.Estado = usuarioEditado.Estado;
            usuarioOriginal.IdEscuela = usuarioEditado.IdEscuela;
            usuarioOriginal.NombreUsuario = usuarioEditado.NombreUsuario;
            
            usuarioOriginal.IdRol = usuarioEditado.IdRol;
            

            Unidad.UsuarioDAL.Update(usuarioOriginal);

            if (cambioARolEstudiante || estabainactivo)
            {
                var yaExiste = Unidad.EstudianteDAL.GetByUsuario(usuarioEditado.IdUsuario);
                if (yaExiste == null)
                {
                    byte[] codigoQR = GenerarCodigoQR(usuarioEditado.Cedula);

                    var estudiante = new Estudiante
                    {
                        Nombre = usuarioEditado.NombreCompleto,
                        Cedula = usuarioEditado.Cedula,
                        IdEscuela = usuarioEditado.IdEscuela,
                        IdUsuario = usuarioEditado.IdUsuario,
                        TiquetesRestantes = 5,
                        NombreUsuario = usuarioEditado.NombreUsuario,
                        Clave = usuarioEditado.Clave,
                        RutaQR = codigoQR,
                        FechaUltimoRebajo = DateTime.Now.Date
                    };

                    Unidad.EstudianteDAL.Add(estudiante);
                }
            }

            if (dejoDeSerEstudiante)
            {
                var estudiante = Unidad.EstudianteDAL.GetByUsuario(usuarioEditado.IdUsuario);
                if (estudiante != null)
                {
                    Unidad.EstudianteDAL.Remove(estudiante);
                }
            }

            return Unidad.Complete();
        }



        public bool Eliminar(UsuarioDTO usuario)
        {
            var entity = Convertir(usuario);

            // Verificar si el usuario tiene rol de estudiante
            var rol = context.Roles.FirstOrDefault(r => r.IdRol == usuario.IdRol);

            if (rol != null && rol.NombreRol.ToLower() == "estudiante")
            {
                // Buscar el estudiante vinculado a ese usuario
                var estudiante = context.Estudiantes.FirstOrDefault(e => e.IdUsuario == usuario.IdUsuario);
                if (estudiante != null)
                {
                    context.Estudiantes.Remove(estudiante);
                    context.SaveChanges(); // Asegura que se elimina antes de eliminar el usuario
                }
            }

            Unidad.UsuarioDAL.Remove(entity);
            return Unidad.Complete();
        }

        public UsuarioDTO Obtener(int id)
        {
            return Convertir(Unidad.UsuarioDAL.Get(id));
        }
        public List<UsuarioDTO> Obtener()
        {
            var usuarios = Unidad.UsuarioDAL.GetAll()
                .Join(
                    context.Roles,
                    usuario => usuario.IdRol,
                    rol => rol.IdRol,
                    (usuario, rol) => new { Usuario = usuario, Rol = rol }
                )
                .Join(
                    context.TipoCedulas,
                    ur => ur.Usuario.IdTipoCedula,
                    tipocedula => tipocedula.IdTipoCedula,
                    (ur, tipocedula) => new { UR = ur, TipoCedula = tipocedula }
                )
                .Join(
                    context.Escuelas,
                    urt => urt.UR.Usuario.IdEscuela,
                    escuela => escuela.IdEscuela,
                    (urt, escuela) => new UsuarioDTO
                    {
                        IdUsuario = urt.UR.Usuario.IdUsuario,
                        NombreUsuario = urt.UR.Usuario.NombreUsuario,
                        NombreCompleto = urt.UR.Usuario.NombreCompleto,
                        IdTipoCedula = urt.UR.Usuario.IdTipoCedula,
                        Cedula = urt.UR.Usuario.Cedula,
                        Telefono = urt.UR.Usuario.Telefono,
                        Direccion = urt.UR.Usuario.Direccion,
                        CorreoElectronico = urt.UR.Usuario.CorreoElectronico,
                        Clave = urt.UR.Usuario.Clave,
                        Estado = urt.UR.Usuario.Estado,
                        IdEscuela = urt.UR.Usuario.IdEscuela,
                        IdRol = urt.UR.Usuario.IdRol,
                        NombreRol = urt.UR.Rol.NombreRol,
                        NombreTipoCedula = urt.TipoCedula.NombreTipoCedula,
                        NombreEscuela = escuela.NombreEscuela
                    }
                ).ToList();

            return usuarios;
        }


    }
}
