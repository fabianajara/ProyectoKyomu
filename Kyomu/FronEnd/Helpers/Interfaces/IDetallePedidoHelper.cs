using FronEnd.Models;

namespace FronEnd.Helpers.Interfaces
{
    public interface IDetallePedidoHelper
    {

        List<DetallePedidoViewModel> GetDetallePedidos();

        DetallePedidoViewModel GetDetallePedido(int? id);
        DetallePedidoViewModel Add(DetallePedidoViewModel detallePedido);
        DetallePedidoViewModel Update(DetallePedidoViewModel detallePedido);
        void Delete(int id);
    }
}
