using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using System.Linq;

namespace BackEnd.Services.Implementations
{
    public class ProductoDiaService : IProductoDiaService
    {
        IUnidadDeTrabajo Unidad;
        SisComedorContext context;
        public ProductoDiaService(IUnidadDeTrabajo unidadDeTrabajo, SisComedorContext context)
        {
            this.Unidad = unidadDeTrabajo;
            this.context = context;
        }


        #region
        ProductosDium Convertir(ProductoDiaDTO productoDia)
        {
            return new ProductosDium
            {
                IdProductoDia = productoDia.IdProductoDia,
                IdProducto = productoDia.IdProducto,
                Cantidad = productoDia.Cantidad,
                Fecha = productoDia.Fecha,
                Estado = productoDia.Estado,
                IdEscuela = productoDia.IdEscuela
            };
        }

        ProductoDiaDTO Convertir(ProductosDium productoDia)
        {
            return new ProductoDiaDTO
            {
                IdProductoDia = productoDia.IdProductoDia,
                IdProducto = productoDia.IdProducto,
                Cantidad = productoDia.Cantidad,
                Fecha = productoDia.Fecha,
                Estado = productoDia.Estado,
                IdEscuela = productoDia.IdEscuela
            };
        }
        #endregion




        public bool Agregar(ProductoDiaDTO productoDia)
        {
            ProductosDium entity = Convertir(productoDia);
            Unidad.ProductoDiaDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(ProductoDiaDTO productoDia)
        {
            var entity = Convertir(productoDia);
            Unidad.ProductoDiaDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(ProductoDiaDTO productoDia)
        {
            var entity = Convertir(productoDia);
            Unidad.ProductoDiaDAL.Remove(entity);
            return Unidad.Complete();
        }

        public ProductoDiaDTO Obtener(int id)
        {
            return Convertir(Unidad.ProductoDiaDAL.Get(id));
        }

        public List<ProductoDiaDTO> Obtener()
        {
            List<ProductoDiaDTO> list = new List<ProductoDiaDTO>();
            var productosDias = Unidad.ProductoDiaDAL.GetAll()
                .Join(
                    context.Productos,
                    productoDia => productoDia.IdProducto,
                    producto => producto.IdProducto,
                    (productoDia, producto) => new
                    {
                        ProductoDia = productoDia,
                        Producto = producto
                    })
                .Join(
                    context.Escuelas,
                    joined => joined.ProductoDia.IdEscuela,
                    escuela => escuela.IdEscuela,
                    (joined, escuela) => new
                    {
                        IdProductoDia = joined.ProductoDia.IdProductoDia,
                        IdProducto = joined.ProductoDia.IdProducto,
                        NombreProducto = joined.Producto.NombreProducto,
                        Cantidad = joined.ProductoDia.Cantidad,
                        Fecha = joined.ProductoDia.Fecha,
                        Estado = joined.ProductoDia.Estado,
                        IdEscuela = joined.ProductoDia.IdEscuela,
                        NombreEscuela = escuela.NombreEscuela
                    })
                .ToList();
            foreach (var item in productosDias)
            {
                var productoDiaDTO = new ProductoDiaDTO
                {
                    IdProductoDia = item.IdProductoDia,
                    IdProducto = item.IdProducto,
                    NombreProducto = item.NombreProducto,
                    Cantidad = item.Cantidad,
                    Fecha = item.Fecha,
                    Estado = item.Estado,
                    IdEscuela = item.IdEscuela,
                    NombreEscuela = item.NombreEscuela
                };
                list.Add(productoDiaDTO);
            }
            return list;
        }
    }
}
