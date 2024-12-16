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


        public LoginAPI Login(UserViewModel user)
        {
            try
            {
                HttpResponseMessage response = ServiceRepository
                                                    .PostResponse("/api/Auth/login", new {user.UserName, user.Password});

                var content  = response.Content.ReadAsStringAsync().Result;
                LoginAPI loginAPI = JsonConvert.DeserializeObject<LoginAPI>(content);

                return loginAPI;
            }
            catch (Exception)
            {
                throw;
            }
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


        public List<UserAPI> GetAllUsers()
        {
            try
            {
                var response = ServiceRepository.GetResponse("/api/Auth/users");
                var content = response.Content.ReadAsStringAsync().Result;

                var users = JsonConvert.DeserializeObject<List<UserAPI>>(content);

                return users;
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener los usuarios: " + ex.Message);
            }
        }


        public UserAPI GetUserDetails(string id)
        {
            var response = ServiceRepository.GetResponse($"/api/Auth/details/{id}");
            var content = response.Content.ReadAsStringAsync().Result;
            return JsonConvert.DeserializeObject<UserAPI>(content);
        }

        public bool UpdateUser(UsersViewModel user)
        {
            var response = ServiceRepository.PutResponse($"/api/Auth/update/{user.Id}", new
            {
                user.UserName,
                user.Email
            });

            return response.IsSuccessStatusCode;
        }

        public bool DeleteUser(string id)
        {
            var response = ServiceRepository.DeleteResponse($"/api/Auth/delete/{id}");
            return response.IsSuccessStatusCode;
        }

    }
}
