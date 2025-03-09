using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FronEnd.Controllers
{
    public class RolController : Controller
    {
        IRolHelper _rolHelper;
        public RolController(IRolHelper rolHelper)
        {
            _rolHelper = rolHelper;
        }

        // GET: RolController
        public ActionResult Index()
        {
            var result = _rolHelper.GetRoles();
            return View(result);
        }

        // GET: RolController/Details/5
        public ActionResult Details(int id)
        {
            var result = _rolHelper.GetRol(id);
            return View(result);
        }

        // GET: RolController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RolController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(RolViewModel rol)
        {
            try
            {
                _rolHelper.Add(rol);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RolController/Edit/5
        public ActionResult Edit(int id)
        {
            var rol = _rolHelper.GetRol(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // POST: RolController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, RolViewModel rol)
        {
            try
            {
                if (id != rol.IdRol)
                {
                    return BadRequest();
                }


                var updatedRol = _rolHelper.Update(rol);
                if (updatedRol == null)
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

        // GET: RolController/Delete/5
        public ActionResult Delete(int id)
        {
            var rol = _rolHelper.GetRol(id);
            if (rol == null)
            {
                return NotFound();
            }
            return View(rol);
        }

        // POST: RolController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, RolViewModel rol)
        {
            try
            {

                _rolHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
