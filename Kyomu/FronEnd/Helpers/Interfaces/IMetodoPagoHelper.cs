using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface IMetodoPagoHelper
    {
        List<MetodoPagoViewModel> GetMetodosPago();
        MetodoPagoViewModel GetMetodoPago(int? id);
        MetodoPagoViewModel Add(MetodoPagoViewModel metodoPago);
        MetodoPagoViewModel Update(MetodoPagoViewModel metodoPago);
        void Delete(int id);
    }
}
