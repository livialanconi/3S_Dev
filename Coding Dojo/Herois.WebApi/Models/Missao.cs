using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Herois.WebApi.Models;

[Table("Missao")]
public partial class Missao
{
    [Key]
    public int IdMissao { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    public int HeroiId { get; set; }

    public int EquipeId { get; set; }

    [ForeignKey("EquipeId")]
    [InverseProperty("Missaos")]
    public virtual Equipe Equipe { get; set; } = null!;

    [ForeignKey("HeroiId")]
    [InverseProperty("Missaos")]
    public virtual Heroi Heroi { get; set; } = null!;
}
