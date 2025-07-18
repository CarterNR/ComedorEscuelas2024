using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Net.Http;
using System.Text;

namespace FrontEnd.Helpers.Implementations
{
    public class UsuarioHelper : IUsuarioHelper
    {

        private readonly IServiceRepository _ServiceRepository;


        public UsuarioHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;

        }

        public UsuarioViewModel Autenticar(string nombreUsuario, string clave)
        {
            var login = new
            {
                NombreUsuario = nombreUsuario,
                Clave = clave
            };

            HttpResponseMessage response = _ServiceRepository.PostResponse("api/usuario/login", login);

            if (response.IsSuccessStatusCode)
            {
                var contenido = response.Content.ReadAsStringAsync().Result;
                var usuario = JsonConvert.DeserializeObject<UsuarioViewModel>(contenido);
                return usuario;
            }

            return null;
        }



        private Usuario Convertir(UsuarioViewModel usuario)
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

        public async Task<List<UsuarioViewModel>> GetUsuariosAsync()
        {
            var responseMessage = await _ServiceRepository.GetResponseAsync("api/Usuario");
            var usuarios = new List<Usuario>();

            if (responseMessage.IsSuccessStatusCode)
            {
                var content = await responseMessage.Content.ReadAsStringAsync();
                usuarios = JsonConvert.DeserializeObject<List<Usuario>>(content);
            }

            var resultado = usuarios.Select(item => new UsuarioViewModel
            {
                IdUsuario = item.IdUsuario,
                NombreCompleto = item.NombreCompleto,
                IdTipoCedula = item.IdTipoCedula,
                Cedula = item.Cedula,
                Telefono = item.Telefono,
                Direccion = item.Direccion,
                CorreoElectronico = item.CorreoElectronico,
                Clave = item.Clave,
                Estado = item.Estado,
                IdEscuela = item.IdEscuela,
                IdRol = item.IdRol,
                NombreUsuario = item.NombreUsuario
            }).ToList();

            return resultado;
        }



        public UsuarioViewModel Add(UsuarioViewModel usuario)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Usuario", Convertir(usuario));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Usuario creado exitosamente. Respuesta del servidor: " + content);
            }
            else
            {
                var errorContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al crear el usuario. Código de estado: " + response.StatusCode);
                Console.WriteLine("Detalles del error: " + errorContent);
                throw new Exception("Error al crear el usuario: " + errorContent);
            }
            return usuario;
        }

        public void Delete(int id)
        {
            // Buscar el usuario primero
            UsuarioViewModel usuario = GetUsuario(id);

            if (usuario != null && usuario.IdRol != null)
            {
                // Buscar el rol del usuario (si es estudiante)
                // Buscar estudiante asociado
                HttpResponseMessage estudianteResponse = _ServiceRepository.GetResponse("api/Estudiante/porUsuario/" + id);
                if (estudianteResponse.IsSuccessStatusCode)
                {
                    var estudianteContent = estudianteResponse.Content.ReadAsStringAsync().Result;

                    if (!string.IsNullOrWhiteSpace(estudianteContent) && estudianteContent != "null")
                    {
                        var estudiante = JsonConvert.DeserializeObject<Estudiante>(estudianteContent);

                        if (estudiante != null)
                        {
                            // Eliminar estudiante
                            HttpResponseMessage deleteEst = _ServiceRepository.DeleteResponse("api/Estudiante/" + estudiante.IdEstudiante);
                            if (!deleteEst.IsSuccessStatusCode)
                            {
                                throw new Exception("Error al eliminar el estudiante antes de borrar el usuario.");
                            }
                        }
                    }
                }

            }

            // Finalmente eliminar el usuario
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Usuario/" + id);
            if (!responseMessage.IsSuccessStatusCode)
            {
                var error = responseMessage.Content.ReadAsStringAsync().Result;
                throw new Exception("Error al eliminar el usuario: " + error);
            }
        }


        public List<UsuarioViewModel> GetUsuarios()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Usuario");
            List<Usuario> usuarios = new List<Usuario>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                usuarios = JsonConvert.DeserializeObject<List<Usuario>>(content);
            }

            List<UsuarioViewModel> resultado = new List<UsuarioViewModel>();
            foreach (var item in usuarios)
            {

                Escuela escuela = null;

                try
                {
                    HttpResponseMessage escuelaResponse = _ServiceRepository.GetResponse("api/Escuela/" + item.IdEscuela);
                    if (escuelaResponse.IsSuccessStatusCode)
                    {
                        var escuelaContent = escuelaResponse.Content.ReadAsStringAsync().Result;
                        escuela = JsonConvert.DeserializeObject<Escuela>(escuelaContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener datos de la escuela: {ex.Message}");
                }

                resultado.Add(
                            new UsuarioViewModel
                            {
                                IdUsuario = item.IdUsuario,
                                NombreCompleto = item.NombreCompleto,
                                IdTipoCedula = item.IdTipoCedula,
                                Cedula = item.Cedula,
                                Telefono = item.Telefono,
                                Direccion = item.Direccion,
                                CorreoElectronico = item.CorreoElectronico,
                                Clave = item.Clave,
                                Estado = item.Estado,
                                IdEscuela = item.IdEscuela,
                                IdRol = item.IdRol,
                                NombreUsuario = item.NombreUsuario,
                                NombreEscuela = escuela?.NombreEscuela ?? "Desconocido"

                            }
                    );
            }
            return resultado;

        }

        public UsuarioViewModel GetUsuario(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Usuario/" + id.ToString());
            Usuario usuario = new Usuario();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                usuario = JsonConvert.DeserializeObject<Usuario>(content);
            }

            UsuarioViewModel resultado =
                            new UsuarioViewModel
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


            return resultado;
        }

        public UsuarioViewModel Update(UsuarioViewModel usuario)
        {
            HttpResponseMessage response = _ServiceRepository.PutResponse("api/Usuario", Convertir(usuario));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return usuario;
        }

    }
}