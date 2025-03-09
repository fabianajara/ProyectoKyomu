using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface ICategoriaHelper
    {

        List<CategoriaViewModel> GetCategorias();

        CategoriaViewModel GetCategoria(int? id);
        CategoriaViewModel Add(CategoriaViewModel category);
        CategoriaViewModel Update(CategoriaViewModel category);
        void Delete(int id);
    }
}
