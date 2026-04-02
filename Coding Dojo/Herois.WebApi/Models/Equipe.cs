using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace Herois.WebApi.Models;

[Table("Equipe")]
public partial class Equipe
{
    [Key]
    public int IdEquipe { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string Nome { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string Descricao { get; set; } = null!;

    [InverseProperty("Equipe")]
    public virtual ICollection<Heroi> Herois { get; set; } = new List<Heroi>();

    [InverseProperty("Equipe")]
    public virtual ICollection<Missao> Missaos { get; set; } = new List<Missao>();
}
