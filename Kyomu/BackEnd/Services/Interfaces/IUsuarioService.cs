using BackEnd.DTO;
using Entities.Entities;

namespace BackEnd.Services.Interfaces
{
    public interface IUsuarioService
    {
        List<UsuarioDTO> GetUsuarios();
        void Add(UsuarioDTO usuario);
        void Update(UsuarioDTO usuario);
        void Delete(int id);
        UsuarioDTO GetUsuarioById(int id);
        UsuarioDTO Login(string correoElectronico, string contraseña);
        bool Registro(UsuarioDTO usuarioDTO);
        UsuarioDTO GetUsuarioByCorreo(string correo);

    }
}
