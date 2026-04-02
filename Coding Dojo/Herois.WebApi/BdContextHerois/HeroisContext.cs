using System;
using System.Collections.Generic;
using Herois.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Herois.WebApi.BdContextHerois;

public partial class HeroisContext : DbContext
{
    public HeroisContext()
    {
    }

    public HeroisContext(DbContextOptions<HeroisContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Equipe> Equipes { get; set; }

    public virtual DbSet<Heroi> Herois { get; set; }

    public virtual DbSet<Missao> Missaos { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=(localdb)\\MSSQLLocalDB;Database=Herois;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Equipe>(entity =>
        {
            entity.HasKey(e => e.IdEquipe).HasName("PK__Equipe__D8052412D2E95381");
        });

        modelBuilder.Entity<Heroi>(entity =>
        {
            entity.HasKey(e => e.IdHeroi).HasName("PK__Heroi__C6D83F1560C87F60");

            entity.HasOne(d => d.Equipe).WithMany(p => p.Herois)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Heroi__EquipeId__5EBF139D");
        });

        modelBuilder.Entity<Missao>(entity =>
        {
            entity.HasKey(e => e.IdMissao).HasName("PK__Missao__51FA0AD5C071CFFE");

            entity.HasOne(d => d.Equipe).WithMany(p => p.Missaos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Missao__EquipeId__628FA481");

            entity.HasOne(d => d.Heroi).WithMany(p => p.Missaos)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Missao__HeroiId__619B8048");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
