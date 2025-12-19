using MiCafeteria.Core.Entities;
using MiCafeteria.Core.Enums;
using MiCafeteria.Data.Contexts;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace MiCafeteria.Controllers
{
    public class UsuarioController : Controller
    {
        MyDbContext _context;
        private readonly IPasswordHasher<Usuario> _passwordHasher;



        public UsuarioController(IPasswordHasher<Usuario> passwordHasher, MyDbContext context)
        {
            _passwordHasher = passwordHasher;
            _context = context;
        }



        private SelectList GetRolesSelectList(RolUsuario? rolSeleccionado = null)
        {
            var roles = Enum.GetValues(typeof(RolUsuario))
                .Cast<RolUsuario>()
                .Select(r => new { Value = r, Text = r.ToString() })
                .ToList();

            return new SelectList(roles, "Value", "Text", rolSeleccionado);
        }



        public IActionResult Index()
        {
            List<Usuario> usuarios = _context.Usuarios.ToList();
            return View(usuarios);
        }


        //---------------------------------CREATE----------------------------------

        public IActionResult CreateUsuario()
        {
            //// Sin rol 
            ViewBag.Roles = GetRolesSelectList();
            return View(/*nameof(Index)*/);
        }


        [HttpPost]
        public IActionResult CreateUsuario(Usuario model)
        {
            string passHasheada = _passwordHasher.HashPassword(model, model.Password);

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = GetRolesSelectList(model.Rol);
                return View(model);
            }
            model.Password = passHasheada;
            _context.Usuarios.Add(model);
            _context.SaveChanges();
            return RedirectToAction(nameof(Index)); ;


        }

        //---------------------------------EDIT------------------------------------

        public IActionResult Edit(int? id)
        {

            //return View(/*categorias[id]*/);
            if (id == 0)
            {
                return NotFound();
            }
            var usuario = _context.Usuarios.Find(id);

            if (usuario == null)
            {
                return NotFound();
            }

            ViewBag.Roles = GetRolesSelectList(usuario.Rol);

            return View(usuario);


        }
        [HttpPost]
        public IActionResult Edit(int id, MiCafeteria.Core.Entities.Usuario model)
        {

            if (id != model.UsuarioId)
            {
                return NotFound();
            }
            var entidadDb = _context.Usuarios.Find(id);
            if (entidadDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                entidadDb.NombreCompleto = model.NombreCompleto;
                entidadDb.Email = model.Email;
                entidadDb.Password = model.Password;
                entidadDb.Rol = model.Rol;
                //_context.Update(model);


                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }

            //Usuario[id] = model;
            ViewBag.Roles = GetRolesSelectList();
            return View(model);
        }

        //---------------------------------DELETE------------------------------------

        public IActionResult Delete(int id)
        {
            var usuario = _context.Usuarios.Find(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return View(usuario);
        }

        [HttpPost]
        public IActionResult Delete(int id, MiCafeteria.Core.Entities.Usuario model)
        {
            ViewBag.Roles = GetRolesSelectList();

            ViewBag.Id = id;
            var usuario = _context.Usuarios.Find(id);
            if (usuario != null)
            {
                _context.Usuarios.Remove(usuario);
                _context.SaveChanges();


            }
            return RedirectToAction(nameof(Index), model);
        }


    }
}
