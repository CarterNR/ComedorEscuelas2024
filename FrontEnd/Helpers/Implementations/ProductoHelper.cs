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
            HttpResponseMessage response = _serviceRepository.DeleteResponse($"api/Producto/{id}");
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception("No se puede eliminar el producto porque está siendo utilizado");
            }
        }

        public ProductoViewModel GetProducto(int? id)
        {
            HttpResponseMessage response = _serviceRepository.GetResponse($"api/Producto/{id}");
            if (!response.IsSuccessStatusCode) return null;

            var content = response.Content.ReadAsStringAsync().Result;
            var producto = JsonConvert.DeserializeObject<Producto>(content);

            return new ProductoViewModel
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
        }

        public List<ProductoViewModel> GetProductos()
        {
            HttpResponseMessage response = _serviceRepository.GetResponse("api/Producto");
            if (!response.IsSuccessStatusCode) return new List<ProductoViewModel>();

            var content = response.Content.ReadAsStringAsync().Result;
            var productos = JsonConvert.DeserializeObject<List<Producto>>(content);

            return productos.Select(p => new ProductoViewModel
            {
                IdProducto = p.IdProducto,
                NombreProducto = p.NombreProducto,
                Cantidad = p.Cantidad,
                Imagen = p.Imagen,
                Estado = p.Estado,
                IdProveedor = p.IdProveedor,
                IdEscuela = p.IdEscuela,
                NombreProveedor = p.NombreProveedor,
                NombreEscuela = p.NombreEscuela
            }).ToList();
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
            HttpResponseMessage response = _serviceRepository.PutResponse("api/Producto", productoParaActualizar);

            if (!response.IsSuccessStatusCode)
            {
                // Obtener detalles del error
                var errorContent = response.Content.ReadAsStringAsync().Result;
                throw new Exception($"Error al actualizar el producto: {errorContent}");
            }

            return producto;
        }
    }
}