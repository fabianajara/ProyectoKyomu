using FronEnd.Helpers.Interfaces;
using FronEnd.Models;

namespace FronEnd.Helpers.Implementations
{
    public class ItemCarritoHelper : IItemCarritoHelper
    {
        public ItemCarritoViewModel CreateItem(int idPlatillo, string nombre, decimal precio, int cantidad, string imagen)
        {
            return new ItemCarritoViewModel
            {
                IdPlatillo = idPlatillo,
                Nombre = nombre,
                Precio = precio,
                Cantidad = cantidad,
                Imagen = imagen
            };
        }

        public void UpdateCantidad(ItemCarritoViewModel item, int nuevaCantidad)
        {
            if (item != null)
            {
                item.Cantidad = nuevaCantidad;
            }
        }
    }
}
