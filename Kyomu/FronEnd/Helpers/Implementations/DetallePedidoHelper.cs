using FronEnd.ApiModels;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Newtonsoft.Json;


namespace FronEnd.Helpers.Implementations
{
    public class DetallePedidoHelper : IDetallePedidoHelper
    {
        IServiceRepository _ServiceRepository;

        DetallePedidoViewModel Convertir(DetallePedidoAPI detallePedido)
        {
            DetallePedidoViewModel detallePedidoViewModel = new DetallePedidoViewModel
            {
                IdDetallePedido = detallePedido.IdDetallePedido,
                IdPedido = detallePedido.IdPedido,
                IdPlatillo = detallePedido.IdPlatillo,
                Cantidad = detallePedido.Cantidad
            };
            return detallePedidoViewModel;
        }


        public DetallePedidoHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }

        public DetallePedidoViewModel Add(DetallePedidoViewModel detallePedido)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/DetallePedido", detallePedido);
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return detallePedido;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public List<DetallePedidoViewModel> GetDetallePedidos()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/DetallePedido");
            List<DetallePedidoAPI> detallePedidos = new List<DetallePedidoAPI>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                detallePedidos = JsonConvert.DeserializeObject<List<DetallePedidoAPI>>(content);
            }
            List<DetallePedidoViewModel> lista = new List<DetallePedidoViewModel>();
            foreach (var detallePedido in detallePedidos)
            {
                lista.Add(Convertir(detallePedido));
            }   
            return lista;
        }

        public DetallePedidoViewModel GetDetallePedido(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/DetallePedido/" + id.ToString());
            DetallePedidoAPI detallePedido = new DetallePedidoAPI();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                detallePedido = JsonConvert.DeserializeObject<DetallePedidoAPI>(content);
            }

            DetallePedidoViewModel resultado = Convertir(detallePedido);


            return resultado;
        }

        public DetallePedidoViewModel Update(DetallePedidoViewModel categoria)
        {
            throw new NotImplementedException();
        }
    }
}
