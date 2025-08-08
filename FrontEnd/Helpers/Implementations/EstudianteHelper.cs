using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class EstudianteHelper : IEstudianteHelper
    {
        IServiceRepository _ServiceRepository;

        private readonly string _baseUrl;

        public EstudianteHelper(IConfiguration config, IServiceRepository serviceRepository)
        {
            _baseUrl = config["BaseUrl"];
            _ServiceRepository = serviceRepository;
        }

        private Estudiante Convertir(EstudianteViewModel estudiante)
        {
            return new Estudiante
            {
                IdEstudiante = estudiante.IdEstudiante,
                Nombre = estudiante.Nombre,
                Cedula = estudiante.Cedula,
                IdEscuela = estudiante.IdEscuela,
                TiquetesRestantes = estudiante.TiquetesRestantes,
                IdUsuario = estudiante.IdUsuario,
                RutaQR = estudiante.RutaQR,
                NombreUsuario = estudiante.NombreUsuario,
                Clave = estudiante.Clave,
                FechaUltimoRebajo = estudiante.FechaUltimoRebajo


            };
        }

        public EstudianteViewModel Add(EstudianteViewModel estudiante)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Estudiante", Convertir(estudiante));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return estudiante;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Estudiante/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode) { var content = responseMessage.Content; }



        }

        public List<EstudianteViewModel> GetEstudiantes()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Estudiante");
            List<Estudiante> estudiantes = new List<Estudiante>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                estudiantes = JsonConvert.DeserializeObject<List<Estudiante>>(content);
            }

            List<EstudianteViewModel> resultado = new List<EstudianteViewModel>();
            foreach (var item in estudiantes)
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

                resultado.Add(new EstudianteViewModel
                {

                    IdEstudiante = item.IdEstudiante,
                    Nombre = item.Nombre,
                    Cedula = item.Cedula,
                    IdEscuela = item.IdEscuela,
                    TiquetesRestantes = item.TiquetesRestantes,
                    IdUsuario = item.IdUsuario,
                    RutaQR = item.RutaQR,
                    NombreUsuario = item.NombreUsuario,
                    Clave = item.Clave,
                    FechaUltimoRebajo = item.FechaUltimoRebajo,
                    NombreEscuela = escuela?.NombreEscuela ?? "Desconocido"

                });
            }
            return resultado;

        }


        public EstudianteViewModel GetEstudiante(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Estudiante/" + id.ToString());
            Estudiante estudiante = new Estudiante();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                estudiante = JsonConvert.DeserializeObject<Estudiante>(content);
            }

            EstudianteViewModel resultado =
                            new EstudianteViewModel
                            {
                                IdEstudiante = estudiante.IdEstudiante,
                                Nombre = estudiante.Nombre,
                                Cedula = estudiante.Cedula,
                                IdEscuela = estudiante.IdEscuela,
                                TiquetesRestantes = estudiante.TiquetesRestantes,
                                IdUsuario = estudiante.IdUsuario,
                                RutaQR = estudiante.RutaQR,
                                NombreUsuario = estudiante.NombreUsuario,
                                Clave = estudiante.Clave,
                                FechaUltimoRebajo = estudiante.FechaUltimoRebajo
                            };


            return resultado;
        }

        public EstudianteViewModel Update(EstudianteViewModel estudiante)
        {
            HttpResponseMessage response = _ServiceRepository.PutResponse("api/Estudiante", Convertir(estudiante));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return estudiante;
        }



        public EstudianteViewModel GetEstudiantePorUsuario(int idUsuario)
        {
            EstudianteViewModel estudiante = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl); // Usa lo que esté en appsettings.json
                var response = client.GetAsync($"api/Estudiante/PorUsuario/{idUsuario}").Result;

                if (response.IsSuccessStatusCode)
                {
                    var content = response.Content.ReadAsStringAsync().Result;
                    estudiante = JsonConvert.DeserializeObject<EstudianteViewModel>(content);
                }
            }

            return estudiante;
        }

        public EstudianteViewModel DescontarTiquete(int idEstudiante)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse($"api/Estudiante/DescontarTiquete/{idEstudiante}", null);

            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<EstudianteViewModel>(content);
            }
            else
            {
                return null;
            }
        }

        public EstudianteViewModel GetEstudiantePorCedula(string cedula)
        {
            EstudianteViewModel estudiante = null;

            using (var client = new HttpClient())
            {
                client.BaseAddress = new Uri(_baseUrl);

                var response = client.GetAsync($"api/Estudiante/VerPorCedula/{cedula}").Result;

                if (response.IsSuccessStatusCode)
                {
                    estudiante = response.Content.ReadFromJsonAsync<EstudianteViewModel>().Result;
                }
            }

            return estudiante;
        }



    }
}