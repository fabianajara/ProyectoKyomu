using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FronEnd.Controllers
{
    public class ReseñaController : Controller
    {
        IReseñaHelper _reseñaHelper;
        public ReseñaController(IReseñaHelper reseñaHelper)
        {
            _reseñaHelper = reseñaHelper;
        }

        // GET: ReseñaController
        public ActionResult Index()
        {
            var result = _reseñaHelper.GetReseñas();
            return View(result);
        }

        // GET: ReseñaController/Details/5
        public ActionResult Details(int id)
        {
            var result = _reseñaHelper.GetReseña(id);
            return View(result);
        }

        // GET: ReseñaController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ReseñaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReseñaViewModel reseña)
        {
            try
            {
                _reseñaHelper.Add(reseña);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ReseñaController/Edit/5
        public ActionResult Edit(int id)
        {
            var reseña = _reseñaHelper.GetReseña(id);
            if (reseña == null)
            {
                return NotFound();
            }
            return View(reseña);
        }

        // POST: ReseñaController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, ReseñaViewModel reseña)
        {
            try
            {
                if (id != reseña.IdReseña)
                {
                    return BadRequest();
                }


                var updatedReseña = _reseñaHelper.Update(reseña);
                if (updatedReseña == null)
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

        // GET: ReseñaController/Delete/5
        public ActionResult Delete(int id)
        {
            var reseña = _reseñaHelper.GetReseña(id);
            if (reseña == null)
            {
                return NotFound();
            }
            return View(reseña);
        }

        // POST: ReseñaController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, ReseñaViewModel reseña)
        {
            try
            {

                _reseñaHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
