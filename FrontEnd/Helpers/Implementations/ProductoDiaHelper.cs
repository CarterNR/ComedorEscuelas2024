using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class ProductoDiaHelper : IProductoDiaHelper
    {
        IServiceRepository _ServiceRepository;

        ProductoDia Convertir(ProductoDiaViewModel productodia)
        {
            return new ProductoDia
            {

                IdProductoDia = productodia.IdProductoDia,
                IdProducto = productodia.IdProducto,
                Cantidad = productodia.Cantidad,
                Fecha = productodia.Fecha,
                Estado = productodia.Estado,
                IdEscuela = productodia.IdEscuela

    };
        }


        public ProductoDiaHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public ProductoDiaViewModel Add(ProductoDiaViewModel productodia)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/ProductoDia", Convertir(productodia));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return productodia;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/ProductoDia/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode) { var content = responseMessage.Content; }



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



        public ProductoDiaViewModel GetProductoDia(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/ProductoDia/" + id.ToString());
            ProductoDia productodia = new ProductoDia();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                productodia = JsonConvert.DeserializeObject<ProductoDia>(content);
            }

            ProductoDiaViewModel resultado =
                            new ProductoDiaViewModel
                            {
                                IdProductoDia = productodia.IdProductoDia,
                                IdProducto = productodia.IdProducto,
                                Cantidad = productodia.Cantidad,
                                Fecha = productodia.Fecha,
                                Estado = productodia.Estado,
                                IdEscuela = productodia.IdEscuela
                            };


            return resultado;
        }

        public ProductoDiaViewModel Update(ProductoDiaViewModel productodia)
        {
            HttpResponseMessage response = _ServiceRepository.PutResponse("api/ProductoDia", Convertir(productodia));
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return productodia;
        }


    }
}