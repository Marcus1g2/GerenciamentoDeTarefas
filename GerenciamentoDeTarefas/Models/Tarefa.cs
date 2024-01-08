namespace GerenciamentoDeTarefas.Models

{
    public class Tarefa
    {

            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Comentario { get; set; }

            public DateTime Prazo { get; set; }

            public Guid UsuarioId { get; set; }
            public Usuario Usuario { get; set; }


    }
}
