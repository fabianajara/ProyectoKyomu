using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Models
{
    public class UsuarioViewModel
    {
        public int IdUsuario { get; set; }
        public int? IdRol { get; set; }
        public string Nombre { get; set; } = null!;
        public string? Telefono { get; set; }
        public string CorreoElectronico { get; set; } = null!;
        public string Contraseña { get; set; } = null!;
        public string? Direccion { get; set; }
        public string? Imagen { get; set; }

        public IEnumerable<SelectListItem>? RolesDisponibles { get; set; }
    }
}