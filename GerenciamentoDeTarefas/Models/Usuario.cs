// Usuario.cs
using GerenciamentoDeTarefas.Models.Enums;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

public class Usuario
{
    public Guid Id { get; set; }

    [Required(ErrorMessage = "Nome obrigatório")]
    [StringLength(50, ErrorMessage = "Nome muito grande", MinimumLength = 3)]
    public string Nome { get; set; }

    [Required(ErrorMessage = "Email obrigatório")]
    [EmailAddress(ErrorMessage ="Email incorreto")]
    public string Email { get; set; }

    [Required(ErrorMessage = "Senha obrigatório")]
    
    public string Senha { get; set; }

    public DateTime DataAtual { get; set; }
    [Required(ErrorMessage = "Selecione Perfil obrigatório")]
    public PerfilEnum  Perfil { get; set; }

}
