using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class PedidoHelper : IPedidoHelper
    {
        IServiceRepository _ServiceRepository;

        Pedido Convertir(PedidoViewModel pedido)
        {
            return new Pedido
            {
                IdPedido = pedido.IdPedido,
                IdProducto = pedido.IdProducto,
                FechaHoraIngreso = pedido.FechaHoraIngreso,
                Cantidad = pedido.Cantidad,
                IdUsuario = pedido.IdUsuario,
                IdEscuela = pedido.IdEscuela,
                IdEstadoPedido = pedido.IdEstadoPedido,
                Estado = pedido.Estado

            };
        }


        public PedidoHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public PedidoViewModel Add(PedidoViewModel pedido)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Pedido", Convertir(pedido));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Pedido creado exitosamente. Respuesta del servidor: " + content);
            }
            else
            {
                var errorContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al crear el pedido. Código de estado: " + response.StatusCode);
                Console.WriteLine("Detalles del error: " + errorContent);
                throw new Exception("Error al crear el proveedor: " + errorContent);
            }
            return pedido;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Pedido/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode) { var content = responseMessage.Content; }



        }

        public List<PedidoViewModel> GetPedidos()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Pedido");
            List<Pedido> pedidos = new List<Pedido>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                pedidos = JsonConvert.DeserializeObject<List<Pedido>>(content);
            }

            List<PedidoViewModel> resultado = new List<PedidoViewModel>();
            foreach (var item in pedidos)
            {

                // Obtener el nombre del producto asociado
                HttpResponseMessage productoResponse = _ServiceRepository.GetResponse("api/Producto/" + item.IdProducto);
                Producto producto = null;
                if (productoResponse.IsSuccessStatusCode)
                {
                    producto = JsonConvert.DeserializeObject<Producto>(productoResponse.Content.ReadAsStringAsync().Result);
                }

                // Obtener el nombre de la escuela asociada
                HttpResponseMessage escuelaResponse = _ServiceRepository.GetResponse("api/Escuela/" + item.IdEscuela);
                Escuela escuela = null;
                if (escuelaResponse.IsSuccessStatusCode)
                {
                    escuela = JsonConvert.DeserializeObject<Escuela>(escuelaResponse.Content.ReadAsStringAsync().Result);
                }

                HttpResponseMessage usuarioResponse = _ServiceRepository.GetResponse("api/Usuario/" + item.IdUsuario);
                Usuario usuario = null;
                if (usuarioResponse.IsSuccessStatusCode)
                {
                    usuario = JsonConvert.DeserializeObject<Usuario>(usuarioResponse.Content.ReadAsStringAsync().Result);
                }

                HttpResponseMessage estadopedidoResponse = _ServiceRepository.GetResponse("api/EstadoPedido/" + item.IdEstadoPedido);
                EstadoPedido estadopedido = null;
                if (estadopedidoResponse.IsSuccessStatusCode)
                {
                    estadopedido = JsonConvert.DeserializeObject<EstadoPedido>(estadopedidoResponse.Content.ReadAsStringAsync().Result);
                }


                resultado.Add(new PedidoViewModel
                {

                    IdPedido = item.IdPedido,
                    IdProducto = item.IdProducto,
                    NombreProducto = producto?.NombreProducto ?? "Desconocido", // Nombre del producto
                    FechaHoraIngreso = item.FechaHoraIngreso,
                    Cantidad = item.Cantidad,
                    IdUsuario = item.IdUsuario,
                    NombreCompleto = usuario?.NombreCompleto ?? "Desconocido", 
                    IdEscuela = item.IdEscuela,
                    NombreEscuela = escuela?.NombreEscuela ?? "Desconocido", // Nombre de la escuela
                    IdEstadoPedido = item.IdEstadoPedido,
                    EstadoPedido1 = estadopedido?.EstadoPedido1 ?? "Desconocido", // Nombre de la escuela
                    Estado = item.Estado
                });
            }
            return resultado;
        

        }

        public List<ProductoDiaViewModel> GetProductosDia()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/ProductoDia");
            List<ProductoDia> productosdia = new List<ProductoDia>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                productosdia = JsonConvert.DeserializeObject<List<ProductoDia>>(content);
            }

            List<ProductoDiaViewModel> resultado = new List<ProductoDiaViewModel>();
            foreach (var item in productosdia)
            {
                // Obtener el nombre del producto asociado
                HttpResponseMessage productoResponse = _ServiceRepository.GetResponse("api/Producto/" + item.IdProducto);
                Producto producto = null;
                if (productoResponse.IsSuccessStatusCode)
                {
                    producto = JsonConvert.DeserializeObject<Producto>(productoResponse.Content.ReadAsStringAsync().Result);
                }

                // Obtener el nombre de la escuela asociada
                HttpResponseMessage escuelaResponse = _ServiceRepository.GetResponse("api/Escuela/" + item.IdEscuela);
                Escuela escuela = null;
                if (escuelaResponse.IsSuccessStatusCode)
                {
                    escuela = JsonConvert.DeserializeObject<Escuela>(escuelaResponse.Content.ReadAsStringAsync().Result);
                }

                resultado.Add(new ProductoDiaViewModel
                {
                    IdProductoDia = item.IdProductoDia,
                    IdProducto = item.IdProducto,
                    NombreProducto = producto?.NombreProducto ?? "Desconocido", // Nombre del producto
                    Cantidad = item.Cantidad,
                    Fecha = item.Fecha,
                    Estado = item.Estado,
                    IdEscuela = item.IdEscuela,
                    NombreEscuela = escuela?.NombreEscuela ?? "Desconocido" // Nombre de la escuela
                });
            }
            return resultado;
        }

        public PedidoViewModel GetPedido(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Pedido/" + id.ToString());
            Pedido pedido = new Pedido();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                pedido = JsonConvert.DeserializeObject<Pedido>(content);
            }

            PedidoViewModel resultado =
                            new PedidoViewModel
                            {
                                IdPedido = pedido.IdPedido,
                                IdProducto = pedido.IdProducto,
                                FechaHoraIngreso = pedido.FechaHoraIngreso,
                                Cantidad = pedido.Cantidad,
                                IdUsuario = pedido.IdUsuario,
                                IdEscuela = pedido.IdEscuela,
                                IdEstadoPedido = pedido.IdEstadoPedido,
                                Estado = pedido.Estado
                            };


            return resultado;
        }

        public PedidoViewModel Update(PedidoViewModel pedido)
        {
            HttpResponseMessage response = _ServiceRepository.PutResponse("api/Pedido", Convertir(pedido));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return pedido;
        }


    
    }
}