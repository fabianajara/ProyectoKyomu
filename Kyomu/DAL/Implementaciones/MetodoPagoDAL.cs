using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class MetodoPagoDAL : DALGenericoImpl<MetodoPago>, IMetodoPagoDAL
    {
        private readonly KyomuContext _context;

        public MetodoPagoDAL(KyomuContext context) : base(context)
        {
            _context = context;
        }

        public List<MetodoPago> GetAllMetodosPago()
        {
            return _context.MetodoPagos
                .Select(m => new MetodoPago
                {
                    IdMetodo = m.IdMetodo,
                    TipoMetodo = m.TipoMetodo
                })
                .ToList();
        }
    }
}
