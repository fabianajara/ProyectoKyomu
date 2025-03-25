using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Models
{
    public class PedidoViewModel
    {
        public int IdPedido { get; set; }
        public int IdUsuario { get; set; }
        public DateTime FechaPedido { get; set; }
        public DateTime? FechaEntrega { get; set; }
        public string? TipoEntrega { get; set; }
        public decimal Total { get; set; }
        public string Estado { get; set; } = null!;

        public IEnumerable<SelectListItem>? UsuariosDisponibles { get; set; }
        public IEnumerable<SelectListItem>? OpcionesEntrega { get; set; }
        public IEnumerable<SelectListItem>? EstadosDisponibles { get; set; }
    }
}


