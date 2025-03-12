using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FronEnd.Controllers
{
    public class PagoController : Controller
    {

        IPagoHelper _pagoHelper;

        public PagoController(IPagoHelper pagoHelper)
        {
            _pagoHelper = pagoHelper;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            var result = _pagoHelper.GetPagos();
            return View(result);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var result = _pagoHelper.GetPago(id);
            return View(result);
        }

        // GET: CategoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: CategoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoViewModel pago)
        {
            try
            {
                _pagoHelper.Add(pago);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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


                var updatedPago = _pagoHelper.Update(pago);
                if (updatedPago == null)
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

        // GET: CategoriaController/Delete/5
        public ActionResult Delete(int id)
        {
            var pago = _pagoHelper.GetPago(id);
            if (pago == null)
            {
                return NotFound();
            }
            return View(pago);
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                _pagoHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
