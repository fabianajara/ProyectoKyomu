using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Models
{
    public class PagoViewModel
    {
        public int IdPago { get; set; }

        public int IdPedido { get; set; }

        public int IdMetodo { get; set; }

        public decimal Monto { get; set; }

        public DateTime FechaPago { get; set; }

        public IEnumerable<SelectListItem>? PedidosDisponibles { get; set; }
        public IEnumerable<SelectListItem>? MetodosPagoDisponibles { get; set; }
    }
}
