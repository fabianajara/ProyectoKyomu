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
        public ActionResult Create(DetallePedidoViewModel detallePedidoHelper)
        {
            try
            {
                _detallePedidoHelper.Add(detallePedidoHelper);
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
            return View();
        }

        // POST: DetallePedido/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
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
            return View();
        }

        // POST: DetallePedidoController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
