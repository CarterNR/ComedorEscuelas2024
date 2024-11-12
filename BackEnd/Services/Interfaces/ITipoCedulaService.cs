using BackEnd.DTO;

namespace BackEnd.Services.Interfaces
{
    public interface ITipoCedulaService
    {
        bool Agregar(TipoCedulaDTO tipoCedula);
        bool Editar(TipoCedulaDTO tipoCedula);
        bool Eliminar(TipoCedulaDTO tipoCedula);
        TipoCedulaDTO Obtener(int id);
        List<TipoCedulaDTO> Obtener();
    }
}
