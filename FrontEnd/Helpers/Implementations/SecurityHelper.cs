using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;

namespace FrontEnd.Helpers.Implementations
{
    public class SecurityHelper : ISecurityHelper
    {
        IServiceRepository ServiceRepository;
        private readonly IHttpContextAccessor httpContextAccessor;
        public SecurityHelper(IServiceRepository serviceRepository, IHttpContextAccessor httpContextAccessor)
        {
            ServiceRepository = serviceRepository;
            this.httpContextAccessor = httpContextAccessor;
        }


        public bool Register(RegisterAPI registerModel)
        {
            try
            {
                HttpResponseMessage response = ServiceRepository
                    .PostResponse("/api/Auth/register", registerModel);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                // Manejar errores según tu lógica
                throw new ApplicationException("Error al registrar el usuario", ex);
            }
        }

        public void Logout()
        {
            // Accede al HttpContext a través del accessor
            var session = httpContextAccessor.HttpContext.Session;

            if (session != null)
            {
                session.Remove("token"); // Elimina el token de la sesión
            }
        }



        public bool DeleteUser(string id)
        {
            var response = ServiceRepository.DeleteResponse($"/api/Auth/delete/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
