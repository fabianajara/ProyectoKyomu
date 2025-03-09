namespace FronEnd.Models
{
    public class ReseñaViewModel
    {
        public int IdReseña { get; set; }

        public int IdUsuario { get; set; }

        public int IdPlatillo { get; set; }

        public int Calificacion { get; set; }

        public string? Comentario { get; set; }
    }
}
