using ExamenTienda.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExamenTienda.Models;

namespace ExamenTienda.Controllers
{
    public class ManagedController : Controller
    {

        private RepositoryTienda repo;
        public ManagedController(RepositoryTienda repo)
        {
            this.repo = repo;
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login
            (string username, string password)
        {
            //string passw = password;
            Usuario user = await
                this.repo.LogInUsuarioAsync(username, password);
            if (user != null)
            {
                ClaimsIdentity identity =
                    new ClaimsIdentity(
                        CookieAuthenticationDefaults.AuthenticationScheme,
                        ClaimTypes.Name, ClaimTypes.Role);
                
                Claim claimName =
                    new Claim(ClaimTypes.Name, user.Nombre);
                identity.AddClaim(claimName);

                Claim claimId = 
                    new Claim(ClaimTypes.NameIdentifier, user.IdUsuario.ToString());
                identity.AddClaim(claimId);

                Claim claimEmail =
                    new Claim("Email", user.Email);
                identity.AddClaim(claimEmail);

                Claim claimApellidos =
                    new Claim("Apellidos", user.Apellidos);
                identity.AddClaim(claimApellidos);

                Claim claimPassw =
                    new Claim("Passw", user.Pass);
                identity.AddClaim(claimPassw);

                Claim claimFoto =
                    new Claim("Foto", user.Foto);
                identity.AddClaim(claimFoto);

                ClaimsPrincipal userPrincipal =
                    new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync(
                    CookieAuthenticationDefaults.AuthenticationScheme,
                    userPrincipal);
                
                return RedirectToAction("PerfilUsuario", "Libros");
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
            return RedirectToAction("Index", "Libros");
        }

    }
}
