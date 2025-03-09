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

        public ActionResult Index()
        {
            var result = _pagoHelper.GetPagos();
            return View(result);
        }

        public ActionResult Details(int id)
        {
            var result = _pagoHelper.GetPago(id);
            return View(result);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PagoViewModel pagoHelper)
        {
            try
            {
                _pagoHelper.Add(pagoHelper);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        public ActionResult Edit(int id)
        {
            return View();
        }

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

        public ActionResult Delete(int id)
        {
            return View();
        }

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
