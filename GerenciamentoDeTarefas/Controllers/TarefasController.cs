using GerenciamentoDeTarefas.Data;
using GerenciamentoDeTarefas.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GerenciamentoDeTarefas.Controllers
{
    public class TarefasController : Controller
    {
        private readonly UsuariosDbContext _context;

        public TarefasController(UsuariosDbContext context)
        {
            _context = context;
        }




        public IActionResult Create()
        {
            var tarefa = new Tarefa();
            return View(tarefa);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult>
            Create([Bind("Id", "Name", "Comentario", "Prazo")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {
                tarefa.Prazo = DateTime.Now;
                _context.Tarefas.Add(tarefa);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index","Usuarios");
            }
            return View(tarefa);
        }
    }
}
