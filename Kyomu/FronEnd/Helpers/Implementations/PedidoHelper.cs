using FronEnd.ApiModels;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Newtonsoft.Json;
using NuGet.Protocol.Core.Types;

namespace FronEnd.Helpers.Implementations
{
    public class PedidoHelper : IPedidoHelper
    {
        IServiceRepository _ServiceRepository;

        PedidoViewModel Convertir(PedidoAPI pedido)
        {
            PedidoViewModel pedidoViewModel = new PedidoViewModel
            {
                IdPedido = pedido.IdPedido,
                IdUsuario = pedido.IdUsuario,
                FechaPedido = pedido.FechaPedido,
                FechaEntrega = pedido.FechaEntrega,
                TipoEntrega = pedido.TipoEntrega,
                Total = pedido.Total,
                Estado = pedido.Estado

            };
            return pedidoViewModel;
        }

        public PedidoHelper(IServiceRepository serviceRepository)
        {
            _ServiceRepository = serviceRepository;
        }
        public PedidoViewModel Add(PedidoViewModel pedido)
        {
            HttpResponseMessage response = _ServiceRepository.PostResponse("api/Pedido", pedido);
            if (response.IsSuccessStatusCode)
            {

                var content = response.Content.ReadAsStringAsync().Result;
            }
            return pedido;
        }

        public void Delete(int id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.DeleteResponse("api/Pedido" + id.ToString());
            _ = _ServiceRepository.DeleteResponse("api/Pedido/" + id.ToString());
            if (responseMessage != null)
            {
                var content = responseMessage.Content;
            }
        }

        public PedidoViewModel GetPedido(int? id)
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Pedido/" + id.ToString());
            PedidoAPI pedido = new PedidoAPI();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                pedido = JsonConvert.DeserializeObject<PedidoAPI>(content);
            }

            PedidoViewModel resultado = Convertir(pedido);


            return resultado;
        }

        public List<PedidoViewModel> GetPedidos()
        {
            HttpResponseMessage responseMessage = _ServiceRepository.GetResponse("api/Pedido");
            List<PedidoAPI> pedidos = new List<PedidoAPI>();
            if (responseMessage != null)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                pedidos = JsonConvert.DeserializeObject<List<PedidoAPI>>(content);
            }
            List<PedidoViewModel> lista = new List<PedidoViewModel>();
            foreach (var pedido in pedidos)
            {
                lista.Add(Convertir(pedido));
            }
            return lista;
        }

        public PedidoViewModel Update(PedidoViewModel pedido)
        {
            
            HttpResponseMessage responseMessage = _ServiceRepository.PutResponse("api/Pedido/" + pedido.IdPedido.ToString(), pedido);
            if (responseMessage.IsSuccessStatusCode)
            {
                var content = responseMessage.Content.ReadAsStringAsync().Result;
                PedidoAPI updatedPedido = JsonConvert.DeserializeObject<PedidoAPI>(content);
                return Convertir(updatedPedido);  
            }
            else
            {
               
                var errorMessage = responseMessage.Content.ReadAsStringAsync().Result;
                Console.WriteLine("Error al actualizar el pedido: " + errorMessage);
                return null;
            }
        }
    }
}
