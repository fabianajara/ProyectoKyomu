using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Controllers
{
    public class PlatilloController : Controller
    {
        IPlatilloHelper _platilloHelper;
        ICategoriaHelper _categoriaHelper;

        public PlatilloController(IPlatilloHelper platilloHelper, ICategoriaHelper categoriaHelper)
        {
            _platilloHelper = platilloHelper;
            _categoriaHelper = categoriaHelper;
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
            var model = new PlatilloViewModel
            {
                CategoriasDisponibles = _categoriaHelper.GetCategorias().Select(c => new SelectListItem
                {
                    Value = c.IdCategoria.ToString(),
                    Text = c.NombreCategoria
                })
            };
            return View(model);
        }

        // POST: PlatilloController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PlatilloViewModel platillo)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _platilloHelper.Add(platillo);
                    return RedirectToAction(nameof(Index));
                }

                // Recargar dropdown si hay error
                platillo.CategoriasDisponibles = _categoriaHelper.GetCategorias().Select(c => new SelectListItem
                {
                    Value = c.IdCategoria.ToString(),
                    Text = c.NombreCategoria
                });

                return View(platillo);
            }
            catch
            {
                platillo.CategoriasDisponibles = _categoriaHelper.GetCategorias().Select(c => new SelectListItem
                {
                    Value = c.IdCategoria.ToString(),
                    Text = c.NombreCategoria
                });
                return View(platillo);
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

            platillo.CategoriasDisponibles = _categoriaHelper.GetCategorias().Select(c => new SelectListItem
            {
                Value = c.IdCategoria.ToString(),
                Text = c.NombreCategoria,
                Selected = c.IdCategoria == platillo.IdCategoria
            });

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

                if (ModelState.IsValid)
                {
                    var updatedPlatillo = _platilloHelper.Update(platillo);
                    if (updatedPlatillo == null)
                    {
                        return NotFound();
                    }
                    return RedirectToAction(nameof(Index));
                }

                // Recargar dropdown si hay error
                platillo.CategoriasDisponibles = _categoriaHelper.GetCategorias().Select(c => new SelectListItem
                {
                    Value = c.IdCategoria.ToString(),
                    Text = c.NombreCategoria,
                    Selected = c.IdCategoria == platillo.IdCategoria
                });

                return View(platillo);
            }
            catch
            {
                platillo.CategoriasDisponibles = _categoriaHelper.GetCategorias().Select(c => new SelectListItem
                {
                    Value = c.IdCategoria.ToString(),
                    Text = c.NombreCategoria
                });
                return View(platillo);
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

            // Cargar información de categoría para mostrar
            var categoria = platillo.IdCategoria.HasValue ?
                _categoriaHelper.GetCategoria(platillo.IdCategoria.Value) : null;

            ViewBag.CategoriaInfo = categoria != null ? categoria.NombreCategoria : "Sin categoría";

            return View(platillo);
        }

        // POST: PlatilloController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            try
            {
                _platilloHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var platillo = _platilloHelper.GetPlatillo(id);
                if (platillo == null)
                {
                    return NotFound();
                }

                var categoria = platillo.IdCategoria.HasValue ?
                    _categoriaHelper.GetCategoria(platillo.IdCategoria.Value) : null;

                ViewBag.CategoriaInfo = categoria != null ? categoria.NombreCategoria : "Sin categoría";

                return View(platillo);
            }
        }
    }
}
