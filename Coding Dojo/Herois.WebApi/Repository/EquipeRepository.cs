using Herois.WebApi.BdContextHerois;
using Herois.WebApi.Interfaces;
using Herois.WebApi.Models;

namespace Herois.WebApi.Repositories;

public class EquipeRepository : IEquipeRepository
{
    private readonly HeroisContext _context;

    public EquipeRepository(HeroisContext context)
    {
        _context = context;
    }

    public List<Equipe> Listar() =>
        _context.Equipes.ToList();

    public Equipe? BuscarPorId(int id) =>
        _context.Equipes.Find(id);

    public void CadastrarEquipe(Equipe equipe)
    {
        _context.Equipes.Add(equipe);
        _context.SaveChanges();
    }

    public void AtualizarEquipe(int id, Equipe equipe)
    {
        var existente = _context.Equipes.Find(id);
        if (existente == null) return;

        existente.Nome = string.IsNullOrWhiteSpace(equipe.Nome) ? existente.Nome : equipe.Nome;
        existente.Descricao = string.IsNullOrWhiteSpace(equipe.Descricao) ? existente.Descricao : equipe.Descricao;

        _context.SaveChanges();
    }

    public void DeletarEquipe(int id)
    {
        var equipe = _context.Equipes.Find(id);
        if (equipe == null) return;

        _context.Equipes.Remove(equipe);
        _context.SaveChanges();
    }

    public IEnumerable<Equipe> GetAll() => Listar();

    public Equipe? GetById(int id) => BuscarPorId(id);

    public Equipe Create(Equipe equipe)
    {
        CadastrarEquipe(equipe);
        return equipe;
    }

    public void Delete(int id) => DeletarEquipe(id);
}