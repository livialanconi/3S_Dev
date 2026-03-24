using System.ComponentModel.DataAnnotations;

namespace EventPlus_.WebAPI.DTO;

public class EventoDTO
{
    [Required(ErrorMessage = "O titulo do tipo de Usuário é obrigatório!")]
    public string? Nome { get; set; }
    public string? Descricao { get; set; }
    public DateTime? DateTime { get; set; }

    public Guid? IdInstituicao { get; set; }

    public Guid? IdTipoEvento { get; set; }


}
