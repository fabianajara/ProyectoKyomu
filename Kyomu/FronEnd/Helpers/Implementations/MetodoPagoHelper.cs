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
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }
    }
}
