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



      
        public async Task<IActionResult> Create()
        {
           
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task <IActionResult>Create([Bind("Id", "Name", "Comentario", "Prazo", "UsuarioId")] Tarefa tarefa)
        {
            if (ModelState.IsValid)
            {

                _context.Tarefas.Add(tarefa);
                _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(tarefa);
        }
    }
}
