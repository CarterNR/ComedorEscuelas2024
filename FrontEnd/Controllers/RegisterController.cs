using FrontEnd.ApiModels;
using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class RegisterController : Controller
    {
        private readonly ISecurityHelper securityHelper;

        public RegisterController(ISecurityHelper securityHelper)
        {
            this.securityHelper = securityHelper;
        }

        public IActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                var registerModel = new RegisterAPI
                {
                    Username = model.Username,
                    Password = model.Password,
                    Email = model.Email,
                    Role = model.Role
                };

                var success = securityHelper.Register(registerModel);

                if (success)
                {
                    ViewBag.Message = "Usuario registrado exitosamente.";
                    return RedirectToAction("Index", "Home");
                }

                ViewBag.Message = "Error al registrar el usuario.";
            }

            return View(model);
        }
    }
}
