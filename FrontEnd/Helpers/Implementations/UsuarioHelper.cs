using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class UsuarioHelper : IUsuarioHelper
    {
        IServiceRepository _ServiceRepository;

        Usuario Convertir(UsuarioViewModel usuario)
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


        public UsuarioHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public UsuarioViewModel Add(UsuarioViewModel usuario)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Usuario", Convertir(usuario));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return usuario;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Usuario/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode) { var content = responseMessage.Content; }



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
                                IdRol = item.IdRol
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
                                IdRol = usuario.IdRol
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