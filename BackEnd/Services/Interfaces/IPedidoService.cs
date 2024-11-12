using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IPedidoService
    {
        bool Agregar(PedidoDTO pedido);
        bool Editar(PedidoDTO pedido);
        bool Eliminar(PedidoDTO pedido);
        PedidoDTO Obtener(int id);
        List<PedidoDTO> Obtener();
    }
}
