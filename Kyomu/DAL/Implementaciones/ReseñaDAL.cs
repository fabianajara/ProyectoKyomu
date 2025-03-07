using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Interfaces;
using Entities.Entities;

namespace DAL.Implementations
{
    public class ReseñaDAL : DALGenericoImpl<Reseña>, IReseñaDAL
    {
        public KyomuContext _context;

        public ReseñaDAL(KyomuContext context) : base(context)
        {
            _context = context;

        }

        public List<Reseña> GetAllReseñas ()
        {
            return _context.Reseñas
                .Select(e => new Reseña
                {
                    IdReseña = e.IdReseña,
                    IdUsuario = e.IdUsuario,
                    IdPlatillo = e.IdPlatillo,
                    Calificacion = e.Calificacion,
                    Comentario = e.Comentario

                })
                .ToList();
        }

        public bool Add(Reseña entity)
        {
            try
            {
                _context.Reseñas.Add(new Reseña
                {
                    IdReseña = entity.IdReseña,
                    IdUsuario = entity.IdUsuario,
                    IdPlatillo = entity.IdPlatillo,
                    Calificacion = entity.Calificacion,
                    Comentario = entity.Comentario
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
