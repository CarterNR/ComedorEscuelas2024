using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface IBitacoraService
    {
        bool Agregar(BitacoraDTO bitacora);
        bool Editar(BitacoraDTO bitacora);
        bool Eliminar(BitacoraDTO bitacora);
        BitacoraDTO Obtener(int id);
        List<BitacoraDTO> Obtener();
    }
}
