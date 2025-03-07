using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Entities.Entities;

namespace DAL.Implementations
{
    public class PlatilloDAL : DALGenericoImpl<Platillo>, IPlatilloDAL
    {
        public KyomuContext _context;

        public PlatilloDAL(KyomuContext context) : base(context)
        {
            _context = context;

        }

        public List<Platillo> GetAllPlatillos()
        {
            return _context.Platillos
                .Select(e => new Platillo
                {
                    IdPlatillo = e.IdPlatillo,
                    Nombre = e.Nombre,
                    Descripcion = e.Descripcion,
                    Precio = e.Precio,
                    Imagen = e.Imagen,
                    IdCategoria = e.IdCategoria

                })
                .ToList();
        }

        public bool Add(Platillo entity)
        {
            try
            {
                _context.Platillos.Add(new Platillo
                {
                    IdPlatillo = entity.IdPlatillo,
                    Nombre = entity.Nombre,
                    Descripcion = entity.Descripcion,
                    Precio = entity.Precio,
                    Imagen = entity.Imagen,
                    IdCategoria = entity.IdCategoria
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
