using MiCafeteria.Core.Entities;
using MiCafeteria.Core.Enums;
using MiCafeteria.Data.Contexts;
using MiCafeteria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MiCafeteria.Controllers
{
    public class UsuarioController : Controller
    {
        MyDbContext _context;



        public UsuarioController(MyDbContext context)
        {
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
            return View();
        }


        //---------------------------------CREATE----------------------------------

        public IActionResult CreateUruario()
        {
            // Sin rol 
            ViewBag.Roles = GetRolesSelectList(); 
            return View();
        }
        

        [HttpPost]
        public IActionResult CreateUsuario(Usuario model)
        {

            if (!ModelState.IsValid)
            {
                ViewBag.Roles = GetRolesSelectList(model.Rol);
                return View(model.Rol);
            }

            // Hacer una migracion..BD..

            return RedirectToAction(nameof(Index));
        }

        //---------------------------------EDIT------------------------------------

        public IActionResult Edit(int id)
        {
            ViewBag.Id = id;
            //return View(/*categorias[id]*/);

            var usuario = _context.Usuarios.Find(id);

            ViewBag.Roles = GetRolesSelectList(usuario.Rol);

            return View(usuario);


        }
        [HttpPost]
        public IActionResult Edit(/*int id, Categoria model*/)
        {
            //Usuario[id] = model;
            return RedirectToAction(nameof(Index));
        }

        //---------------------------------DELETE------------------------------------


        public IActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View();
        }


    }
}
