using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FronEnd.Controllers
{
    public class DetallePedidoController : Controller
    {

        IDetallePedidoHelper _detallePedidoHelper;

        public DetallePedidoController(IDetallePedidoHelper detallePedidoHelper)
        {
            _detallePedidoHelper = detallePedidoHelper;
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
            return View();
        }

        // POST: DetallePedido/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(DetallePedidoViewModel detallePedido)
        {
            try
            {
                _detallePedidoHelper.Add(detallePedido);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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


                var updatedDetallePedido = _detallePedidoHelper.Update(detallePedido);
                if (updatedDetallePedido == null)
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

        // GET: DetallePedidoController/Delete/5
        public ActionResult Delete(int id)
        {
            var detallePedido = _detallePedidoHelper.GetDetallePedido(id);
            if (detallePedido == null)
            {
                return NotFound();
            }
            return View(detallePedido);
        }

        // POST: DetallePedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _detallePedidoHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
