using System.ComponentModel.DataAnnotations;
namespace Herois.WebApi.DTO;

public class EquipeDTO
{
    [Required]
    [StringLength(100)]
    public string Nome { get; set; } = null!;

    [StringLength(255)]
    public string? Descricao { get; set; }
}