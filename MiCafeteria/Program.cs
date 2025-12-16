using MiCafeteria.Data.Contexts;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//INI: EF conf para entityframe.. inyeccion por dependencias
string cnnString = builder.Configuration.GetConnectionString("DefaultConnection") ?? "";
//disponible en todos los controladores del proyecto.Con un contructor
builder.Services.AddDbContext<MyDbContext>(options =>
options.UseSqlServer(cnnString));




//Ini: nuestra configuracion para simular la autentificacion
builder.Services.AddAuthentication("miCookieAuth")
//Controlado que gestiona el login de un usuario
    .AddCookie("miCookieAuth", opt => {
        //controlado que gestiona el login de un usuario
         opt.LoginPath = "/Cuenta/Login";
     });

builder.Services.AddAuthorization();
//Fin: nuestra configuracion para simular la autentificacion

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

//añadimos esta linea para poder usar la autentificacion
app.UseAuthentication(); //Habilitamos la autentificacion

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

