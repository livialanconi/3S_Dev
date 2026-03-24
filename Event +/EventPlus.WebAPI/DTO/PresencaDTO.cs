using System.ComponentModel.DataAnnotations;

namespace EventPlus.WebAPI.DTO;

public class PresencaDTO
{
    [Required(ErrorMessage = "O id do evento é obrigatório!")]
    public Guid IdEvento { get; set; }

    [Required(ErrorMessage = "O id do usuário é obrigatório!")]
    public Guid IdUsuario { get; set; }

    public bool Situacao { get; set; }
}
