using GerenciamentoDeTarefas.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace GerenciamentoDeTarefas.Data
{

    public class UsuariosDbContext : DbContext
    {
        public UsuariosDbContext(DbContextOptions<UsuariosDbContext> options) : base(options)
        {
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarefa> Tarefas { get; set; }


    }
    
}
