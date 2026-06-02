using System.ComponentModel.DataAnnotations;

namespace FIlmes.WebAPI.DTO
{
    public class GeneroDTO
    {

        [Required(ErrorMessage = "O Nome do gênero é obrigatório!")]

        public string? Nome { get; set; }

    }
}
