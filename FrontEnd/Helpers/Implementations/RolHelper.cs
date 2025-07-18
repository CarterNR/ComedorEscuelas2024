using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class RolHelper : IRolHelper
    {
        IServiceRepository _serviceRepository;

        public RolHelper(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        private Rol Convertir(RolViewModel rol)
        {
            return new Rol
            {
                IdRol = rol.IdRol,
                NombreRol = rol.NombreRol
            };
        }

        public RolViewModel Add(RolViewModel rol)
        {


            HttpResponseMessage response = _serviceRepository.PostResponse("api/Rol", Convertir(rol));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al crear el rol");
            }
            return rol;
        }

        public void Delete(int id)
        {
            HttpResponseMessage response = _serviceRepository.DeleteResponse($"api/Rol/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("No se puede eliminar el rol porque está siendo utilizado");
            }
        }

        public RolViewModel GetRol(int? id)
        {
            HttpResponseMessage response = _serviceRepository.GetResponse($"api/Rol/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = response.Content.ReadAsStringAsync().Result;
            var rol = JsonConvert.DeserializeObject<Rol>(content);

            return new RolViewModel
            {
                IdRol = rol.IdRol,
                NombreRol = rol.NombreRol
            };
        }

        public List<RolViewModel> GetRoles()
        {
            HttpResponseMessage response = _serviceRepository.GetResponse("api/Rol");
            if (!response.IsSuccessStatusCode) return new List<RolViewModel>();

            var content = response.Content.ReadAsStringAsync().Result;
            var roles = JsonConvert.DeserializeObject<List<Rol>>(content);

            return roles.Select(r => new RolViewModel
            {
                IdRol = r.IdRol,
                NombreRol = r.NombreRol
            }).ToList();
        }

        public RolViewModel Update(RolViewModel rol)
        {


            // Convertir el ProductoViewModel a un objeto que pueda enviarse a la API
            var rolParaActualizar = Convertir(rol);

            // Enviar la solicitud de actualización a la API
            HttpResponseMessage response = _serviceRepository.PutResponse("api/Rol", rolParaActualizar);

            if (!response.IsSuccessStatusCode)
            {
                // Obtener detalles del error
                var errorContent = response.Content.ReadAsStringAsync().Result;
                throw new Exception($"Error al actualizar el producto: {errorContent}");
            }

            return rol;
        }
    }
}