using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Controllers
{
    public class DetallePedidoController : Controller
    {

        IDetallePedidoHelper _detallePedidoHelper;
        IPedidoHelper _pedidoHelper;
        IPlatilloHelper _platilloHelper;

        public DetallePedidoController(
            IDetallePedidoHelper detallePedidoHelper,
            IPedidoHelper pedidoHelper,
            IPlatilloHelper platilloHelper)
        {
            _detallePedidoHelper = detallePedidoHelper;
            _pedidoHelper = pedidoHelper;
            _platilloHelper = platilloHelper;
        }

        // GET: DetallePedido
        public ActionResult Index()
        {
            var result = _detallePedidoHelper.GetDetallePedidos();
            return View(result);
        }

        // GET: DetallePedido/Details/5
        public ActionResult Details(int id)
        {
            var result = _detallePedidoHelper.GetDetallePedido(id);
            return View(result);
        }

        // GET: DetallePedido/Create
        public ActionResult Create()
        {
            var model = new DetallePedidoViewModel
            {
                // Cargar las opciones para los dropdowns usando tus helpers existentes
                Pedidos = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
                {
                    Value = p.IdPedido.ToString(),
                    Text = $"Pedido #{p.IdPedido} - {p.FechaPedido.ToString("dd/MM/yyyy")}"
                }),

                Platillos = _platilloHelper.GetPlatillos().Select(p => new SelectListItem
                {
                    Value = p.IdPlatillo.ToString(),
                    Text = $"{p.Nombre} - ${p.Precio}"
                })
            };

            return View(model);
        }

        // POST: DetallePedido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DetallePedidoViewModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _detallePedidoHelper.Add(model);
                    return RedirectToAction(nameof(Index));
                }

                // Si hay errores, recargar los dropdowns
                model.Pedidos = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
                {
                    Value = p.IdPedido.ToString(),
                    Text = $"Pedido #{p.IdPedido} - {p.FechaPedido.ToString("dd/MM/yyyy")}"
                });

                model.Platillos = _platilloHelper.GetPlatillos().Select(p => new SelectListItem
                {
                    Value = p.IdPlatillo.ToString(),
                    Text = $"{p.Nombre} - ${p.Precio}"
                });

                return View(model);
            }
            catch
            {
                // Recargar dropdowns en caso de error
                model.Pedidos = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
                {
                    Value = p.IdPedido.ToString(),
                    Text = $"Pedido #{p.IdPedido} - {p.FechaPedido.ToString("dd/MM/yyyy")}"
                });

                model.Platillos = _platilloHelper.GetPlatillos().Select(p => new SelectListItem
                {
                    Value = p.IdPlatillo.ToString(),
                    Text = $"{p.Nombre} - ${p.Precio}"
                });

                return View(model);
            }
        }

        // GET: DetallePedido/Edit/5
        public ActionResult Edit(int id)
        {
            var detallePedido = _detallePedidoHelper.GetDetallePedido(id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            // Cargar dropdowns
            detallePedido.Pedidos = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
            {
                Value = p.IdPedido.ToString(),
                Text = $"Pedido #{p.IdPedido} - {p.FechaPedido:dd/MM/yyyy}",
                Selected = p.IdPedido == detallePedido.IdPedido
            });

            detallePedido.Platillos = _platilloHelper.GetPlatillos().Select(p => new SelectListItem
            {
                Value = p.IdPlatillo.ToString(),
                Text = $"{p.Nombre} - ${p.Precio}",
                Selected = p.IdPlatillo == detallePedido.IdPlatillo
            });

            return View(detallePedido);
        }

        // POST: DetallePedido/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, DetallePedidoViewModel detallePedido)
        {
            try
            {
                if (id != detallePedido.IdDetallePedido)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    var updatedDetalle = _detallePedidoHelper.Update(detallePedido);
                    if (updatedDetalle == null)
                    {
                        return NotFound();
                    }
                    return RedirectToAction(nameof(Index));
                }

                // Si hay errores, recargar dropdowns
                detallePedido.Pedidos = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
                {
                    Value = p.IdPedido.ToString(),
                    Text = $"Pedido #{p.IdPedido} - {p.FechaPedido:dd/MM/yyyy}",
                    Selected = p.IdPedido == detallePedido.IdPedido
                });

                detallePedido.Platillos = _platilloHelper.GetPlatillos().Select(p => new SelectListItem
                {
                    Value = p.IdPlatillo.ToString(),
                    Text = $"{p.Nombre} - ${p.Precio}",
                    Selected = p.IdPlatillo == detallePedido.IdPlatillo
                });

                return View(detallePedido);
            }
            catch
            {
                // Recargar dropdowns en caso de error
                detallePedido.Pedidos = _pedidoHelper.GetPedidos().Select(p => new SelectListItem
                {
                    Value = p.IdPedido.ToString(),
                    Text = $"Pedido #{p.IdPedido} - {p.FechaPedido:dd/MM/yyyy}"
                });

                detallePedido.Platillos = _platilloHelper.GetPlatillos().Select(p => new SelectListItem
                {
                    Value = p.IdPlatillo.ToString(),
                    Text = $"{p.Nombre} - ${p.Precio}"
                });

                return View(detallePedido);
            }
        }

        // GET: DetallePedidoController/Delete/5
        public ActionResult Delete(int id)
        {
            var detallePedido = _detallePedidoHelper.GetDetallePedido(id);
            if (detallePedido == null)
            {
                return NotFound();
            }

            // Opcional: Cargar información adicional para mostrar
            var pedido = _pedidoHelper.GetPedido(detallePedido.IdPedido);
            var platillo = _platilloHelper.GetPlatillo(detallePedido.IdPlatillo);

            ViewBag.PedidoInfo = $"Pedido #{pedido?.IdPedido} - {pedido?.FechaPedido:dd/MM/yyyy}";
            ViewBag.PlatilloInfo = $"{platillo?.Nombre} - ${platillo?.Precio}";

            return View(detallePedido);
        }


        // POST: DetallePedidoController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _detallePedidoHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                // Opcional: Recargar datos si falla
                var detallePedido = _detallePedidoHelper.GetDetallePedido(id);
                if (detallePedido == null)
                {
                    return NotFound();
                }

                var pedido = _pedidoHelper.GetPedido(detallePedido.IdPedido);
                var platillo = _platilloHelper.GetPlatillo(detallePedido.IdPlatillo);

                ViewBag.PedidoInfo = $"Pedido #{pedido?.IdPedido} - {pedido?.FechaPedido:dd/MM/yyyy}";
                ViewBag.PlatilloInfo = $"{platillo?.Nombre} - ${platillo?.Precio}";

                return View(detallePedido);
            }
        }
    }
}
