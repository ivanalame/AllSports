using AllSports.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using AllSports.Models;

namespace AllSports.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryUsuarios repo;

        public ManagedController(RepositoryUsuarios repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
       

        public async Task<IActionResult> Login (string email, string password)

        {
            Usuario usuario = await  this.repo.LogInSeguridad(email, password);

            if (usuario != null)

            {

                //SEGURIDAD 
                ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme,ClaimTypes.Name, ClaimTypes.Role);

                //CREAMOS EL CLAIM PARA EL NOMBRE (APELLIDO) 

                Claim claimEmail = new Claim(ClaimTypes.Name, usuario.Email);

                identity.AddClaim(claimEmail);
                Claim claimIdUser = new Claim(ClaimTypes.NameIdentifier, usuario.IdUsuario.ToString());
                identity.AddClaim(claimIdUser);
                Claim claimNombre = new Claim("Nombre", usuario.Nombre);
                identity.AddClaim(claimNombre);
                Claim claimApellidos = new Claim("Apellidos", usuario.Apellidos);
                identity.AddClaim(claimApellidos);
                Claim claimNif = new Claim("Nif", usuario.Nif.ToString());
                identity.AddClaim(claimNif);

                Claim claimRole = new Claim(ClaimTypes.Role, usuario.IdRolUsuario.ToString());
                identity.AddClaim(claimRole);
               

                ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);


                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal);
                //LO VAMOS A LLEVAR A UNA VISTA CON LA INFORMACION QUE NOS DEVUELVE EL FILTER EN TEMPDATA
                string controller = TempData["controller"].ToString();
                string action = TempData["action"].ToString();
                return RedirectToAction(action, controller);
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }
        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}

