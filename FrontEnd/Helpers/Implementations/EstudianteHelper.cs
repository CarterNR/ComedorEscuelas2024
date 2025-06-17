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

        Estudiante Convertir(EstudianteViewModel estudiante)
        {
            return new Estudiante
            {
                IdEstudiante = estudiante.IdEstudiante,
                Nombre = estudiante.Nombre,
                Cedula = estudiante.Cedula,
                Contrasena = estudiante.Contrasena,
                TiquetesRestantes = estudiante.TiquetesRestantes


    };
        }


        public EstudianteHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
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
                resultado.Add(
                            new EstudianteViewModel
                            {
                                IdEstudiante = item.IdEstudiante,
                                Nombre = item.Nombre,
                                Cedula = item.Cedula,
                                Contrasena = item.Contrasena,
                                TiquetesRestantes = item.TiquetesRestantes
                            }
                    );
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
                                Contrasena = estudiante.Contrasena,
                                TiquetesRestantes = estudiante.TiquetesRestantes



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
    }
}