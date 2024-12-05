using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class EstadoPedidoHelper : IEstadoPedidoHelper
    {
        IServiceRepository _ServiceRepository;

        EstadoPedido Convertir(EstadoPedidoViewModel estadopedido)
        {
            return new EstadoPedido
            {
                IdEstadoPedido = estadopedido.IdEstadoPedido,
                EstadoPedido1 = estadopedido.EstadoPedido1
       
             };
        }


        public EstadoPedidoHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public EstadoPedidoViewModel Add(EstadoPedidoViewModel estadopedido)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/EstadoPedido", Convertir(estadopedido));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return estadopedido;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/EstadoPedido/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode) { var content = responseMessage.Content; }



        }

        public List<EstadoPedidoViewModel> GetEstadoPedidos()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/EstadoPedido");
            List<EstadoPedido> estadopedidos = new List<EstadoPedido>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                estadopedidos = JsonConvert.DeserializeObject<List<EstadoPedido>>(content);
            }

            List<EstadoPedidoViewModel> resultado = new List<EstadoPedidoViewModel>();
            foreach (var item in estadopedidos)
            {
                resultado.Add(
                            new EstadoPedidoViewModel
                            {
                                IdEstadoPedido = item.IdEstadoPedido,
                                EstadoPedido1 = item.EstadoPedido1
                            }
                    );
            }
            return resultado;

        }

        public EstadoPedidoViewModel GetEstadoPedido(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/EstadoPedido/" + id.ToString());
            EstadoPedido estadopedido = new EstadoPedido();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                estadopedido = JsonConvert.DeserializeObject<EstadoPedido>(content);
            }

            EstadoPedidoViewModel resultado =
                            new EstadoPedidoViewModel
                            {
                                IdEstadoPedido = estadopedido.IdEstadoPedido,
                                EstadoPedido1 = estadopedido.EstadoPedido1
                            };


            return resultado;
        }

        public EstadoPedidoViewModel Update(EstadoPedidoViewModel estadopedido)
        {
            HttpResponseMessage response = _ServiceRepository.PutResponse("api/EstadoPedido", Convertir(estadopedido));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return estadopedido;
        }
    }
}