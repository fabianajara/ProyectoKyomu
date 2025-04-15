using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Linq;
using System.Collections.Generic;
using System.Security.Claims;
using System.Security;

namespace FronEnd.Helpers.Implementations
{
    public class CarritoHelper : ICarritoHelper
    {
        private readonly IServiceRepository _serviceRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly int? _userId;

        public CarritoHelper(IServiceRepository serviceRepository, IHttpContextAccessor httpContextAccessor)
        {
            _serviceRepository = serviceRepository;
            _httpContextAccessor = httpContextAccessor;
            _userId = GetAuthenticatedUserId();
        }

        public bool IsUserAuthenticated()
        {
            return _userId.HasValue &&
                   _httpContextAccessor.HttpContext?.User.Identity.IsAuthenticated == true;
        }

        private int? GetAuthenticatedUserId()
        {
            var httpContext = _httpContextAccessor?.HttpContext;
            if (httpContext == null || !httpContext.User.Identity.IsAuthenticated)
            {
                return null;
            }

            var userIdClaim = httpContext.User.FindFirst(ClaimTypes.NameIdentifier);
            if (userIdClaim == null || string.IsNullOrEmpty(userIdClaim.Value))
            {
                return null;
            }

            if (!int.TryParse(userIdClaim.Value, out int parsedUserId))
            {
                return null;
            }

            return parsedUserId;
        }


        /// <summary>
        /// Retorna el carrito (pedido pendiente) del usuario, mapeando sus detalles a CarritoViewModel.
        /// </summary>
        public CarritoViewModel GetCarrito()
        {
            // Obtener todos los pedidos
            HttpResponseMessage response = _serviceRepository.GetResponse("api/Pedido");
            if (!response.IsSuccessStatusCode)
                return new CarritoViewModel();

            var pedidosContent = response.Content.ReadAsStringAsync().Result;
            var pedidos = JsonSerializer.Deserialize<List<PedidoViewModel>>(pedidosContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            // Buscar el pedido pendiente del usuario
            var pedido = pedidos?.FirstOrDefault(p => p.IdUsuario == _userId && p.Estado.Equals("Pendiente", StringComparison.OrdinalIgnoreCase));
            if (pedido == null)
                return new CarritoViewModel();

            // Obtener los detalles del pedido
            HttpResponseMessage detallesResponse = _serviceRepository.GetResponse($"api/DetallePedido/Pedido/{pedido.IdPedido}");
            if (!detallesResponse.IsSuccessStatusCode)
                return new CarritoViewModel();

            var detallesContent = detallesResponse.Content.ReadAsStringAsync().Result;
            var detalles = JsonSerializer.Deserialize<List<DetallePedidoViewModel>>(detallesContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            var carrito = new CarritoViewModel();

            foreach (var detalle in detalles)
            {
                // Obtener información del platillo
                HttpResponseMessage platilloResponse = _serviceRepository.GetResponse($"api/Platillo/{detalle.IdPlatillo}");
                if (!platilloResponse.IsSuccessStatusCode)
                    continue;

                var platilloContent = platilloResponse.Content.ReadAsStringAsync().Result;
                var platillo = JsonSerializer.Deserialize<PlatilloViewModel>(platilloContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

                if (platillo == null)
                    continue;

                carrito.Items.Add(new ItemCarritoViewModel
                {
                    IdPlatillo = platillo.IdPlatillo,
                    Nombre = platillo.Nombre,
                    Precio = platillo.Precio,
                    Cantidad = detalle.Cantidad,
                    Imagen = platillo.Imagen
                });
            }

            return carrito;
        }


        /// <summary>
        /// Agrega un platillo al carrito (pedido pendiente). Si ya existe en el detalle, se actualiza la cantidad.
        /// </summary>
        public void AddItem(int idPlatillo, int cantidad)
        {
            // 1. Obtener el pedido pendiente del usuario mediante el endpoint general
            HttpResponseMessage pedidosResponse = _serviceRepository.GetResponse("api/Pedido");
            int pedidoId;
            if (pedidosResponse.IsSuccessStatusCode)
            {
                var pedidosContent = pedidosResponse.Content.ReadAsStringAsync().Result;
                var pedidos = JsonSerializer.Deserialize<List<PedidoViewModel>>(
                    pedidosContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var pedido = pedidos?.FirstOrDefault(p => p.IdUsuario == _userId
                                                        && p.Estado.Equals("Pendiente", System.StringComparison.OrdinalIgnoreCase));
                if (pedido == null)
                {
                    pedidoId = CrearPedidoPendiente();
                }
                else
                {
                    pedidoId = pedido.IdPedido;
                }
            }
            else
            {
                pedidoId = CrearPedidoPendiente();
            }

            // 2. Obtener todos los detalles generales para filtrar el detalle del platillo.
            HttpResponseMessage detallesResponse = _serviceRepository.GetResponse("api/DetallePedido");
            if (detallesResponse.IsSuccessStatusCode)
            {
                var detallesContent = detallesResponse.Content.ReadAsStringAsync().Result;
                var detalles = JsonSerializer.Deserialize<List<DetallePedidoViewModel>>(
                    detallesContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var detalle = detalles?.FirstOrDefault(d => d.IdPedido == pedidoId && d.IdPlatillo == idPlatillo);
                if (detalle != null)
                {
                    // Actualizar la cantidad del detalle existente
                    detalle.Cantidad += cantidad;
                    _serviceRepository.PutResponse($"api/DetallePedido/{detalle.IdDetallePedido}", detalle);
                    return;
                }
            }
            // Si no existe el detalle, se crea uno nuevo.
            var newDetalle = new DetallePedidoViewModel
            {
                IdPedido = pedidoId,
                IdPlatillo = idPlatillo,
                Cantidad = cantidad
            };
            _serviceRepository.PostResponse("api/DetallePedido", newDetalle);
        }

        /// <summary>
        /// Remueve un platillo del carrito eliminando el detalle correspondiente.
        /// </summary>
        public void RemoveItem(int idPlatillo)
        {
            // Obtener el pedido pendiente del usuario.
            HttpResponseMessage pedidosResponse = _serviceRepository.GetResponse("api/Pedido");
            if (pedidosResponse.IsSuccessStatusCode)
            {
                var pedidosContent = pedidosResponse.Content.ReadAsStringAsync().Result;
                var pedidos = JsonSerializer.Deserialize<List<PedidoViewModel>>(
                    pedidosContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var pedido = pedidos?.FirstOrDefault(p => p.IdUsuario == _userId
                                                        && p.Estado.Equals("Pendiente", System.StringComparison.OrdinalIgnoreCase));
                if (pedido != null)
                {
                    // Obtener todos los detalles y filtrar el detalle del platillo deseado.
                    HttpResponseMessage detallesResponse = _serviceRepository.GetResponse("api/DetallePedido");
                    if (detallesResponse.IsSuccessStatusCode)
                    {
                        var detallesContent = detallesResponse.Content.ReadAsStringAsync().Result;
                        var detalles = JsonSerializer.Deserialize<List<DetallePedidoViewModel>>(
                            detallesContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        var detalle = detalles?.FirstOrDefault(d => d.IdPedido == pedido.IdPedido && d.IdPlatillo == idPlatillo);
                        if (detalle != null)
                        {
                            _serviceRepository.DeleteResponse($"api/DetallePedido/{detalle.IdDetallePedido}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Limpia el carrito eliminando todos los detalles del pedido pendiente.
        /// </summary>
        public void ClearCarrito()
        {
            // Obtener el pedido pendiente del usuario.
            HttpResponseMessage pedidosResponse = _serviceRepository.GetResponse("api/Pedido");
            if (pedidosResponse.IsSuccessStatusCode)
            {
                var pedidosContent = pedidosResponse.Content.ReadAsStringAsync().Result;
                var pedidos = JsonSerializer.Deserialize<List<PedidoViewModel>>(
                    pedidosContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                var pedido = pedidos?.FirstOrDefault(p => p.IdUsuario == _userId
                                                        && p.Estado.Equals("Pendiente", System.StringComparison.OrdinalIgnoreCase));
                if (pedido != null)
                {
                    // Obtener todos los detalles del pedido usando el endpoint específico.
                    HttpResponseMessage detallesResponse = _serviceRepository.GetResponse($"api/DetallePedido/Pedido/{pedido.IdPedido}");
                    if (detallesResponse.IsSuccessStatusCode)
                    {
                        var detallesContent = detallesResponse.Content.ReadAsStringAsync().Result;
                        var detalles = JsonSerializer.Deserialize<List<DetallePedidoViewModel>>(
                            detallesContent, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                        foreach (var detalle in detalles)
                        {
                            _serviceRepository.DeleteResponse($"api/DetallePedido/{detalle.IdDetallePedido}");
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Retorna el total del carrito.
        /// </summary>
        public decimal GetTotal()
        {
            return GetCarrito().Total;
        }

        /// <summary>
        /// Crea un nuevo pedido en estado "Pendiente" para el usuario.
        /// </summary>
        private int CrearPedidoPendiente()
        {

            var nuevoPedido = new PedidoViewModel
            {
                IdUsuario = _userId.Value,
                FechaPedido = System.DateTime.Now,
                Total = 0,
                Estado = "Pendiente"
            };

            HttpResponseMessage response = _serviceRepository.PostResponse("api/Pedido", nuevoPedido);
            if (response.IsSuccessStatusCode)
            {
                var content = response.Content.ReadAsStringAsync().Result;
                var pedido = JsonSerializer.Deserialize<PedidoViewModel>(
                    content, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return pedido.IdPedido;
            }
            return 0;
        }
    }
}
