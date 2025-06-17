using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.Scripting;



namespace FrontEnd.Controllers
{
    public class EstudianteController : Controller
    {

        private readonly EstudianteContext _context;

        public EstudiantesController(EstudianteContext context)
        {
            _context = context;
        }

        // Login - GET
        public IActionResult Login()
        {
            return View();
        }

        // Login - POST
        [HttpPost]
        public async Task<IActionResult> Login(string cedula, string contrasena)
        {
            var estudiante = await _context.Estudiantes
            .FirstOrDefaultAsync(e => e.Cedula == cedula);

            if (estudiante != null && BCrypt.Net.BCrypt.Verify(contrasena, estudiante.Contrasena))
            {
                HttpContext.Session.SetInt32("EstudianteId", estudiante.Id);
                return RedirectToAction("Perfil");
            }
            else
            {
                ViewBag.Error = "Cédula o contraseña incorrecta.";
                return View();
            }
        }

        // Perfil del estudiante
        public async Task<IActionResult> Perfil()
        {
            var estudianteId = HttpContext.Session.GetInt32("EstudianteId");
            if (estudianteId == null)
            {
                return RedirectToAction("Login");
            }

            var estudiante = await _context.Estudiantes
                .FirstOrDefaultAsync(e => e.Id == estudianteId);

            if (estudiante == null)
            {
                return NotFound();
            }

            // Generación del QR
            var qrCodeUrl = $"https://tusistema.com/comedor/registrar?id={estudiante.Id}";
            ViewBag.QRCodeUrl = qrCodeUrl;

            return View(estudiante);
        }

        // Cerrar sesión
        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }
    }

}

