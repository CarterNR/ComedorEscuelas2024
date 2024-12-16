using FrontEnd.Helpers.Interfaces;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class LogoutController : Controller
    {
        ISecurityHelper securityHelper;

        public LogoutController(ISecurityHelper securityHelper)
        {
            this.securityHelper = securityHelper;
        }

        public async Task<IActionResult> Logout()
        {
            // Limpia el token de sesión
            securityHelper.Logout();

            // Cierra la sesión de autenticación
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirige al login
            return RedirectToAction("Login", "Login");
        }
    }
}
