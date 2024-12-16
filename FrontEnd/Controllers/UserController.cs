using FrontEnd.Helpers.Interfaces;
using FrontEnd.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FrontEnd.Controllers
{
    public class UserController : Controller
    {

        private readonly ISecurityHelper securityHelper;

        public UserController(ISecurityHelper securityHelper)
        {
            this.securityHelper = securityHelper;
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Index()
        {
            // Obtén los usuarios con sus roles desde el helper
            var users = securityHelper.GetAllUsers();

            // Mapear de UserAPI a UserViewModel
            var userViewModels = users.Select(user => new UsersViewModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = user.Roles ?? new List<string>() // Asegurarnos de que Roles no sea null
            }).ToList();

            return View(userViewModels); // Pasa la lista de ViewModels a la vista
        }


        [Authorize(Roles = "Admin")]
        public IActionResult Details(string id)
        {
            var userApi = securityHelper.GetUserDetails(id);
            if (userApi == null)
            {
                return NotFound();
            }

            // Convertimos UserAPI a UsersViewModel
            var viewModel = new UsersViewModel
            {
                Id = userApi.Id,
                UserName = userApi.UserName,
                Email = userApi.Email,
                Roles = userApi.Roles,
                Password = userApi.Password
            };

            return View(viewModel);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Edit(string id)
        {
            var userApi = securityHelper.GetUserDetails(id);
            if (userApi == null)
            {
                return NotFound();
            }

            // Convertimos UserAPI a UsersViewModel
            var viewModel = new UsersViewModel
            {
                Id = userApi.Id,
                UserName = userApi.UserName,
                Email = userApi.Email,
                Roles = userApi.Roles
            };

            
            return View(viewModel);
        }

        [HttpPost]
        public IActionResult Edit(UsersViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = securityHelper.UpdateUser(model);
                if (result)
                {
                    return RedirectToAction("Index");
                }
            }

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Delete(string id)
        {
            var result = securityHelper.DeleteUser(id);
            if (result)
            {
                return RedirectToAction("Index");
            }

            return NotFound();
        }
    }
}
