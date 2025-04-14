using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Mvc;

namespace FronEnd.Controllers
{
    public class CarritoController : Controller
    {
        private readonly ICarritoHelper _carritoHelper;

        public CarritoController(ICarritoHelper carritoHelper)
        {
            _carritoHelper = carritoHelper;
        }

        // GET: Carrito/Index
        public ActionResult Index()
        {
            var carrito = _carritoHelper.GetCarrito();
            return View(carrito);
        }

        // POST: Carrito/AddItem
        [HttpPost]
        public ActionResult AddItem(int idPlatillo, int cantidad)
        {
            _carritoHelper.AddItem(idPlatillo, cantidad);
            return RedirectToAction("Index");
        }

        // POST: Carrito/RemoveItem
        [HttpPost]
        public ActionResult RemoveItem(int idPlatillo)
        {
            _carritoHelper.RemoveItem(idPlatillo);
            return RedirectToAction("Index");
        }

        // POST: Carrito/Clear
        [HttpPost]
        public ActionResult Clear()
        {
            _carritoHelper.ClearCarrito();
            return RedirectToAction("Index");
        }
    }
}
