using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Models
{
    public class DetallePedidoViewModel
    {
        public int IdDetallePedido { get; set; }

        public int IdPedido { get; set; }

        public int IdPlatillo { get; set; }

        public int Cantidad { get; set; }

        public IEnumerable<SelectListItem>? Pedidos { get; set; }
        public IEnumerable<SelectListItem>? Platillos { get; set; }

    }
}
