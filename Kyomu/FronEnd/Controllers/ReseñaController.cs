using FronEnd.Helpers.Implementations;
using FronEnd.Helpers.Interfaces;
using FronEnd.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace FronEnd.Controllers
{
    public class ReseñaController : Controller
    {
        IReseñaHelper _reseñaHelper;
        IUsuarioHelper _usuarioHelper;
        IPlatilloHelper _platilloHelper;
        public ReseñaController(IReseñaHelper reseñaHelper, IUsuarioHelper usuarioHelper, IPlatilloHelper platilloHelper)
        {
            _reseñaHelper = reseñaHelper;
            _usuarioHelper = usuarioHelper;
            _platilloHelper = platilloHelper;
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
            var reseña = _reseñaHelper.GetReseña(id);
            if (reseña == null)
                return NotFound();

            reseña.UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u =>
                new SelectListItem { Value = u.IdUsuario.ToString(), Text = u.Nombre });
            reseña.PlatillosDisponibles = _platilloHelper.GetPlatillos().Select(p =>
                new SelectListItem { Value = p.IdPlatillo.ToString(), Text = p.Nombre });

            return View(reseña);
        }

        // GET: ReseñaController/Create
        public ActionResult Create()
        {
            var model = new ReseñaViewModel
            {
                UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u =>
                    new SelectListItem { Value = u.IdUsuario.ToString(), Text = u.Nombre }),
                PlatillosDisponibles = _platilloHelper.GetPlatillos().Select(p =>
                    new SelectListItem { Value = p.IdPlatillo.ToString(), Text = p.Nombre })
            };
            return View(model);
        }

        // POST: ReseñaController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(ReseñaViewModel reseña)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _reseñaHelper.Add(reseña);
                    return RedirectToAction(nameof(Index));
                }

                reseña.UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u =>
                    new SelectListItem { Value = u.IdUsuario.ToString(), Text = u.Nombre });
                reseña.PlatillosDisponibles = _platilloHelper.GetPlatillos().Select(p =>
                    new SelectListItem { Value = p.IdPlatillo.ToString(), Text = p.Nombre });

                return View(reseña);
            }
            catch
            {
                reseña.UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u =>
                    new SelectListItem { Value = u.IdUsuario.ToString(), Text = u.Nombre });
                reseña.PlatillosDisponibles = _platilloHelper.GetPlatillos().Select(p =>
                    new SelectListItem { Value = p.IdPlatillo.ToString(), Text = p.Nombre });
                return View(reseña);
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

            reseña.UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u =>
                new SelectListItem
                {
                    Value = u.IdUsuario.ToString(),
                    Text = u.Nombre,
                    Selected = u.IdUsuario == reseña.IdUsuario
                });
            reseña.PlatillosDisponibles = _platilloHelper.GetPlatillos().Select(p =>
                new SelectListItem
                {
                    Value = p.IdPlatillo.ToString(),
                    Text = p.Nombre,
                    Selected = p.IdPlatillo == reseña.IdPlatillo
                });
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

                if (ModelState.IsValid)
                {
                    var updatedReseña = _reseñaHelper.Update(reseña);
                    if (updatedReseña == null)
                    {
                        return NotFound();
                    }
                    return RedirectToAction(nameof(Index));
                }

                reseña.UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u =>
                    new SelectListItem
                    {
                        Value = u.IdUsuario.ToString(),
                        Text = u.Nombre,
                        Selected = u.IdUsuario == reseña.IdUsuario
                    });
                reseña.PlatillosDisponibles = _platilloHelper.GetPlatillos().Select(p =>
                    new SelectListItem
                    {
                        Value = p.IdPlatillo.ToString(),
                        Text = p.Nombre,
                        Selected = p.IdPlatillo == reseña.IdPlatillo
                    });
                return View(reseña);
            }
            catch
            {
                reseña.UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u =>
                    new SelectListItem { Value = u.IdUsuario.ToString(), Text = u.Nombre });
                reseña.PlatillosDisponibles = _platilloHelper.GetPlatillos().Select(p =>
                    new SelectListItem { Value = p.IdPlatillo.ToString(), Text = p.Nombre });
                return View(reseña);
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
            reseña.UsuariosDisponibles = _usuarioHelper.GetUsuarios().Select(u =>
                new SelectListItem
                {
                    Value = u.IdUsuario.ToString(),
                    Text = u.Nombre,
                    Selected = u.IdUsuario == reseña.IdUsuario
                });
            reseña.PlatillosDisponibles = _platilloHelper.GetPlatillos().Select(p =>
                new SelectListItem
                {
                    Value = p.IdPlatillo.ToString(),
                    Text = p.Nombre,
                    Selected = p.IdPlatillo == reseña.IdPlatillo
                });
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
