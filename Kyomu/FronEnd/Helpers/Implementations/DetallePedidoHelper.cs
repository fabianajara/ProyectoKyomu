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
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/DetallePedido" + id.ToString());
            _ = _ServiceRepository.DeleteResponse("api/DetallePedido/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
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

        public DetallePedidoViewModel Update(DetallePedidoViewModel detallePedido)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.PutResponse("api/DetallePedido/" + detallePedido.IdDetallePedido.ToString(), detallePedido);
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                DetallePedidoAPI updatedDetallePedido = JsonConvert.DeserializeObject<DetallePedidoAPI>(content);
                return Convertir(updatedDetallePedido);
            }
            else
            {

                var errorMessage = responseMessage.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al actualizar el detalle del pedido: " + errorMessage);
                return null;
            }
        }

        public List<PedidoViewModel> GetPedidosDisponibles()
        {
            // Implementación para obtener pedidos disponibles desde tu API
            var response = _ServiceRepository.GetResponse("api/Pedido/Disponibles");
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<PedidoViewModel>>(content);
            }
            return new List<PedidoViewModel>();
        }

        public List<PlatilloViewModel> GetPlatillosDisponibles()
        {
            // Implementación para obtener platillos disponibles desde tu API
            var response = _ServiceRepository.GetResponse("api/Platillo/Disponibles");
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<List<PlatilloViewModel>>(content);
            }
            return new List<PlatilloViewModel>();
        }
    }
}
