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


        public ActionResult Index()
        {
            var result = _metodoPagoHelper.GetMetodosPago;
            return View(result);
        }


        public ActionResult Details(int id)
        {
            var result = _metodoPagoHelper.GetMetodoPago(id);
            return View(result);
        }


        public ActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(MetodoPagoViewModel metodoPagoHelper)
        {
            try
            {
                _metodoPagoHelper.Add(metodoPagoHelper);
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
