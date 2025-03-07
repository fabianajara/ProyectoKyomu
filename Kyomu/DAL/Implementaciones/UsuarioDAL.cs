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
            throw new NotImplementedException();
        }
    }
}
