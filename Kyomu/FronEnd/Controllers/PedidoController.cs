using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FronEnd.Controllers
{
    public class PedidoController : Controller
    {
        IPedidoHelper _pedidoHelper;

        public PedidoController(IPedidoHelper pedidoHelper)
        {
            _pedidoHelper = pedidoHelper;
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
            return View();
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

               
                var updatedPedido = _pedidoHelper.Update(pedido);
                if (updatedPedido == null)
                {
                    return NotFound(); 
                }

                return RedirectToAction(nameof(Index)); 
            }
            catch
            {
                return View(); 
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
