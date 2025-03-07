using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Entities.Entities;

namespace DAL.Implementations
{
    public class RolDAL : DALGenericoImpl<Rol>, IRolDAL
    {
        public KyomuContext _context;

        public RolDAL(KyomuContext context) : base(context)
        {
            _context = context;

        }

        public List<Rol> GetAllRoles()
        {
            return _context.Rols
                .Select(e => new Rol
                {
                    IdRol = e.IdRol,
                    NombreRol = e.NombreRol,
                    Descripcion = e.Descripcion

                })
                .ToList();
        }

        public bool Add(Rol entity)
        {
            try
            {
                _context.Rols.Add(new Rol
                {
                    IdRol = entity.IdRol,
                    NombreRol = entity.NombreRol,
                    Descripcion = entity.Descripcion
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