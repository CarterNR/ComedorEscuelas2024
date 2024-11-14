using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class EscuelaHelper : IEscuelaHelper
    {
        IServiceRepository _ServiceRepository;

        Escuela Convertir(EscuelaViewModel escuela)
        {
            return new Escuela
            {
                IdEscuela = escuela.IdEscuela,
                NombreEscuela = escuela.NombreEscuela,
                Estado = escuela.Estado

            };
        }


        public EscuelaHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public EscuelaViewModel Add(EscuelaViewModel escuela)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Escuela", Convertir(escuela));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return escuela;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Escuela/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode) { var content = responseMessage.Content; }



        }

        public List<EscuelaViewModel> GetEscuelas()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Escuela");
            List<Escuela> escuelas = new List<Escuela>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                escuelas = JsonConvert.DeserializeObject<List<Escuela>>(content);
            }

            List<EscuelaViewModel> resultado = new List<EscuelaViewModel>();
            foreach (var item in escuelas)
            {
                resultado.Add(
                            new EscuelaViewModel
                            {
                                IdEscuela = item.IdEscuela,
                                NombreEscuela = item.NombreEscuela,
                                Estado = item.Estado
                            }
                    );
            }
            return resultado;

        }

        public EscuelaViewModel GetEscuela(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Escuela/" + id.ToString());
            Escuela escuela = new Escuela();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                escuela = JsonConvert.DeserializeObject<Escuela>(content);
            }

            EscuelaViewModel resultado =
                            new EscuelaViewModel
                            {
                                IdEscuela = escuela.IdEscuela,
                                NombreEscuela = escuela.NombreEscuela,
                                Estado = escuela.Estado
                            };


            return resultado;
        }

        public EscuelaViewModel Update(EscuelaViewModel escuela)
        {
            HttpResponseMessage response = _ServiceRepository.PutResponse("api/Escuela", Convertir(escuela));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return escuela;
        }
    }
}