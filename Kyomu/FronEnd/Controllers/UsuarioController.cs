using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Controllers
{
    public class UsuarioController : Controller
    {
        IUsuarioHelper _usuarioHelper;
        IRolHelper _rolHelper;

        public UsuarioController(IUsuarioHelper usuarioHelper, IRolHelper rolHelper)
        {
            _usuarioHelper = usuarioHelper;
            _rolHelper = rolHelper;
        }

        // GET: UsuarioController
        public ActionResult Index()
        {
            var result = _usuarioHelper.GetUsuarios();
            return View(result);
        }

        // GET: UsuarioController/Details/5
        public ActionResult Details(int id)
        {
            var result = _usuarioHelper.GetUsuario(id);
            return View(result);
        }

        // GET: UsuarioController/Create
        public ActionResult Create()
        {
            var model = new UsuarioViewModel
            {
                RolesDisponibles = _rolHelper.GetRoles().Select(r => new SelectListItem
                {
                    Value = r.IdRol.ToString(),
                    Text = r.NombreRol
                })
            };
            return View(model);
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioViewModel usuario, IFormFile imagenFile)
        {
            try
            {
                if (imagenFile == null)
                {
                    ModelState.Remove("imagenFile");
                }

                if (imagenFile != null && imagenFile.Length > 0)
                {
                    usuario.Imagen = SaveImage(imagenFile);
                }

                if (ModelState.IsValid) {
                    _usuarioHelper.Add(usuario);
                    return RedirectToAction(nameof(Index));
                }

                var model = new UsuarioViewModel
                {
                    RolesDisponibles = _rolHelper.GetRoles().Select(r => new SelectListItem
                    {
                        Value = r.IdRol.ToString(),
                        Text = r.NombreRol
                    })
                };
                return View(model);
            }
            catch
            {
                var model = new UsuarioViewModel
                {
                    RolesDisponibles = _rolHelper.GetRoles().Select(r => new SelectListItem
                    {
                        Value = r.IdRol.ToString(),
                        Text = r.NombreRol
                    })
                };
                return View(model);
            }
        }

        // GET: UsuarioController/Edit/5
        public ActionResult Edit(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            usuario.RolesDisponibles = _rolHelper.GetRoles().Select(c => new SelectListItem
            {
                Value = c.IdRol.ToString(),
                Text = c.NombreRol,
                Selected = c.IdRol == usuario.IdRol
            });

            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsuarioViewModel usuario, IFormFile imagenFile)
        {
            try
            {
                if (id != usuario.IdUsuario)
                {
                    return BadRequest();
                }

                if (imagenFile == null)
                {
                    ModelState.Remove("imagenFile");
                }

                if (imagenFile != null && imagenFile.Length > 0)
                {
                    usuario.Imagen = SaveImage(imagenFile);
                }

                if (ModelState.IsValid)
                {
                    var updatedUsuario = _usuarioHelper.Update(usuario);
                    if (updatedUsuario == null)
                    {
                        return NotFound();
                    }

                    return RedirectToAction(nameof(Index));
                }

                usuario.RolesDisponibles = _rolHelper.GetRoles().Select(c => new SelectListItem
                {
                    Value = c.IdRol.ToString(),
                    Text = c.NombreRol,
                    Selected = c.IdRol == usuario.IdRol
                });

                return View(usuario);

            }
            catch
            {
                usuario.RolesDisponibles = _rolHelper.GetRoles().Select(c => new SelectListItem
                {
                    Value = c.IdRol.ToString(),
                    Text = c.NombreRol,
                    Selected = c.IdRol == usuario.IdRol
                });

                return View(usuario);
            }
        }

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }

            var rol = usuario.IdRol.HasValue ?
                _rolHelper.GetRol(usuario.IdRol.Value) : null;

            ViewBag.RolInfo = rol != null ? rol.NombreRol : "Sin rol";

            return View(usuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, UsuarioViewModel usuario)
        {
            try
            {

                _usuarioHelper.Delete(id);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var usuarioToDelete = _usuarioHelper.GetUsuario(id);
                if (usuarioToDelete == null)
                {
                    return NotFound();
                }

                var categoria = usuarioToDelete.IdRol.HasValue ?
                    _rolHelper.GetRol(usuarioToDelete.IdRol.Value) : null;

                ViewBag.CategoriaInfo = categoria != null ? categoria.NombreRol : "Sin rol";

                return View(usuarioToDelete);
            }
        }

        private string SaveImage(IFormFile imagenFile)
        {

            var fileName = Path.GetFileName(imagenFile.FileName);
            var uniqueFileName = Guid.NewGuid().ToString() + "_" + fileName;

            var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", "usuarios");
            if (!Directory.Exists(folderPath))
            {
                Directory.CreateDirectory(folderPath);
            }

            var filePath = Path.Combine(folderPath, uniqueFileName);
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                imagenFile.CopyTo(stream);
            }

            return "/images/usuarios/" + uniqueFileName;
        }
    }
}
