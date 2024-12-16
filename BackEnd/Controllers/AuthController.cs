using Azure;
using BackEnd.DTO;
using BackEnd.Services.Implementations;
using BackEnd.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager;
        private readonly RoleManager<IdentityRole> roleManager;
        private ITokenService TokenService;

        public AuthController(UserManager<IdentityUser> userManager,
                                RoleManager<IdentityRole> roleManager,
                                ITokenService tokenService)
        {
            this.userManager = userManager;
            this.roleManager = roleManager;
            this.TokenService = tokenService;
        }




        [HttpPost]
        [Route("Login")]

        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {

            IdentityUser user = await userManager.FindByNameAsync(model.Username);
            LoginModel Usuario = new LoginModel();
            if (user != null && await userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var jwtToken = TokenService.CreateToken(user, userRoles.ToList());

                Usuario.Token = jwtToken;
                Usuario.Roles = userRoles.ToList(); 
                Usuario.Username = user.UserName;

                return Ok(Usuario);
            }
            var loging = new LoginModel
            {
                Username = model.Username,
                Password = model.Password
            };
            // Crear una lista de claims con los datos del usuario autenticado.
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, model.Username), // Asigna el Username del modelo
                new Claim(ClaimTypes.Name, model.Username)            // Asigna el Username del modelo
            };


            return Unauthorized();
        }




        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterModel model)
        {
            var userExists = await userManager.FindByNameAsync(model.Username);

            if (userExists != null)
            {
                return StatusCode(StatusCodes.Status409Conflict, "El usuario ya existe.");
            }

            IdentityUser user = new IdentityUser
            {
                Email = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                UserName = model.Username
            };

            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Error al crear el usuario.");
            }

            // Asignar el rol al usuario
            if (!string.IsNullOrEmpty(model.Role))
            {
                if (!await roleManager.RoleExistsAsync(model.Role))
                {
                    return StatusCode(StatusCodes.Status400BadRequest, "El rol no existe.");
                }

                await userManager.AddToRoleAsync(user, model.Role);
            }

            return Ok(new { Message = "Usuario registrado exitosamente" });
        }



        [HttpPost]
        [Route("logout")]
        public IActionResult Logout()
        {
            return Ok(new { Message = "Sesión cerrada correctamente" });
        }



        [HttpGet]
        [Route("users")]
        public async Task<IActionResult> GetAllUsers()
        {
            // Obtén la lista de usuarios
            var users = userManager.Users.Select(user => new
            {
                user.Id,
                user.UserName,
                user.Email
            });

            return Ok(users);
        }







        [HttpGet]
        [Route("details/{id}")]
        public async Task<IActionResult> GetUserDetails(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var roles = await userManager.GetRolesAsync(user);
            var model = new UserDetailsModel
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                Roles = roles.ToList(),
                Password = "**********"  // No deberías mostrar la contraseña en texto claro, solo un indicador
            };

            return Ok(model);
        }

        [HttpPut]
        [Route("update/{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] UpdateUserModel model)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            user.UserName = model.UserName;
            user.Email = model.Email;

            var result = await userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }

        [HttpDelete]
        [Route("delete/{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            var user = await userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var result = await userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }

            return Ok();
        }
    }



}

