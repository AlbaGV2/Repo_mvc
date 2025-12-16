using MiCafeteria.Core.Entities;
using MiCafeteria.Data.Contexts;
using MiCafeteria.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace MiCafeteria.Controllers
{
    //[Authorize]
    public class ProductoController : Controller
    {
        //private static List<Producto> productos = new List<Producto>

        //{
        //    new Producto(){Nombre = "cafe", Precio = 1 },
        //    new Producto(){Nombre = "té", Precio = 2 },
        //    new Producto(){Nombre = "zumo de naranja", Precio = 3 }

        //};

        //Variable global para _context
        MyDbContext _context;

        public ProductoController(MyDbContext context)
        {
            _context = context;
        }

        public IActionResult Index(string filtroNomPro, string criterio, int pagina = 1)
        {
            //En este caso nos interesa que los botones se vean en producto cotrolller
            ViewData["esEditable"] = true;
            ViewData["esDeleteable"] = true;


            ViewBag.pagina = pagina;
            int salto = 5;
            int totalProductos = _context.Productos.Count();
            ViewBag.totalPaginas = totalProductos / salto;

            if (totalProductos % salto != 0)
            {
                ViewBag.totalPaginas = ViewBag.totalPaginas + 1;
            }

            var list = _context.Productos
              .Skip((pagina - 1) * salto) //Para que empiece en 0
              .Take(5)
              .ToList(); //esto scaría desde 0 a 5




            //var categoriaConProductos = _context.Productos.ToList();

            //return View(productos);
            //return View(_context.Productos.ToList());
            var productos = _context.Productos.AsQueryable();

            //string.IsNullOrEmpty(filtroNomPro)
            if (filtroNomPro != null && filtroNomPro != "")
                productos = productos.Where(W => W.Nombre.Contains(filtroNomPro));


            if (criterio == "Nombre")
            {

                productos = productos.OrderBy(x => x.Nombre);
               
            }
            else if (criterio == "Descripcion")
            {

                productos = productos.OrderBy(x => x.Precio);
                
            }

           


            //var result = productos.ToList();


            //var productoEquals = _context.Productos
            //    //filtrar
            //    .Where(x => x.Nombre.Equals("Imane"));

            ////Que empiece por:
            //var productosStarts = _context.Productos
            //    //filtrar
            //    .Where(x => x.Nombre.StartsWith("A"));

            ////Contiene:
            //var productosContains = _context.Productos
            //    //filtrar
            //    .Where(x => x.Nombre.Contains("a"));
            ////Acabe en :
            //var productosEnds = _context.Productos
            //    //filtrar
            //    .Where(x => x.Nombre.EndsWith("s"));



            //Retornamos el que mas nos interese. Aun que al final en la practica solo tendremos una unica variable.
            return View(list);
        }
        



        //--------------------------------------------------------------------


        //Para mostrar el nombre del usuario autenticado
        //HttpContext.user.Identity.Name

        //Para usar autorizacion en el controlador
        //[Authorize(Roles = "Admin")]
        public IActionResult Create()
        {



            return View();
        }
        //cuadno enviamos con un post y cuando recojemos informacion con un get
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult Create([Bind("Id", "Nombre", "Precio")] MiCafeteria.Core.Entities.Producto model)
        {
            //Si el estado del modelo es correcto
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

            //productos.Add(model);
            //return View("Index"/*, productos*/);
        }


        //[Authorize(Roles = "Admin")]
        public IActionResult Edit(int? id)
        {
            //ViewBag.Id = id;
            //Para que no salga una excepcion
            if (id <= 0)
            {
               return NotFound(); 
            }
            var model = _context.Productos.Find(id);

            var items = _context.Categorias.ToList();
            ViewData["Id"] = new SelectList(items, 
                nameof(Core.Entities.Categoria.Id), nameof(Core.Entities.Categoria.Nombre));
            return View(model);
        }

        //[Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult Edit(int id, MiCafeteria.Core.Entities.Producto model)
        {
            if (id != model.Id)
            {
                return NotFound();
            }
            var entidadDb = _context.Productos.Find(id);
            if (entidadDb == null)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                entidadDb.Nombre = model.Nombre;
                entidadDb.Precio = model.Precio;
                //_context.Update(model);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //_categorias[id] = model;
            //return RedirectToAction(nameof(Index));
            return View(model);
        }
        




        //[Authorize(Roles = "Admin")]
        //Para usar autorizacion en el controlador
        //[Authorize]
        //El get hace la peticion y muestra el formulario
        public IActionResult Delete(int id)
        {
            ViewBag.Id = id;
            return View(/*productos[id]*/);
        }

        //ElapsedEventArgs post hace la accion y redirige a index (inicio)
        //sealed llaman diferente por que para llamarse igual tienen que tener diferente parametros
        //entonces se llama DeleteConfirmed pero utiliza la misma vista
        //Para usar autorizacion en el controlador
        //[Authorize]
        [HttpPost]
        //[Authorize(Roles = "Admin")]
        public IActionResult DeleteConfirmed(int id)
        {
            //productos.RemoveAt(id);
            return RedirectToAction(nameof(Index));
        }

      

    }
}
