using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Controllers
{
    public class PedidoController : Controller
    {
        IPedidoHelper _pedidoHelper;
        IUsuarioHelper _usuarioHelper;

        public PedidoController(IPedidoHelper pedidoHelper, IUsuarioHelper usuarioHelper)
        {
            _pedidoHelper = pedidoHelper;
            _usuarioHelper = usuarioHelper;
        }


        // GET: PedidoController
        public ActionResult Index()
        {
            var result = _pedidoHelper.GetPedidos();
            return View(result);
        }

        // GET: PedidoController/Details/5
        public ActionResult Details(int id)
        {
            var result = _pedidoHelper.GetPedido(id);
            return View(result);
        }

        // GET: PedidoController/Create
        public ActionResult Create()
        {
            var model = new PedidoViewModel
            {
                FechaPedido = DateTime.Now,
                UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u => new SelectListItem
                {
                    Value = u.IdUsuario.ToString(),
                    Text = $"{u.Nombre} ({u.CorreoElectronico})"
                }),
                OpcionesEntrega = new[]
                {
                    new SelectListItem { Value = "Delivery", Text = "Delivery" },
                    new SelectListItem { Value = "Recoger", Text = "Recoger en local" }
                },
                EstadosDisponibles = new[]
                {
                    new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                    new SelectListItem { Value = "En proceso", Text = "En proceso" },
                    new SelectListItem { Value = "Completado", Text = "Completado" },
                    new SelectListItem { Value = "Cancelado", Text = "Cancelado" }
                }
            };
            return View(model);
        }

        // POST: PedidoController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PedidoViewModel pedido)
        {
            try
            {
                _pedidoHelper.Add(pedido);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PedidoController/Edit/5
        public ActionResult Edit(int id)
        {
            var pedido = _pedidoHelper.GetPedido(id);
            if (pedido == null)
            {
                return NotFound();
            }

            pedido.UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u => new SelectListItem
            {
                Value = u.IdUsuario.ToString(),
                Text = $"{u.Nombre} ({u.CorreoElectronico})",
                Selected = u.IdUsuario == pedido.IdUsuario
            });

            pedido.OpcionesEntrega = new[]
            {
                new SelectListItem { Value = "Delivery", Text = "Delivery", Selected = "Delivery" == pedido.TipoEntrega },
                new SelectListItem { Value = "Recoger", Text = "Recoger en local", Selected = "Recoger" == pedido.TipoEntrega }
            };

            pedido.EstadosDisponibles = new[]
            {
                new SelectListItem { Value = "Pendiente", Text = "Pendiente", Selected = "Pendiente" == pedido.Estado },
                new SelectListItem { Value = "En proceso", Text = "En proceso", Selected = "En proceso" == pedido.Estado },
                new SelectListItem { Value = "Completado", Text = "Completado", Selected = "Completado" == pedido.Estado },
                new SelectListItem { Value = "Cancelado", Text = "Cancelado", Selected = "Cancelado" == pedido.Estado }
            };

            return View(pedido);
        }

        // POST: PedidoController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PedidoViewModel pedido)
        {
            try
            {
                if (id != pedido.IdPedido)
                {
                    return BadRequest();
                }

                if (ModelState.IsValid)
                {
                    var updatedPedido = _pedidoHelper.Update(pedido);
                    if (updatedPedido == null)
                    {
                        return NotFound();
                    }
                    return RedirectToAction(nameof(Index));
                }

                // Recargar dropdowns si hay error
                pedido.UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u => new SelectListItem
                {
                    Value = u.IdUsuario.ToString(),
                    Text = $"{u.Nombre} ({u.CorreoElectronico})"
                });

                pedido.OpcionesEntrega = new[]
                {
                    new SelectListItem { Value = "Delivery", Text = "Delivery" },
                    new SelectListItem { Value = "Recoger", Text = "Recoger en local" }
                };

                pedido.EstadosDisponibles = new[]
                {
                    new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                    new SelectListItem { Value = "En proceso", Text = "En proceso" },
                    new SelectListItem { Value = "Completado", Text = "Completado" },
                    new SelectListItem { Value = "Cancelado", Text = "Cancelado" }
                };

                return View(pedido);
            }
            catch
            {
                // Recargar dropdowns en caso de error
                pedido.UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u => new SelectListItem
                {
                    Value = u.IdUsuario.ToString(),
                    Text = $"{u.Nombre} ({u.CorreoElectronico})"
                });

                pedido.OpcionesEntrega = new[]
                {
                    new SelectListItem { Value = "Delivery", Text = "Delivery" },
                    new SelectListItem { Value = "Recoger", Text = "Recoger en local" }
                };

                pedido.EstadosDisponibles = new[]
                {
                    new SelectListItem { Value = "Pendiente", Text = "Pendiente" },
                    new SelectListItem { Value = "En proceso", Text = "En proceso" },
                    new SelectListItem { Value = "Completado", Text = "Completado" },
                    new SelectListItem { Value = "Cancelado", Text = "Cancelado" }
                };

                return View(pedido);
            }
        }

        // GET: PedidoController/Delete/5
        public ActionResult Delete(int id)
        {
            var pedido = _pedidoHelper.GetPedido(id);
            if (pedido == null)
            {
                return NotFound(); 
            }
            return View(pedido);
        }

        // POST: PedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, PedidoViewModel pedido)
        {
            try
            {
                
                _pedidoHelper.Delete(id);
                return RedirectToAction(nameof(Index)); 
            }
            catch
            {
                return View(); 
            }
        }
    }
}
