using DAL.Interfaces;
using Entities.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Implementations
{
    public class UnidadDeTrabajo : IUnidadDeTrabajo
    {
        public IPagoDAL PagoDAL { get; set; }
        public IPedidoDAL PedidoDAL { get; set; }
        public ICategoriaDAL CategoriaDAL { get; set; }
        public IPlatilloDAL PlatilloDAL { get; set; }
        public IReseñaDAL ReseñaDAL { get; set; }
        public IRolDAL RolDAL { get; set; }
        public IUsuarioDAL UsuarioDAL { get; set; }
        public IDetalleDAL DetalleDAL { get; set; }
        public IMetodoPagoDAL MetodoPagoDAL { get; set; }

        private KyomuContext _context;

        public UnidadDeTrabajo(KyomuContext context,
                        IPagoDAL pagoDAL,
                        IPedidoDAL pedidoDAL,
                        ICategoriaDAL categoriaDAL,
                        IPlatilloDAL platilloDAL,
                        IReseñaDAL reseñaDAL,
                        IRolDAL rolDAL,
                        IUsuarioDAL usuarioDAL,
                        IDetalleDAL detalleDAL,
                        IMetodoPagoDAL metodoPagoDAL)
                        

            {
                this._context = context;
                this.PagoDAL = pagoDAL;
                 this.PedidoDAL = pedidoDAL;
                this.CategoriaDAL = categoriaDAL;
            this.ReseñaDAL = reseñaDAL;
            this.PlatilloDAL = platilloDAL;
            this.RolDAL = rolDAL;
            this.UsuarioDAL = usuarioDAL;
            this.DetalleDAL = detalleDAL;
            this.MetodoPagoDAL = metodoPagoDAL;


        }
       

        public bool Complete()
        {
            try
            {
                return _context.SaveChanges() > 0;
            }
            catch (Exception ex)
            {
                // Loggear el error (necesitarías inyectar ILogger)
                Console.WriteLine($"Error al guardar cambios: {ex.Message}");
                if (ex.InnerException != null)
                {
                    Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
                }
                return false;
            }
        }

        public void Dispose()
        {
            this._context.Dispose();
        }
    }
}
