using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface IReseñaHelper
    {
        List<ReseñaViewModel> GetReseñas();

        ReseñaViewModel GetReseña(int? id);
        ReseñaViewModel Add(ReseñaViewModel reseña);
        ReseñaViewModel Update(ReseñaViewModel reseña);
        void Delete(int id);
    }
}
