namespace FronEnd.ApiModels
{
    public class DetallePedidoAPI
    {
        public int IdDetallePedido { get; set; }

        public int IdPedido { get; set; }

        public int IdPlatillo { get; set; }

        public int Cantidad { get; set; }
    }
}
