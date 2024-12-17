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

        #region Conversiones
        private Producto Convertir(ProductoDTO producto)
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

        private ProductoDTO Convertir(Producto producto)
        {
            return new ProductoDTO
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
        #endregion

        public bool Agregar(ProductoDTO producto)
        {
            Producto entity = Convertir(producto);
            Unidad.ProductoDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(ProductoDTO producto)
        {
            var entity = Convertir(producto);
            Unidad.ProductoDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(ProductoDTO producto)
        {
            var entity = Convertir(producto);
            Unidad.ProductoDAL.Remove(entity);
            return Unidad.Complete();
        }

        public ProductoDTO Obtener(int id)
        {
            var producto = Unidad.ProductoDAL.Get(id);
            return Convertir(producto);
        }

        public List<ProductoDTO> Obtener()
        {
            var productos = Unidad.ProductoDAL.GetAll()
                .Join(
                    context.Proveedores,
                    producto => producto.IdProveedor,
                    proveedor => proveedor.IdProveedor,
                    (producto, proveedor) => new { Producto = producto, Proveedor = proveedor }
                )
                .Join(
                    context.Escuelas,
                    joined => joined.Producto.IdEscuela,
                    escuela => escuela.IdEscuela,
                    (joined, escuela) => new ProductoDTO
                    {
                        IdProducto = joined.Producto.IdProducto,
                        NombreProducto = joined.Producto.NombreProducto,
                        Cantidad = joined.Producto.Cantidad,
                        Imagen = joined.Producto.Imagen,
                        Estado = joined.Producto.Estado,
                        IdProveedor = joined.Producto.IdProveedor,
                        IdEscuela = joined.Producto.IdEscuela,
                        NombreProveedor = joined.Proveedor.NombreProveedor,
                        NombreEscuela = escuela.NombreEscuela
                    }
                ).ToList();

            return productos;
        }
    }
}
