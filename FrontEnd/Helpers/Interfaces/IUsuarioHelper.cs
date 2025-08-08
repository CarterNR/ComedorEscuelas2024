using FrontEnd.Models;

namespace FrontEnd.Helpers.Interfaces
{
    public interface IUsuarioHelper
    {
        List<UsuarioViewModel> GetUsuarios();

        UsuarioViewModel GetUsuario(int? id);
        UsuarioViewModel Add(UsuarioViewModel usuario);
        UsuarioViewModel Update(UsuarioViewModel usuario);
        void Delete(int id);
        void Desactivar(int id);

        UsuarioViewModel Autenticar(string nombreUsuario, string clave);



    }
}