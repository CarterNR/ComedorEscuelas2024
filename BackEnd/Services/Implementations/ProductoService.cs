using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Implementations;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services.Implementations
{
    public class ProductoService : IProductoService
    {
        IUnidadDeTrabajo Unidad;
        SisComedorContext context;
        public ProductoService(IUnidadDeTrabajo unidadDeTrabajo, SisComedorContext context)
        {
            this.Unidad = unidadDeTrabajo;
            this.context = context;
        }


        #region


        ProductoDTO Convertir(Producto producto)
        {
            return new ProductoDTO
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                Cantidad = producto.Cantidad,
                Estado = producto.Estado,
                Imagen = producto.Imagen,
                IdEscuela = producto.IdEscuela,
                IdProveedor = producto.IdProveedor
            };
        }

        Producto Convertir(ProductoDTO productoDTO)
        {
            return new Producto
            {
                IdProducto = productoDTO.IdProducto,
                NombreProducto = productoDTO.NombreProducto,
                Imagen = productoDTO.Imagen,  // Guardar la imagen en la base de datos
                IdProveedor = productoDTO.IdProveedor,
                IdEscuela = productoDTO.IdEscuela,
                Cantidad = productoDTO.Cantidad,
                Estado = productoDTO.Estado
            };
        }


        #endregion




        public void Agregar(ProductoDTO producto)
        {
            if (producto == null)
                throw new ArgumentNullException(nameof(producto));

            var entidad = new Producto
            {
                IdProducto = producto.IdProducto,
                NombreProducto = producto.NombreProducto,
                Cantidad = producto.Cantidad,
                Imagen = producto.Imagen, // Asegúrate de que sea un byte[]
                Estado = producto.Estado,
                IdProveedor = producto.IdProveedor,
                IdEscuela = producto.IdEscuela
            };

            context.Productos.Add(entidad); // Usa 'context' aquí
            context.SaveChanges();
        }



        public bool Editar(ProductoDTO producto)
        {
            // Convertir el DTO en la entidad Producto
            var productoEntity = Convertir(producto);

            // Actualizar la entidad en la base de datos
            Unidad.ProductoDAL.Update(productoEntity);

            // Guardar los cambios
            return Unidad.Complete();
        }

        



        public bool Eliminar(ProductoDTO productoDTO)
        {
            try
            {
                var producto = Convertir(productoDTO);
                var existente = Unidad.ProductoDAL.Get(producto.IdProducto);

                if (existente == null)
                {
                    Console.WriteLine($"Producto con ID {producto.IdProducto} no encontrado.");
                    return false;
                }

                var eliminado = Unidad.ProductoDAL.Remove(existente);
                if (eliminado)
                {
                    return Unidad.Complete();
                }
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error eliminando producto: {ex.Message}");
                return false;
            }
        }




        public ProductoDTO Obtener(int id)
        {
            return Convertir(Unidad.ProductoDAL.Get(id));
        }

        public List<ProductoDTO> Obtener()
        {
            List<ProductoDTO> list = new List<ProductoDTO>();
            var productos = Unidad.ProductoDAL.GetAll()
                .Join(
                    context.Proveedores,
                    producto => producto.IdProveedor,
                    proveedor => proveedor.IdProveedor,
                    (producto, proveedor) => new
                    {
                        Producto = producto,
                        Proveedor = proveedor
                    })
                .Join(
                    context.Escuelas,
                    joined => joined.Producto.IdEscuela,
                    escuela => escuela.IdEscuela,
                    (joined, escuela) => new
                    {
                        IdProducto = joined.Producto.IdProducto,
                        NombreProducto = joined.Producto.NombreProducto,
                        Cantidad = joined.Producto.Cantidad,
                        Estado = joined.Producto.Estado,
                        Imagen = joined.Producto.Imagen,
                        IdProveedor = joined.Producto.IdProveedor,
                        IdEscuela = joined.Producto.IdEscuela,
                        NombreProveedor = joined.Proveedor.NombreProveedor,
                        NombreEscuela = escuela.NombreEscuela
                    })
                .ToList();

            foreach (var item in productos)
            {
                var productoDTO = new ProductoDTO
                {
                    IdProducto = item.IdProducto,
                    NombreProducto = item.NombreProducto,
                    Cantidad = item.Cantidad,
                    Estado = item.Estado,
                    Imagen = item.Imagen,
                    IdProveedor = item.IdProveedor,
                    IdEscuela = item.IdEscuela,
                    NombreProveedor = item.NombreProveedor,
                    NombreEscuela = item.NombreEscuela
                };
                list.Add(productoDTO);
            }
            return list;
        }
    }
}
