using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritoHelper _carritoHelper;

        public CarritoController(ICarritoHelper carritoHelper)
        {
            _carritoHelper = carritoHelper;
        }

        // GET: Carrito/Index
        public ActionResult Index()
        {
            var carrito = _carritoHelper.GetCarrito();
            return View(carrito);
        }

        // POST: Carrito/AddItem
        [HttpPost]
        [Authorize]
        public ActionResult AddItem(int idPlatillo, int cantidad)
        {
            try
            {
                if (!_carritoHelper.IsUserAuthenticated())
                {
                    return RedirectToAction("Login", "Usuario");
                }

                _carritoHelper.AddItem(idPlatillo, cantidad);
                return RedirectToAction("Index");
            }
            catch (ApplicationException ex)
            {
                // Loggear error
                return RedirectToAction("Error", "Home");
            }
        }
        // POST: Carrito/RemoveItem
        [HttpPost]
        public ActionResult RemoveItem(int idPlatillo)
        {
            _carritoHelper.RemoveItem(idPlatillo);
            return RedirectToAction("Index");
        }

        // POST: Carrito/Clear
        [HttpPost]
        public ActionResult Clear()
        {
            _carritoHelper.ClearCarrito();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Checkout()
        {
            var carrito = _carritoHelper.GetCarrito(); 
            var metodoPagos = GetMetodosDePago();  
            var opcionesEntrega = GetOpcionesDeEntrega();  

            var model = new CarritoViewModel
            {
                Items = carrito.Items,  
                MetodosPagoDisponibles = metodoPagos.Select(mp => new SelectListItem
                {
                    Value = mp.IdMetodo.ToString(),
                    Text = mp.TipoMetodo
                }),
                OpcionesEntrega = opcionesEntrega.Select(oe => new SelectListItem
                {
                    Value = oe,
                    Text = oe
                }),
                IdMetodoSeleccionado = metodoPagos.FirstOrDefault()?.IdMetodo ?? 0,
                TipoEntregaSeleccionado = opcionesEntrega.FirstOrDefault()
            };

            return View(model);
        }

        // Método simulado para obtener métodos de pago
        private List<MetodoPagoViewModel> GetMetodosDePago()
        {
            return new List<MetodoPagoViewModel>
    {
        new MetodoPagoViewModel { IdMetodo = 1, TipoMetodo = "Tarjeta de Crédito" },
        new MetodoPagoViewModel { IdMetodo = 2, TipoMetodo = "PayPal" },
        new MetodoPagoViewModel { IdMetodo = 3, TipoMetodo = "Transferencia Bancaria" }
    };
        }

        // Método simulado para obtener opciones de entrega
        private List<string> GetOpcionesDeEntrega()
        {
            return new List<string> { "Envío a domicilio", "Recogida en tienda", "Envío express" };
        }

        [HttpPost]
        [Authorize]  
        public ActionResult ConfirmarPedido(CarritoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View("Checkout", model);  
            }

            try
            {
               
                var metodoPago = GetMetodosDePago().FirstOrDefault(mp => mp.IdMetodo == model.IdMetodoSeleccionado);
                if (metodoPago == null)
                {
                    // En caso de que el método de pago no sea válido
                    return RedirectToAction("Error", "Home");
                }

                var tipoEntrega = model.TipoEntregaSeleccionado;
                if (string.IsNullOrEmpty(tipoEntrega))
                {
                    // En caso de que no se haya seleccionado una opción de entrega válida
                    return RedirectToAction("Error", "Home");
                }

                _carritoHelper.ClearCarrito();

                return RedirectToAction("PedidoExitoso");
            }
            catch (Exception ex)
            {
                return RedirectToAction("Error", "Home");
            }
        }

        public ActionResult PedidoExitoso()
        {
            return View();
        }

    }
}
