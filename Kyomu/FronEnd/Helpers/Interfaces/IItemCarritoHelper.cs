using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface IItemCarritoHelper
    {
        ItemCarritoViewModel CreateItem(int idPlatillo, string nombre, decimal precio, int cantidad, string imagen);
        void UpdateCantidad(ItemCarritoViewModel item, int nuevaCantidad);
    }
}
