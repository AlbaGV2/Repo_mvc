using MiCafeteria.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MiCafeteria.Controllers
{
    public class CuentaController : Controller
    {

        public IActionResult InformacionUsuario()
        {

            return View("_InforUser");
        }


        public IActionResult Login() { 
            return View();
        }

        [HttpPost]
        public IActionResult Login(MiLogin model) {

            //Me falta que pinches en el boton login y te lleve a la accion

            //FALTA VERIFICAR QUE EL NOMBRE Y EL USUARIO CON CORRECTOS
            if (model.Pass == "1234")
            {
                //crear los claims para el login
                var claims = new List<Claim> {


                new Claim(ClaimTypes.Name, model.Nombre),
                new Claim(ClaimTypes.Role, "Admin")


                };
                //crear la identidad
                var identidad = new ClaimsIdentity(claims, "miCookieAuth");
                var principal = new ClaimsPrincipal(identidad);

                //con esto te estas logueando
                HttpContext.SignInAsync("miCookieAuth", principal);

                return RedirectToAction("Index", "Home");

            }
            else {
                return RedirectToAction("Login", "Cuenta");
            }
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
