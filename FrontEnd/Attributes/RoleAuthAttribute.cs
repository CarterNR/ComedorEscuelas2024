using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace FrontEnd.Attributes
{
    public class RoleAuthAttribute : ActionFilterAttribute
    {
        private readonly int[] _allowedRoles;

        public RoleAuthAttribute(params int[] allowedRoles)
        {
            _allowedRoles = allowedRoles;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            var session = context.HttpContext.Session;
            var userRole = session.GetInt32("IdRol");
            var nombreUsuario = session.GetString("NombreUsuario");

            // Permitir acceso si es una acción de logout
            var actionName = context.ActionDescriptor.RouteValues["action"]?.ToString();
            var controllerName = context.ActionDescriptor.RouteValues["controller"]?.ToString();
            
            if (controllerName?.ToLower() == "login" && actionName?.ToLower() == "logout")
            {
                base.OnActionExecuting(context);
                return;
            }

            // Verificar si el usuario está autenticado
            if (string.IsNullOrEmpty(nombreUsuario) || userRole == null)
            {
                context.Result = new RedirectToActionResult("Login", "Login", null);
                return;
            }

            // Verificar si el rol del usuario está en los roles permitidos
            if (!_allowedRoles.Contains(userRole.Value))
            {
                // Redirigir según el rol del usuario
                if (userRole.Value == 3) // Estudiante
                {
                    var idUsuario = session.GetInt32("IdUsuario");
                    context.Result = new RedirectToActionResult("Index", "Estudiante", new { id = idUsuario });
                }
                else if (userRole.Value == 2) // Escuela
                {
                    context.Result = new RedirectToActionResult("Index", "ProductoDia", null);
                }
                else // Administrador
                {
                    context.Result = new RedirectToActionResult("Index", "Home", null);
                }
                return;
            }

            base.OnActionExecuting(context);
        }
    }
} 