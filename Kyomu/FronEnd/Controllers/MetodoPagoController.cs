using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FronEnd.Controllers
{
    public class MetodoPagoController : Controller
    {

        IMetodoPagoHelper _metodoPagoHelper;

        public MetodoPagoController(IMetodoPagoHelper metodoPagoHelper)
        {
            _metodoPagoHelper = metodoPagoHelper;
        }

        // GET: CategoryController
        public ActionResult Index()
        {
            var result = _metodoPagoHelper.GetMetodosPago();
            return View(result);
        }

        // GET: CategoryController/Details/5
        public ActionResult Details(int id)
        {
            var result = _metodoPagoHelper.GetMetodoPago(id);
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
        public ActionResult Create(MetodoPagoViewModel metodoPago)
        {
            try
            {
                _metodoPagoHelper.Add(metodoPago);
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
            var metodoPago = _metodoPagoHelper.GetMetodoPago(id);
            if (metodoPago == null)
            {
                return NotFound();
            }
            return View(metodoPago);
        }


        // POST: CategoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, MetodoPagoViewModel metodoPago)
        {
            try
            {
                if (id != metodoPago.IdMetodo)
                {
                    return BadRequest();
                }


                var updatedMetodo = _metodoPagoHelper.Update(metodoPago);
                if (updatedMetodo == null)
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
            var metodoPago = _metodoPagoHelper.GetMetodoPago(id);
            if (metodoPago == null)
            {
                return NotFound();
            }
            return View(metodoPago);
        }

        // POST: CategoriaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, MetodoPagoViewModel metodoPago)
        {
            try
            {
                _metodoPagoHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
