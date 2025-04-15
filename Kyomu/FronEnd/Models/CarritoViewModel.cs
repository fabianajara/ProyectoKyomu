using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Models
{
    public class CarritoViewModel
    {
        public List<ItemCarritoViewModel> Items { get; set; } = new List<ItemCarritoViewModel>();
        public decimal Total => Items.Sum(i => i.Subtotal);
        public IEnumerable<SelectListItem>? MetodosPagoDisponibles { get; set; }
        public IEnumerable<SelectListItem>? OpcionesEntrega { get; set; }
        public int IdMetodoSeleccionado { get; set; }
        public string? TipoEntregaSeleccionado { get; set; }
    }
}
