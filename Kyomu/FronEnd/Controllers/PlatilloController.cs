using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FronEnd.Controllers
{
    public class PlatilloController : Controller
    {
        IPlatilloHelper _platilloHelper;

        public PlatilloController(IPlatilloHelper platilloHelper)
        {
            _platilloHelper = platilloHelper;
        }


        // GET: PlatilloController
        public ActionResult Index()
        {
            var result = _platilloHelper.GetPlatillos();
            return View(result);
        }

        // GET: PlatilloController/Details/5
        public ActionResult Details(int id)
        {
            var result = _platilloHelper.GetPlatillo(id);
            return View(result);
        }

        // GET: PlatilloController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: PlatilloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlatilloViewModel platillo)
        {
            try
            {
                _platilloHelper.Add(platillo);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: PlatilloController/Edit/5
        public ActionResult Edit(int id)
        {
            var platillo = _platilloHelper.GetPlatillo(id);
            if (platillo == null)
            {
                return NotFound(); 
            }
            return View(platillo);
        }

        // POST: PlatilloController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, PlatilloViewModel platillo)
        {
            try
            {
                if (id != platillo.IdPlatillo)
                {
                    return BadRequest();
                }


                var updatedPlatillo = _platilloHelper.Update(platillo);
                if (updatedPlatillo == null)
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

        // GET: PlatilloController/Delete/5
        public ActionResult Delete(int id)
        {
            var platillo = _platilloHelper.GetPlatillo(id);
            if (platillo == null)
            {
                return NotFound();
            }
            return View(platillo);
        }

        // POST: PlatilloController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, PlatilloViewModel platillo)
        {
            try
            {

                _platilloHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
