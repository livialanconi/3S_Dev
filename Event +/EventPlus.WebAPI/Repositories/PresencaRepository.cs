using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class PresencaRepository : IPresencaRepository
{
    private readonly EventContext _context;

    public PresencaRepository(EventContext eventContext)
    {
        _context = eventContext; 
    }

    public void Atualizar(Guid IdPresencaEvento)
    {
        var presencaBuscada = _context.Presencas.Find(IdPresencaEvento);
        if (presencaBuscada != null)
        {
            // Alterna o estado da situação (confirma/desconfirma presença)
            presencaBuscada.Situacao = !presencaBuscada.Situacao;
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca uma presença por id
    /// </summary>
    /// <param name="id">id da presença a ser buscada</param>
    /// <returns>presença buscada</returns>

    public Presenca BuscarPorId(Guid id)
    {
        return _context.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).FirstOrDefault(p => p.IdPresenca == id)!;
    }

    public void Deletar(Guid id)
    {
        var presencaBuscada = _context.Presencas.Find(id);
        if (presencaBuscada != null)
        {
            _context.Presencas.Remove(presencaBuscada);
            _context.SaveChanges();
        }
    }

    public void Inscrever(Presenca Inscricao)
    {
        _context.Presencas.Add(Inscricao);
        _context.SaveChanges();
    }

    public List<Presenca> Listar()
    {
        return _context.Presencas
           .Include(p => p.IdEventoNavigation)
           .ThenInclude(e => e!.IdInstituicaoNavigation)
           .OrderBy(p => p.IdPresenca)
           .ToList();
    }

    /// <summary>
    /// lista as presenças de um usuário específico
    /// </summary>
    /// <param name="IdUsuario">id do usuario para filtragem</param>
    /// <returns>uma lista de presencas de um usuario especifico</returns>

    public List<Presenca> ListarMinhas(Guid IdUsuario)
    {
        return _context.Presencas.Include(p => p.IdEventoNavigation).ThenInclude(e => e!.IdInstituicaoNavigation).Where(p => p.IdUsuario == IdUsuario).ToList();
    }
}
