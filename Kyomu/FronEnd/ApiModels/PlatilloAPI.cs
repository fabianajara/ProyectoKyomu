using System.ComponentModel.DataAnnotations;

namespace FronEnd.ApiModels
{
    public class PlatilloAPI
    {
        public int IdPlatillo { get; set; }

        public string Nombre { get; set; } = null!;

        public string? Descripcion { get; set; }

        public decimal Precio { get; set; }

        public string? Imagen { get; set; }

        public int? IdCategoria { get; set; }
    }
}
