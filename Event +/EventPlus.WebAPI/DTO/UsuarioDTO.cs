using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class UsuarioDTO
{
    [Required(ErrorMessage = "O nome do usuário é obrigatório!")]
    public string? Nome { get; set; }

    [Required(ErrorMessage = "O email do usuário é obrigatório!")]
    public string? Email { get; set; }

    [Required(ErrorMessage = "A senha do usuário é obrigatória!")]
    public string? Senha { get; set; }

    [Required(ErrorMessage = "A senha do usuário é obrigatória!")]
    public Guid? IdTipoUsuario { get; set; }
}