using Herois.WebApi.BdContextHerois;
using Herois.WebApi.Interfaces;
using Herois.WebApi.Models;

public class MissaoRepository : IMissaoRepository
{
    private readonly HeroisContext _context;

    public MissaoRepository(HeroisContext context)
    {
        _context = context;
    }

    public List<Missao> Listar() =>
        _context.Missaos.ToList();

    public Missao? BuscarPorId(int id) =>
        _context.Missaos.Find(id);

    public void CadastrarMissao(Missao missao)
    {
        _context.Missaos.Add(missao);
        _context.SaveChanges();
    }

    public void AtualizarMissao(int id, Missao missao)
    {
        var existente = _context.Missaos.Find(id);
        if (existente == null) return;

        existente.Nome = string.IsNullOrWhiteSpace(missao.Nome) ? existente.Nome : missao.Nome;
        existente.Descricao = string.IsNullOrWhiteSpace(missao.Descricao) ? existente.Descricao : missao.Descricao;
        existente.HeroiId = missao.HeroiId != 0 ? missao.HeroiId : existente.HeroiId;
        existente.EquipeId = missao.EquipeId != 0 ? missao.EquipeId : existente.EquipeId;

        _context.SaveChanges();
    }

    public void DeletarMissao(int id)
    {
        var missao = _context.Missaos.Find(id);
        if (missao == null) return;

        _context.Missaos.Remove(missao);
        _context.SaveChanges();
    }
}