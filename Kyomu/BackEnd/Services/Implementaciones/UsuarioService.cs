using BackEnd.DTO;
using BackEnd.Services.Interfaces;
using DAL.Interfaces;
using Entities.Entities;
using Microsoft.EntityFrameworkCore;

namespace BackEnd.Services.Implementations
{
    public class UsuarioService : IUsuarioService
    {
        IUnidadDeTrabajo _unidadDeTrabajo;

        public UsuarioService(IUnidadDeTrabajo unidadDeTrabajo)
        {
            _unidadDeTrabajo = unidadDeTrabajo;
        }

        Usuario Convertir(UsuarioDTO usuario)
        {
            return new Usuario
            {
                IdUsuario = usuario.IdUsuario,
                IdRol = usuario.IdRol,
                Nombre = usuario.Nombre,
                Telefono = usuario.Telefono,
                CorreoElectronico = usuario.CorreoElectronico,
                Contraseña = usuario.Contraseña,
                Direccion = usuario.Direccion,
                Imagen = usuario.Imagen

            };
        }

        UsuarioDTO Convertir(Usuario usuario)
        {
            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                IdRol = usuario.IdRol,
                Nombre = usuario.Nombre,
                Telefono = usuario.Telefono,
                CorreoElectronico = usuario.CorreoElectronico,
                Contraseña = usuario.Contraseña,
                Direccion = usuario.Direccion,
                Imagen = usuario.Imagen
            };
        }

        // Login
        public UsuarioDTO Login(string correo, string contraseña)
        {
            var usuario = _unidadDeTrabajo.UsuarioDAL.GetAll()
                .FirstOrDefault(u => u.CorreoElectronico == correo && u.Contraseña == contraseña);

            if (usuario == null) return null;

            return new UsuarioDTO
            {
                IdUsuario = usuario.IdUsuario,
                IdRol = usuario.IdRol,
                Nombre = usuario.Nombre,
                Telefono = usuario.Telefono,
                CorreoElectronico = usuario.CorreoElectronico,
                Contraseña = usuario.Contraseña,
                Direccion = usuario.Direccion,
                Imagen = usuario.Imagen
            };
        }


        // Registro
        public bool Registro(UsuarioDTO usuarioDTO)
        {
            // Verificar si ya existe un usuario con el mismo correo
            var usuarioExistente = _unidadDeTrabajo.UsuarioDAL.GetAll()
                .FirstOrDefault(u => u.CorreoElectronico == usuarioDTO.CorreoElectronico);

            if (usuarioExistente != null)
            {
                return false;  // El correo ya está registrado
            }

            // Crear el nuevo usuario
            var nuevoUsuario = new Usuario
            {
                Nombre = usuarioDTO.Nombre,
                CorreoElectronico = usuarioDTO.CorreoElectronico,
                Contraseña = usuarioDTO.Contraseña, // Aquí debes usar hash para la contraseña
                Telefono = usuarioDTO.Telefono,
                Direccion = usuarioDTO.Direccion,
                Imagen = usuarioDTO.Imagen
            };

            _unidadDeTrabajo.UsuarioDAL.Add(nuevoUsuario);
            _unidadDeTrabajo.Complete();
            return true;  // Registro exitoso
        }

        public UsuarioDTO GetUsuarioByCorreo(string correo)
        {
            var usuario = _unidadDeTrabajo.UsuarioDAL.GetAllUsuarios()
                            .FirstOrDefault(u => u.CorreoElectronico == correo);
            return usuario == null ? null : Convertir(usuario);
        }

        public void Add(UsuarioDTO usuario)
        {
            var usuarioEntity = Convertir(usuario);
            _unidadDeTrabajo.UsuarioDAL.Add(usuarioEntity);
            _unidadDeTrabajo.Complete();
        }

        public void Delete(int id)
        {
            var usuario = new Usuario { IdUsuario = id };
            _unidadDeTrabajo.UsuarioDAL.Remove(usuario);
            _unidadDeTrabajo.Complete();
        }

        public UsuarioDTO GetUsuarioById(int id)
        {
            var result = _unidadDeTrabajo.UsuarioDAL.Get(id);
            return Convertir(result);
        }

        public List<UsuarioDTO> GetUsuarios()
        {
            var result = _unidadDeTrabajo.UsuarioDAL.GetAll();
            List<UsuarioDTO> usuarios = new List<UsuarioDTO>();
            foreach (var item in result)
            {
                usuarios.Add(Convertir(item));
            }
            return usuarios;
        }

        public void Update(UsuarioDTO usuario)
        {
            var usuarioEntity = Convertir(usuario);
            _unidadDeTrabajo.UsuarioDAL.Update(usuarioEntity);
            _unidadDeTrabajo.Complete();
        }
    }
}
