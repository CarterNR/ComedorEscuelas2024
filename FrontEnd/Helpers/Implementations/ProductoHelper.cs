using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Newtonsoft.Json;
using System.ComponentModel;

namespace FrontEnd.Helpers.Implementations
{
    public class ProductoHelper : IProductoHelper
    {
        IServiceRepository _ServiceRepository;

        Producto Convertir(ProductoViewModel producto)
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


        public ProductoHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public ProductoViewModel Add(ProductoViewModel producto)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Producto", Convertir(producto));
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Producto creado exitosamente. Respuesta del servidor: " + content);
            }
            else
            {
                var errorContent = response.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al crear el producto. Código de estado: " + response.StatusCode);
                Console.WriteLine("Detalles del error: " + errorContent);
                throw new Exception("Error al crear el producto: " + errorContent);
            }
            return producto;
        }



        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Producto/" + id.ToString());
            if (responseMessage.IsSuccessStatusCode) { var content = responseMessage.Content; }



        }

        public List<ProductoViewModel> GetProductos()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Producto");
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
                Proveedor proveedor = null;

                try
                {
                    HttpResponseMessage escuelaResponse = _ServiceRepository.GetResponse("api/Escuela/" + item.IdEscuela);
                    if (escuelaResponse.IsSuccessStatusCode)
                    {
                        var escuelaContent = escuelaResponse.Content.ReadAsStringAsync().Result;
                        escuela = JsonConvert.DeserializeObject<Escuela>(escuelaContent);
                    }

                    HttpResponseMessage proveedorResponse = _ServiceRepository.GetResponse("api/Proveedor/" + item.IdProveedor);
                    if (proveedorResponse.IsSuccessStatusCode)
                    {
                        var proveedorContent = proveedorResponse.Content.ReadAsStringAsync().Result;
                        proveedor = JsonConvert.DeserializeObject<Proveedor>(proveedorContent);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Error al obtener datos de la escuela/proveedor: {ex.Message}");
                }

                resultado.Add(new ProductoViewModel
                {
                    IdProducto = item.IdProducto,
                    NombreProducto = item.NombreProducto,
                    Cantidad = item.Cantidad,
                    Imagen = item.Imagen,
                    Estado = item.Estado,
                    IdProveedor = item.IdProveedor,
                    NombreProveedor = proveedor?.NombreProveedor ?? "Desconocido",
                    IdEscuela = item.IdEscuela,
                    NombreEscuela = escuela?.NombreEscuela ?? "Desconocido"
                });
            }

            return resultado;
        }


        public ProductoViewModel GetProducto(int? id)
        {
            if (id == null)
                return null;

            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Producto/" + id.ToString());
            Producto producto = new Producto();

            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                producto = JsonConvert.DeserializeObject<Producto>(content);
            }



            ProductoViewModel resultado = new ProductoViewModel
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                Cantidad = producto.Cantidad,
                Imagen = producto.Imagen,
                Estado = producto.Estado,
                IdProveedor = producto.IdProveedor,
                IdEscuela = producto.IdEscuela
            };

            return resultado;
        }







        public ProductoViewModel Update(ProductoViewModel producto)
        {
            HttpResponseMessage response = _ServiceRepository.PutResponse("api/Producto", Convertir(producto));

            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("No se pudo actualizar el producto.");
            }

            // Aquí asumimos que el servicio de la API devuelve el producto actualizado
            var content = response.Content.ReadAsStringAsync().Result;
            var productoActualizado = JsonConvert.DeserializeObject<ProductoViewModel>(content);

            return productoActualizado; // Retornamos el producto actualizado
        }
    }
}