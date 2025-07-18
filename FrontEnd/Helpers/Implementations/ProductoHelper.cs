using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class ProductoHelper : IProductoHelper
    {
        IServiceRepository _serviceRepository;

        public ProductoHelper(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        private Producto Convertir(ProductoViewModel producto)
        {
            return new Producto
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                Cantidad = producto.Cantidad,
                Imagen = producto.Imagen,
                Estado = producto.Estado,
                IdProveedor = producto.IdProveedor,
                IdEscuela = producto.IdEscuela
            };
        }

        public ProductoViewModel Add(ProductoViewModel producto)
        {
            if (producto.ImagenFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    producto.ImagenFile.CopyTo(ms);
                    producto.Imagen = ms.ToArray();
                }
            }

            HttpResponseMessage response = _serviceRepository.PostResponse("api/Producto", Convertir(producto));
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("Error al crear el producto");
            }
            return producto;
        }


        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _serviceRepository.DeleteResponse("api/Producto/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode) { var content = responseMessage.Content; }

        }



        public ProductoViewModel GetProducto(int? id)
        {
            HttpResponseMessage responseMessage = _serviceRepository.GetResponse("api/Producto/" + id.ToString());
            Producto producto = new Producto();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                producto = JsonConvert.DeserializeObject<Producto>(content);
            }

            ProductoViewModel resultado =
                            new ProductoViewModel
                            {
                                IdProducto = producto.IdProducto,
                                NombreProducto = producto.NombreProducto,
                                Cantidad = producto.Cantidad,
                                Imagen = producto.Imagen,
                                Estado = producto.Estado,
                                IdProveedor = producto.IdProveedor,
                                IdEscuela = producto.IdEscuela,
                                NombreProveedor = producto.NombreProveedor,
                                NombreEscuela = producto.NombreEscuela
                            };


            return resultado;
        }

        public List<ProductoViewModel> GetProductos()
        {
            HttpResponseMessage responseMessage = _serviceRepository.GetResponse("api/Producto");
            List<Producto> productos = new List<Producto>();
            if (responseMessage != null && responseMessage.IsSuccessStatusCode)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                productos = JsonConvert.DeserializeObject<List<Producto>>(content);
            }
            else
            {
                return new List<ProductoViewModel>();
            }

            List<ProductoViewModel> resultado = new List<ProductoViewModel>();

            foreach (var item in productos)
            {
                Escuela escuela = null;

                try
                {
                    HttpResponseMessage escuelaResponse = _serviceRepository.GetResponse("api/Escuela/" + item.IdEscuela);
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
                resultado.Add(new ProductoViewModel
                {
                    IdProducto = item.IdProducto,
                    NombreProducto = item.NombreProducto,
                    Cantidad = item.Cantidad,
                    Imagen = item.Imagen,
                    Estado = item.Estado,
                    IdProveedor = item.IdProveedor,
                    IdEscuela = item.IdEscuela,
                    NombreProveedor = item.NombreProveedor,
                    NombreEscuela = item.NombreEscuela
                });
            }
            return resultado;

        }

        public ProductoViewModel Update(ProductoViewModel producto)
        {
            // Convertir la imagen si hay un nuevo archivo
            if (producto.ImagenFile != null)
            {
                using (var ms = new MemoryStream())
                {
                    producto.ImagenFile.CopyTo(ms);
                    producto.Imagen = ms.ToArray();
                }
            }
            else
            {
                // Si no hay nueva imagen, obtener la imagen existente
                var productoExistente = GetProducto(producto.IdProducto);
                producto.Imagen = productoExistente.Imagen;
            }

            // Convertir el ProductoViewModel a un objeto que pueda enviarse a la API
            var productoParaActualizar = Convertir(producto);

            // Enviar la solicitud de actualización a la API
            HttpResponseMessage response = _serviceRepository.PutResponse("api/Producto", Convertir(producto));

            if (!response.IsSuccessStatusCode)
            {
                // Obtener detalles del error
                throw new Exception($"Error al actualizar el producto: ");
            }

            var content = response.Content.ReadAsStringAsync().Result;
            var productoActualizado = JsonConvert.DeserializeObject<ProductoViewModel>(content);

            return productoActualizado;
        }

    }
}