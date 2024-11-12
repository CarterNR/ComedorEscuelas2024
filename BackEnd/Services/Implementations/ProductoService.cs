using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

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
        Producto Convertir(ProductoDTO producto)
        {
            return new Producto
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
