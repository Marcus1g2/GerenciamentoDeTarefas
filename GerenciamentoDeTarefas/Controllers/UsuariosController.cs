using GerenciamentoDeTarefas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace GerenciamentoDeTarefas.Controllers
{
    public class UsuariosController : Controller
    {
        private readonly UsuariosDbContext _context;

        public UsuariosController(UsuariosDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return _context.Usuarios != null ?
                View(_context.Usuarios.ToList()) :
                Problem("Entity set 'UsuariosDbContext.Usuarios'  is null.");
        }

        public IActionResult Cadrastro()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadrastro([Bind("Id", "Nome", "Email", "Senha")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.DataAtual = DateTime.Now;
                _context.Usuarios.Add(usuario);
                _context.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            return View(usuario);
        }

        public IActionResult Login()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Login(string email, string senha)
        {

            var User = _context.Usuarios.FirstOrDefault(f => f.Email == email && f.Senha == senha);

            if (User != null)
            {
                HttpContext.Session.SetString("UserId", User.Id.ToString());
                return RedirectToAction(nameof(Acesso));
            }
            ModelState.AddModelError(String.Empty, "Login ou senha invalidos");
            return View();
        }
        public IActionResult Acesso()
        {
            return View();
        }
    }
}
