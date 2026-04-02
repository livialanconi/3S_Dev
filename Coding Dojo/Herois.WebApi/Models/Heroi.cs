using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Herois.WebApi.Models;

[Table("Heroi")]
public partial class Heroi
{
    [Key]
    public int IdHeroi { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string Poder { get; set; } = null!;

    public int EquipeId { get; set; }

    [ForeignKey("EquipeId")]
    [InverseProperty("Herois")]
    public virtual Equipe Equipe { get; set; } = null!;

    [InverseProperty("Heroi")]
    public virtual ICollection<Missao> Missaos { get; set; } = new List<Missao>();
}
