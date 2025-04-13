using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.IO;
using System.Linq;

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
        public ActionResult Create(PlatilloViewModel platillo, IFormFile imagenFile)
        {
            try
            {
                // Si se envió un archivo, se guarda y se asigna la ruta al modelo
                if (imagenFile != null && imagenFile.Length > 0)
                {
                    platillo.Imagen = SaveImage(imagenFile);
                }

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
        public ActionResult Edit(int id, PlatilloViewModel platillo, IFormFile imagenFile)
        {
            try
            {
                if (id != platillo.IdPlatillo)
                {
                    return BadRequest();
                }

                // Si se envió una nueva imagen, se guarda y se actualiza la propiedad
                if (imagenFile != null && imagenFile.Length > 0)
                {
                    platillo.Imagen = SaveImage(imagenFile);
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
        /// <summary>
        /// Guarda la imagen en la carpeta wwwroot/images/platillos y retorna la ruta relativa.
        /// </summary>
        /// <param name="imagenFile">Archivo de imagen subido.</param>
        /// <returns>Ruta relativa de la imagen.</returns>
        private string SaveImage(IFormFile imagenFile)
        {
            // Generar un nombre único para evitar colisiones
            var fileName = Path.GetFileName(imagenFile.FileName);
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

            // Ruta de la carpeta destino (wwwroot/images/platillos)
            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "platillos");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imagenFile.CopyTo(stream);
            }

            // Retorna la ruta relativa para ser usada en la vista y en el modelo
            return "/images/platillos/" + uniqueFileName;
        }
    }
}
