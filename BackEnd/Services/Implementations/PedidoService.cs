using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;

namespace BackEnd.Services.Implementations
{
    public class PedidoService : IPedidoService
    {
        IUnidadDeTrabajo Unidad;
        SisComedorContext context;
        public PedidoService(IUnidadDeTrabajo unidadDeTrabajo, SisComedorContext context)
        {
            this.Unidad = unidadDeTrabajo;
            this.context = context;
        }


        #region
        Pedido Convertir(PedidoDTO pedido)
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

        PedidoDTO Convertir(Pedido pedido)
        {
            return new PedidoDTO
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
        #endregion





        public bool Agregar(PedidoDTO pedido)
        {
            Pedido entity = Convertir(pedido);
            Unidad.PedidoDAL.Add(entity);
            return Unidad.Complete();
        }

        public bool Editar(PedidoDTO pedido)
        {
            var entity = Convertir(pedido);
            Unidad.PedidoDAL.Update(entity);
            return Unidad.Complete();
        }

        public bool Eliminar(PedidoDTO pedido)
        {
            var entity = Convertir(pedido);
            Unidad.PedidoDAL.Remove(entity);
            return Unidad.Complete();
        }

        public PedidoDTO Obtener(int id)
        {
            return Convertir(Unidad.PedidoDAL.Get(id));
        }

        public List<PedidoDTO> Obtener()
        {
            List<PedidoDTO> list = new List<PedidoDTO>();
            var pedidos = Unidad.PedidoDAL.GetAll()
                .Join(
                    context.Productos,
                    pedido => pedido.IdProducto,
                    producto => producto.IdProducto,
                    (pedido, producto) => new
                    {
                        Pedido = pedido,
                        Producto = producto
                    })
                .Join(
                    context.Proveedores,
                    joined => joined.Producto.IdProveedor,
                    proveedor => proveedor.IdProveedor,
                    (joined, proveedor) => new
                    {
                        Pedido = joined.Pedido,
                        Producto = joined.Producto,
                        Proveedor = proveedor
                    })
                .Join(
                    context.Usuarios,
                    joined => joined.Pedido.IdUsuario,  // Aquí está la corrección
                    usuario => usuario.IdUsuario,
                    (joined, usuario) => new
                    {
                        Pedido = joined.Pedido,
                        Producto = joined.Producto,
                        Proveedor = joined.Proveedor,
                        Usuario = usuario
                    })
                .Join(
                    context.EstadoPedidos,
                    joined => joined.Pedido.IdEstadoPedido,  // Aquí está la corrección
                    estadoPedido => estadoPedido.IdEstadoPedido,
                    (joined, estadoPedido) => new
                    {
                        Pedido = joined.Pedido,
                        Producto = joined.Producto,
                        Proveedor = joined.Proveedor,
                        Usuario = joined.Usuario,
                        EstadoPedido = estadoPedido
                    })
                .Join(
                    context.Escuelas,
                    joined => joined.Pedido.IdEscuela,
                    escuela => escuela.IdEscuela,
                    (joined, escuela) => new
                    {
                        IdPedido = joined.Pedido.IdPedido,

                        IdProducto = joined.Pedido.IdProducto,
                        NombreProducto = joined.Producto.NombreProducto,

                        FechaHoraIngreso = joined.Pedido.FechaHoraIngreso,
                        Cantidad = joined.Pedido.Cantidad,

                        IdUsuario = joined.Pedido.IdUsuario,
                        NombreCompleto = joined.Usuario.NombreCompleto,


                        IdEscuela = joined.Pedido.IdEscuela,
                        NombreEscuela = escuela.NombreEscuela,

                        IdEstadoPedido = joined.Pedido.IdEstadoPedido,
                        EstadoPedido1 = joined.EstadoPedido.EstadoPedido1,

                        NombreProveedor = joined.Proveedor.NombreProveedor,
                        Estado = joined.Usuario.Estado

                    })
                .ToList();

            foreach (var item in pedidos)
            {
                var pedidoDTO = new PedidoDTO
                {
                    IdPedido = item.IdPedido,

                    IdProducto = item.IdProducto,
                    NombreProducto = item.NombreProducto,

                    FechaHoraIngreso = item.FechaHoraIngreso,
                    Cantidad = item.Cantidad,

                    IdUsuario = item.IdUsuario,
                    NombreCompleto = item.NombreCompleto,


                    IdEscuela = item.IdEscuela,
                    NombreEscuela = item.NombreEscuela,

                    IdEstadoPedido = item.IdEstadoPedido,
                    EstadoPedido1 = item.EstadoPedido1,

                    NombreProveedor = item.NombreProveedor,
                    Estado = item.Estado
                };
                list.Add(pedidoDTO);
            }
            return list;
        }
    }
}
