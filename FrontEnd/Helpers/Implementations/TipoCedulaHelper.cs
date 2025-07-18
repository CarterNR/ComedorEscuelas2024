using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class TipoCedulaHelper : ITipoCedulaHelper
    {
        IServiceRepository _serviceRepository;

        public TipoCedulaHelper(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        private TipoCedula Convertir(TipoCedulaViewModel tipocedula)
        {
            return new TipoCedula
            {
                IdTipoCedula = tipocedula.IdTipoCedula,
                NombreTipoCedula= tipocedula.NombreTipoCedula
            };
        }

        public TipoCedulaViewModel Add(TipoCedulaViewModel tipocedula)
        {


            HttpResponseMessage response = _serviceRepository.PostResponse("api/TipoCedula", Convertir(tipocedula));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al crear el tipo de cedula");
            }
            return tipocedula;
        }

        public void Delete(int id)
        {
            HttpResponseMessage response = _serviceRepository.DeleteResponse($"api/TipoCedula/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("No se puede eliminar el tipo de cedula porque está siendo utilizado");
            }
        }

        public TipoCedulaViewModel GetTipoCedula(int? id)
        {
            HttpResponseMessage response = _serviceRepository.GetResponse($"api/TipoCedula/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = response.Content.ReadAsStringAsync().Result;
            var tipocedula = JsonConvert.DeserializeObject<TipoCedula>(content);

            return new TipoCedulaViewModel
            {
                IdTipoCedula = tipocedula.IdTipoCedula,
                NombreTipoCedula = tipocedula.NombreTipoCedula
            };
        }

        public List<TipoCedulaViewModel> GetTiposCedulas()
        {
            HttpResponseMessage response = _serviceRepository.GetResponse("api/TipoCedula");
            if (!response.IsSuccessStatusCode) return new List<TipoCedulaViewModel>();

            var content = response.Content.ReadAsStringAsync().Result;
            var tiposcedulas = JsonConvert.DeserializeObject<List<TipoCedula>>(content);

            return tiposcedulas.Select(t => new TipoCedulaViewModel
            {
                IdTipoCedula = t.IdTipoCedula,
                NombreTipoCedula = t.NombreTipoCedula
            }).ToList();
        }

        public TipoCedulaViewModel Update(TipoCedulaViewModel tipocedula)
        {


            // Convertir el ProductoViewModel a un objeto que pueda enviarse a la API
            var tipocedulaParaActualizar = Convertir(tipocedula);

            // Enviar la solicitud de actualización a la API
            HttpResponseMessage response = _serviceRepository.PutResponse("api/TipoCedula", tipocedulaParaActualizar);

            if (!response.IsSuccessStatusCode)
            {
                // Obtener detalles del error
                var errorContent = response.Content.ReadAsStringAsync().Result;
                throw new Exception($"Error al actualizar el tipo de cedula: {errorContent}");
            }

            return tipocedula;
        }
    }
}