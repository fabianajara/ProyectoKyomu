using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface IPlatilloHelper
    {
        List<PlatilloViewModel> GetPlatillos();

        PlatilloViewModel GetPlatillo(int? id);
        PlatilloViewModel Add(PlatilloViewModel platillo);
        PlatilloViewModel Update(PlatilloViewModel platillo);
        void Delete(int id);
    }
}
