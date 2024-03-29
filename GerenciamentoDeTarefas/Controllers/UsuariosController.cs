﻿using GerenciamentoDeTarefas.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
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
            var usuario = new Usuario();
            return View(usuario);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Cadrastro([Bind("Id", "Nome", "Email", "Senha", "Perfil")] Usuario usuario)
        {
            if (ModelState.IsValid)
            {
                usuario.DataAtual = DateTime.Now;
                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();
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
        public async Task<IActionResult> Login(string email, string senha)
        {
            var UserId = await _context.Usuarios.SingleOrDefaultAsync(s => s.Email == email && s.Senha == senha);
            if (UserId != null)
            {
                HttpContext.Session.SetString("UserId", UserId.Id.ToString());
                return RedirectToAction("Create", "Tarefas");
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
