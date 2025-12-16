using MiCafeteria.Core.Entities;
using MiCafeteria.Data.Contexts;
using MiCafeteria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.Threading.Tasks;
using System.Timers;
using static System.Runtime.InteropServices.JavaScript.JSType;


namespace MiCafeteria.Controllers
{
    //unicamente un usuario autenticado puede acceder a este controlador
    //[Authorize(Roles = "Gerente")]
    public class CategoriaController : Controller
    {
        //private static List<Categoria> categorias = new List<Categoria>
        
        //{
        //    new Categoria(){nombre = "Bebidas Calientes", descripcion = "Cafés, tés y más"},
        //    new Categoria(){nombre = "Bebidas Frías", descripcion = "Zumos y refrescos"},
        //    new Categoria(){nombre = "Postres", descripcion = "Dulces y pasteles"}
        //};


        //VIERNES, CREAR UN FILTRO PARA FILTRAR EN CATEGORIA CON UN FORMULARIO DE BUSQUEDA. SIN VISTA, NO HACE FALTA



        MyDbContext _context;

        //El que quiera utilizar mydbcontext tiene que recuperarlo a traves de su constructor
        public CategoriaController(MyDbContext context)
        {
            
            _context = context;
        }

        public IActionResult Index(string nombreFiltro, string descripcion, string criterio)
        {
         
            var categorias = _context.Categorias.AsQueryable();

            if (!string.IsNullOrEmpty(nombreFiltro))
                categorias = categorias.Where(w => w.Nombre.Contains(nombreFiltro));
            if (!string.IsNullOrEmpty(descripcion))
                categorias = categorias.Where(w => w.Descripcion.Contains(descripcion));
            



            //string filtroNombre = "bebida";




            ViewBag.Title = "Listado de Categorías";
            //aqui me traido tofa la carpeta, 
            //var categorias = _context.Categorias.ToList();
            //return View(categorias);

            //Me va a dar una LISTA de todas las categorias que se llames de determinada manera. COSNTRUIMOS LA CONSULTA.  EJemplos;

            //Ordenar por nombre y descripcion
            if (criterio == "Nombre")
            {

                categorias = categorias.OrderBy(x => x.Nombre);
                //categorias.OrderByDescending(x => x.Nombre);
            }
            else if (criterio == "Descripcion")
            {

                categorias = categorias.OrderBy(x => x.Descripcion);
                //categorias.OrderByDescending(x => x.Descripcion);
            }

            var result = categorias.ToList();


            //var categoriasEquals = _context.Categorias
            //    //filtrar
            //    .Where(x => x.Nombre.Equals("Imane"));

            ////Que empiece por:
            //var categoriasStarts = _context.Categorias
            //    //filtrar
            //    .Where(x => x.Nombre.StartsWith("A"));

            ////Contiene:
            //var categoriasContains = _context.Categorias
            //    //filtrar
            //    .Where(x => x.Nombre.Contains("a"));
            ////Acabe en :
            //var categoriasEnds = _context.Categorias
            //    //filtrar
            //    .Where(x => x.Nombre.EndsWith("s"));


            
            //Retornamos el que mas nos interese. Aun que al final en la practica solo tendremos una unica variable.
            return View(result);
        }

        public IActionResult CreateCategoria()
        {
            return View();
        }
        //cuadno enviamos con un post y cuando recojemos informacion con un get


        //public IActionResult CreatePost(Producto model)
        //{
        //    productos.Add(model);
        //    return View("Index", productos);
        //}


        [HttpPost]
        public IActionResult CreateCategoria([Bind("Id","Nombre", "Descripcion")] Core.Entities.Categoria model)
        {
            if (ModelState.IsValid)
            {
                //con entity framework
                _context.Add(model);
                //guardar los cambios
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            else
            {
                return View(model);
            }
            //categorias.Add(model);
            //return RedirectToAction(nameof(Index));
        }

        public IActionResult Edit(int? id)
        {

            //ViewBag.Id = id;
            //Para que no salga una excepcion
            if (id == 0)
            {
                return NotFound();
            }
            var categoria = _context.Categorias.Find(id);

            if (categoria == null)
            {
                return NotFound();
            }

            return View(categoria);
            //var items = _context.Categorias.ToList();
            //ViewData["Id"] = new SelectList(items,
            //    nameof(Core.Entities.Categoria.Id), nameof(Core.Entities.Categoria.Nombre));
            //return View(model);



            //ViewBag.Id = id;
            //return View(/*categorias[id]*/);
        }




        [HttpPost]
        public IActionResult Edit(int id, MiCafeteria.Core.Entities.Categoria model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            var entidadDb = _context.Categorias.Find(id);
            if (entidadDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                entidadDb.Nombre = model.Nombre;
                entidadDb.Descripcion = model.Descripcion;
                //_context.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //_categorias[id] = model;
            //return RedirectToAction(nameof(Index));
            return View(model);
        }

        //El get hace la peticion y muestra el formulario




        //[Authorize]
        public IActionResult Delete(int id)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria == null)
            {
                return NotFound();
            }
            return View(categoria);
        }


        //[Authorize]
        //ElapsedEventArgs post hace la accion y redirige a index (inicio)
        [HttpPost]
        public IActionResult Delete(int id, MiCafeteria.Core.Entities.Categoria model)
        {
            var categoria = _context.Categorias.Find(id);
            if (categoria != null)
            {
                _context.Categorias.Remove(categoria);
                _context.SaveChanges();
            }
            return RedirectToAction(nameof(Index), model);
        }





    }
}
