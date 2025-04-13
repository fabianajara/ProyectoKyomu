using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface IUsuarioHelper
    {
        List<UsuarioViewModel> GetUsuarios();

        UsuarioViewModel GetUsuario(int? id);
        UsuarioViewModel Add(UsuarioViewModel usuario);
        UsuarioViewModel Update(UsuarioViewModel usuario);
        void Delete(int id);
    }
}
