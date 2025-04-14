namespace FronEnd.Models
{
    public class ItemCarritoViewModel
    {
        public int IdPlatillo { get; set; }
        public string Nombre { get; set; } = null!;
        public decimal Precio { get; set; }
        public int Cantidad { get; set; }
        public decimal Subtotal => Precio * Cantidad;
        public string? Imagen { get; set; }
    }
}
