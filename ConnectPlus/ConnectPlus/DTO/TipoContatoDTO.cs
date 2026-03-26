using System.ComponentModel.DataAnnotations;

namespace ConnectPlus.DTO;

public class TipoContatoDTO
{
    [Required(ErrorMessage = "O título do tipo de usuário é obrigatório.")]
    public string? Titulo { get; set; }
}