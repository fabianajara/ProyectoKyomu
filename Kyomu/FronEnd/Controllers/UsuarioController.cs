using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FronEnd.Controllers
{
    public class UsuarioController : Controller
    {
        IUsuarioHelper _usuarioHelper;
        public UsuarioController(IUsuarioHelper usuarioHelper)
        {
            _usuarioHelper = usuarioHelper;
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
            return View();
        }

        // POST: UsuarioController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(UsuarioViewModel usuario)
        {
            try
            {
                _usuarioHelper.Add(usuario);
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
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
            return View(usuario);
        }

        // POST: UsuarioController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, UsuarioViewModel usuario)
        {
            try
            {
                if (id != usuario.IdUsuario)
                {
                    return BadRequest();
                }


                var updatedUsuario = _usuarioHelper.Update(usuario);
                if (updatedUsuario == null)
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

        // GET: UsuarioController/Delete/5
        public ActionResult Delete(int id)
        {
            var usuario = _usuarioHelper.GetUsuario(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        // POST: UsuarioController/Delete/5
        [HttpPost]
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
                return View();
            }
        }
    }
}
