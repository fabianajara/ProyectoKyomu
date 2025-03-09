namespace FronEnd.ApiModels
{
    public class PagoAPI
    {
        public int IdPago { get; set; }

        public int IdPedido { get; set; }

        public int IdMetodo { get; set; }

        public decimal Monto { get; set; }

        public DateTime FechaPago { get; set; }
    }
}
