using GerenciamentoDeTarefas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Globalization;

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
            var usuario=new Usuario();
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Cadrastro([Bind("Id", "Nome", "Email", "Senha", "Perfil")] Usuario usuario)
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
            var UserId = _context.Usuarios.SingleOrDefault(s=>s.Email == email && s.Senha==senha);
            if (UserId != null)
            {
                HttpContext.Session.SetString("UserId", UserId.Id.ToString());
                return RedirectToAction(nameof(Acesso));
            }
            TempData["Erro"] = "Email ou senha incorreto(s)";
            return View();

        }
        public IActionResult Acesso()
        {
            return View();
        }
    }
}
