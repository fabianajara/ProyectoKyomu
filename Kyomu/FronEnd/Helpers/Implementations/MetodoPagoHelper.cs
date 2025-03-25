using FronEnd.ApiModels;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Newtonsoft.Json;


namespace FronEnd.Helpers.Implementations
{
    public class MetodoPagoHelper : IMetodoPagoHelper
    {
        IServiceRepository _ServiceRepository;

        MetodoPagoViewModel Convertir(MetodoPagoAPI metodoPago)
        {
            MetodoPagoViewModel metodoPagoViewModel = new MetodoPagoViewModel
            {
                IdMetodo = metodoPago.IdMetodo,
                TipoMetodo = metodoPago.TipoMetodo
            };
            return metodoPagoViewModel;
        }


        public MetodoPagoHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public MetodoPagoViewModel Add(MetodoPagoViewModel metodoPago)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/MetodoPago", metodoPago);
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return metodoPago;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/MetodoPago/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
        }

        public List<MetodoPagoViewModel> GetMetodosPago()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/MetodoPago");
            List<MetodoPagoAPI> metodosPago = new List<MetodoPagoAPI>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                metodosPago = JsonConvert.DeserializeObject<List<MetodoPagoAPI>>(content);
            }
            List<MetodoPagoViewModel> lista = new List<MetodoPagoViewModel>();
            foreach (var metodoPago in metodosPago)
            {
                lista.Add(Convertir(metodoPago));
            }   
            return lista;
        }

        public MetodoPagoViewModel GetMetodoPago(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/MetodoPago/" + id.ToString());
            MetodoPagoAPI metodoPago = new MetodoPagoAPI();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                metodoPago = JsonConvert.DeserializeObject<MetodoPagoAPI>(content);
            }

            MetodoPagoViewModel resultado = Convertir(metodoPago);


            return resultado;
        }

        public MetodoPagoViewModel Update(MetodoPagoViewModel metodoPago)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.PutResponse("api/MetodoPago/" + metodoPago.IdMetodo.ToString(), metodoPago);
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                MetodoPagoAPI updatedmetodoPago = JsonConvert.DeserializeObject<MetodoPagoAPI>(content);
                return Convertir(updatedmetodoPago);
            }
            else
            {

                var errorMessage = responseMessage.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al actualizar el método de pago: " + errorMessage);
                return null;
            }
        }
    }
}
