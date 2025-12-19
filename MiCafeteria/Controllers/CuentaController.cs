using MiCafeteria.Core.Entities;
using MiCafeteria.Data.Contexts;
using MiCafeteria.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Threading.Tasks;

namespace MiCafeteria.Controllers
{
    public class CuentaController(IPasswordHasher<Usuario> passwordHasher, MyDbContext context) : Controller
    {

        MyDbContext _context = context;
        private readonly IPasswordHasher<Usuario> _passwordHasher = passwordHasher;

        public IActionResult InformacionUsuario()
        {

            return View("_InforUser");
        }


        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(Usuario model)
        {
            //PARA VERIFICAR QUE LA CONTRASEÑA INTRODUCIDA ES IGUAL A LA DE LA BASE DE DATOS

            var usuario = await _context.Usuarios
                .FirstOrDefaultAsync(u => u.Email == model.Email);

            if (usuario == null)
            {
                return View(model);
            }

            var result = _passwordHasher.VerifyHashedPassword(usuario, usuario.Password, model.Password);

            //Si la contraseña es correcta...
            if (result == PasswordVerificationResult.Success)
            {






                //ARREGALR LO SIGUIENTE EN EL SUCCESS

                //Me falta que pinches en el boton login y te lleve a la accion

                //FALTA VERIFICAR QUE EL NOMBRE Y EL USUARIO CON CORRECTOS

                //crear los claims para el login

                var claims = new List<Claim>()
                {


                    new Claim(ClaimTypes.Name, model.NombreCompleto),
                    new Claim(ClaimTypes.Role, "Admin")
                };


                //crear la identidad
                var identidad = new ClaimsIdentity(claims, "miCookieAuth");
                var principal = new ClaimsPrincipal(identidad);

                //con esto te estas logueando
                HttpContext.SignInAsync("miCookieAuth", principal);

                return RedirectToAction("Index", "Home");

            }
      

            return View(model);

        }




        //public IActionResult Login(string rol = "Gerente")
        //{
        ////crear los claims
        //var claims = new List<Claim> {


        //new Claim(ClaimTypes.Name, "Alba"),
        //new Claim(ClaimTypes.Role, rol)


        //};
        ////crear la identidad
        //var identidad = new ClaimsIdentity(claims, "miCookieAuth");
        //var principal = new ClaimsPrincipal(identidad);

        ////con esto te estas logueando
        //HttpContext.SignInAsync("miCookieAuth", principal);

        ////redireccionar a la pagina principal
        ////return RedirectToAction(nameof(Index), "Home");
        //return View("_InforUser");
        //}



        //Cerrar sesion
        //public IActionResult Logout()
        //{

        //    //LO mismo con logout
        //    //cerrar la sesion
        //    HttpContext.SignOutAsync("miCookieAuth");
        //    return RedirectToAction(nameof(CuentaController.Login), "Cuenta");
        //}

        ///Otra forma de hacer el logout
        public async Task<IActionResult> logout()
        {
            //Cerramos la sesion
            await HttpContext.SignOutAsync("miCookieAuth");

            return RedirectToAction("Index", "Home");
        }
    }
}
