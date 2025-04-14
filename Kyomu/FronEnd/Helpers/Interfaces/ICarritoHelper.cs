using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface ICarritoHelper
    {
        CarritoViewModel GetCarrito();
        void AddItem(int idPlatillo, int cantidad);
        void RemoveItem(int idPlatillo);
        void ClearCarrito();
        decimal GetTotal();
    }
}
