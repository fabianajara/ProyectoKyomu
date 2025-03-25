using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Controllers
{
    public class PagoController : Controller
    {

        IPagoHelper _pagoHelper;
        IPedidoHelper _pedidoHelper;
        IMetodoPagoHelper _metodoPagoHelper;

        public PagoController(IPagoHelper pagoHelper, IPedidoHelper pedidoHelper, IMetodoPagoHelper metodoPagoHelper)
        {
            _pagoHelper = pagoHelper;
            _pedidoHelper = pedidoHelper;
            _metodoPagoHelper = metodoPagoHelper;
        }

        // GET: PagoController
        public ActionResult Index()
        {
            var result = _pagoHelper.GetPagos();
            return View(result);
        }

        // GET: Pago/Details/5
        public ActionResult Details(int id)
        {
            var result = _pagoHelper.GetPago(id);
            return View(result);
        }

        // GET: Pago/Create
        public ActionResult Create()
        {
            var model = new PagoViewModel
            {
                FechaPago = DateTime.Now,
                PedidosDisponibles = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
                {
                    Value = p.IdPedido.ToString(),
                    Text = $"Pedido #{p.IdPedido} - {p.FechaPedido:dd/MM/yyyy}"
                }),
                MetodosPagoDisponibles = _metodoPagoHelper.GetMetodosPago().Select(m => new SelectListItem
                {
                    Value = m.IdMetodo.ToString(),
                    Text = m.TipoMetodo
                })
            };
            return View(model);
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoViewModel pago)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _pagoHelper.Add(pago);
                    return RedirectToAction(nameof(Index));
                }

                // Recargar dropdowns si hay error
                pago.PedidosDisponibles = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
                {
                    Value = p.IdPedido.ToString(),
                    Text = $"Pedido #{p.IdPedido} - {p.FechaPedido:dd/MM/yyyy}"
                });
                pago.MetodosPagoDisponibles = _metodoPagoHelper.GetMetodosPago().Select(m => new SelectListItem
                {
                    Value = m.IdMetodo.ToString(),
                    Text = m.TipoMetodo
                });

                return View(pago);
            }
            catch
            {
                pago.PedidosDisponibles = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
                {
                    Value = p.IdPedido.ToString(),
                    Text = $"Pedido #{p.IdPedido} - {p.FechaPedido:dd/MM/yyyy}"
                });
                pago.MetodosPagoDisponibles = _metodoPagoHelper.GetMetodosPago().Select(m => new SelectListItem
                {
                    Value = m.IdMetodo.ToString(),
                    Text = m.TipoMetodo
                });
                return View(pago);
            }
        }

        // GET: CategoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var pago = _pagoHelper.GetPago(id);
            if (pago == null)
            {
                return NotFound();
            }

            pago.PedidosDisponibles = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
            {
                Value = p.IdPedido.ToString(),
                Text = $"Pedido #{p.IdPedido} - {p.FechaPedido:dd/MM/yyyy}",
                Selected = p.IdPedido == pago.IdPedido
            });
            pago.MetodosPagoDisponibles = _metodoPagoHelper.GetMetodosPago().Select(m => new SelectListItem
            {
                Value = m.IdMetodo.ToString(),
                Text = m.TipoMetodo,
                Selected = m.IdMetodo == pago.IdMetodo
            });

            return View(pago);
        }

        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PagoViewModel pago)
        {
            try
            {
                if (id != pago.IdPago)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    var updatedPago = _pagoHelper.Update(pago);
                    if (updatedPago == null)
                    {
                        return NotFound();
                    }
                    return RedirectToAction(nameof(Index));
                }

                // Recargar dropdowns si hay error
                pago.PedidosDisponibles = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
                {
                    Value = p.IdPedido.ToString(),
                    Text = $"Pedido #{p.IdPedido} - {p.FechaPedido:dd/MM/yyyy}"
                });

                pago.MetodosPagoDisponibles = _metodoPagoHelper.GetMetodosPago().Select(m => new SelectListItem
                {
                    Value = m.IdMetodo.ToString(),
                    Text = m.TipoMetodo
                });

                return View(pago);
            }
            catch
            {
                // Recargar dropdowns en caso de error
                pago.PedidosDisponibles = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
                {
                    Value = p.IdPedido.ToString(),
                    Text = $"Pedido #{p.IdPedido} - {p.FechaPedido:dd/MM/yyyy}"
                });

                pago.MetodosPagoDisponibles = _metodoPagoHelper.GetMetodosPago().Select(m => new SelectListItem
                {
                    Value = m.IdMetodo.ToString(),
                    Text = m.TipoMetodo
                });

                return View(pago);
            }
        }

        // GET: CategoriaController/Delete/5
        public ActionResult Delete(int id)
        {
            var pago = _pagoHelper.GetPago(id);
            if (pago == null)
            {
                return NotFound();
            }

            // Cargar información adicional para mostrar
            var pedido = _pedidoHelper.GetPedido(pago.IdPedido);
            var metodoPago = _metodoPagoHelper.GetMetodoPago(pago.IdMetodo);

            ViewBag.PedidoInfo = $"Pedido #{pedido?.IdPedido} - {pedido?.FechaPedido:dd/MM/yyyy}";
            ViewBag.MetodoPagoInfo = $"{metodoPago?.TipoMetodo} - (ID: {metodoPago?.IdMetodo})";

            return View(pago);
        }

        // POST: PagoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _pagoHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Recargar datos si falla
                var pago = _pagoHelper.GetPago(id);
                if (pago == null)
                {
                    return NotFound();
                }

                var pedido = _pedidoHelper.GetPedido(pago.IdPedido);
                var metodoPago = _metodoPagoHelper.GetMetodoPago(pago.IdMetodo);

                ViewBag.PedidoInfo = $"Pedido #{pedido?.IdPedido} - {pedido?.FechaPedido:dd/MM/yyyy}";
                ViewBag.MetodoPagoInfo = $"{metodoPago?.TipoMetodo} - (ID: {metodoPago?.IdMetodo})";

                return View(pago);
            }
        }
    }
}
