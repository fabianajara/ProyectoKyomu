using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Entities.Entities;

namespace DAL.Implementations
{
    public class UsuarioDAL : DALGenericoImpl<Usuario>, IUsuarioDAL
    {
        public KyomuContext _context;

        public UsuarioDAL(KyomuContext context) : base(context)
        {
            _context = context;

        }

        public List<Usuario> GetAllUsuarios()
        {
            return _context.Usuarios
                .Select(e => new Usuario
                {
                    IdUsuario = e.IdUsuario,
                    IdRol = e.IdRol,
                    Nombre = e.Nombre,
                    Telefono = e.Telefono,
                    CorreoElectronico = e.CorreoElectronico,
                    Contraseña = e.Contraseña,
                    Direccion = e.Direccion,
                    Imagen = e.Imagen

                })
                .ToList();
        }

        public bool Add(Usuario entity)
        {
            try
            {
                _context.Usuarios.Add(new Usuario
                {
                    IdUsuario = entity.IdUsuario,
                    IdRol = entity.IdRol,
                    Nombre = entity.Nombre,
                    Telefono = entity.Telefono,
                    CorreoElectronico = entity.CorreoElectronico,
                    Contraseña = entity.Contraseña,
                    Direccion = entity.Direccion,
                    Imagen = entity.Imagen
                });
                _context.SaveChanges();
                return true;
            }
            catch (Exception e)
            {

                return false;
            }
        }
    }
}