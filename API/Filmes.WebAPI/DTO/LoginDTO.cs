using System.ComponentModel.DataAnnotations;

namespace Filmes.WebAPI.DTO;

public class LoginDTO
{
    [Required(ErrorMessage = "O email do usuário é obrigatório!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "O senha do usuário é obrigatória!")]
    public string? Senha { get; set; }
}