using FronEnd.ApiModels;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Newtonsoft.Json;


namespace FronEnd.Helpers.Implementations
{
    public class PagoHelper : IPagoHelper
    {
        IServiceRepository _ServiceRepository;

        PagoViewModel Convertir(PagoAPI pago)
        {
            PagoViewModel pagoViewModel = new PagoViewModel
            {
                IdPago = pago.IdPago,
                IdPedido = pago.IdPedido,
                IdMetodo = pago.IdMetodo,
                Monto = pago.Monto,
                FechaPago = pago.FechaPago
            };
            return pagoViewModel;
        }


        public PagoHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public PagoViewModel Add(PagoViewModel pago)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Pago", pago);
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return pago;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Pago" + id.ToString());
            _ = _ServiceRepository.DeleteResponse("api/Pago/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
        }

        public List<PagoViewModel> GetPagos()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Pago");
            List<PagoAPI> pagos = new List<PagoAPI>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                pagos = JsonConvert.DeserializeObject<List<PagoAPI>>(content);
            }
            List<PagoViewModel> lista = new List<PagoViewModel>();
            foreach (var pago in pagos)
            {
                lista.Add(Convertir(pago));
            }   
            return lista;
        }

        public PagoViewModel GetPago(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Pago/" + id.ToString());
            PagoAPI pago = new PagoAPI();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                pago = JsonConvert.DeserializeObject<PagoAPI>(content);
            }

            PagoViewModel resultado = Convertir(pago);


            return resultado;
        }

        public PagoViewModel Update(PagoViewModel pago)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.PutResponse("api/Pago/" + pago.IdPago.ToString(), pago);
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                PagoAPI updatedPago = JsonConvert.DeserializeObject<PagoAPI>(content);
                return Convertir(updatedPago);
            }
            else
            {

                var errorMessage = responseMessage.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al actualizar el pago: " + errorMessage);
                return null;
            }
        }
    }
}
