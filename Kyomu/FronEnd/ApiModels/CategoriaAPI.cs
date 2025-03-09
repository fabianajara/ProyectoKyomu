namespace FronEnd.ApiModels 
{
    public class CategoriaAPI
    {
        public int IdCategoria { get; set; }

        public string NombreCategoria { get; set; } = null!;

        public string? Descripcion { get; set; }
    }
}
