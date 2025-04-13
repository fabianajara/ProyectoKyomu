using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface IUsuarioHelper
    {
        List<UsuarioViewModel> GetUsuarios();
        UsuarioViewModel GetUsuario(int? id);
        UsuarioViewModel Add(UsuarioViewModel usuario);
        UsuarioViewModel Update(UsuarioViewModel usuario);
        UsuarioViewModel Login(string correo, string contraseña);
        UsuarioViewModel GetUsuarioByCorreo(string correo);
        void Delete(int id);
    }
}
