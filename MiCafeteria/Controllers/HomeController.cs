using MiCafeteria.Core.Entities;
using MiCafeteria.Data.Contexts;
using MiCafeteria.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Diagnostics;

namespace MiCafeteria.Controllers
{
    public class HomeController : Controller
    {
        // Simulamos una lista como base de datos
        private static List<MiCafeteria.Core.Entities.Producto> listaProductos = new List<MiCafeteria.Core.Entities.Producto>();

        private MyDbContext _context;
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger, MyDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            //int id = 0;
            //var producto = _context.Productos.FirstOrDefault(p => p.Id == id);
            //var producto2 = _context.Productos.Find(id);


            //Esto es para que no se vean los botones en home controler por que no nos interesa que se vean aqui
            ViewData["esEditable"] = false;
            ViewData["esDeleteable"] = false;
            ViewData["sePuedeAniadirACesta"] = true;
            ViewData["numProductos"] = listaProductos.Count;

            var categoriasConProductos = _context.Categorias
            .Include(c => c.Productos)
            .ToList();

            return View("Index", categoriasConProductos);
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        //[HttpPost]
        //public IActionResult Create(Producto listaProductos) {

        //    Producto.Id = listaProductos.Count + 1;
        //    listaProductos.Add(Producto);
        //    return RedirectToAction("Index");

        //}

        //[HttpPost]
        //public IActionResult Edit(int id) {
        //    var pr = listaProductos.FirstOrDefault(r => r.Id == id);
        //    return View(reserva);
        //}

        public IActionResult AddToCesta( int Id)
        {

            //TODO: Validar que el producto exista y añadirlo al carrito.
            var producto = _context.Productos.Find(Id);

            //si el producto no es null o existe, se añade
           
                listaProductos.Add(producto);
            

            return RedirectToAction(nameof(Index));

        }

    }
}
