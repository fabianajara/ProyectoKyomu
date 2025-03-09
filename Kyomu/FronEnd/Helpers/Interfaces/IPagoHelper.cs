using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface IPagoHelper
    {
        List<PagoViewModel> GetPagos();
        PagoViewModel GetPago(int? id);
        PagoViewModel Add(PagoViewModel pago);
        PagoViewModel Update(PagoViewModel pago);
        void Delete(int id);
    }
}
