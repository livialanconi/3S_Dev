using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace EventPlus_.WebAPI.Repositories;

public class EventoRepository : IEventoRepository
{
    private readonly EventContext _context;

    public EventoRepository(EventContext context)
    {
        _context = context;
    }


    /// <summary>
    /// Atualiza um tipo de evento usando o rastreamento automático
    /// </summary>
    /// <param name="id">id do tipo evento a ser atualizado</param>
    /// <param name="evento">Novos dados do evento</param>
    public void Atualizar(Guid id, Evento evento)
    {
        var eventoBuscado = _context.Eventos.Find(id);

        if (eventoBuscado != null)
        {
            eventoBuscado.Nome = evento.Nome;

            eventoBuscado.Descricao = evento.Descricao;

            eventoBuscado.DataEvento = evento.DataEvento;

            eventoBuscado.IdTipoEvento = evento.IdTipoEvento;

            eventoBuscado.IdInstituicao = evento.IdInstituicao;
        }

        // o SaveChages() detecta a mudança na propriedade automaticamente

        _context.SaveChanges();
    }


    /// <summary>
    /// Busca um Tipo de Evento por Id
    /// </summary>
    /// <param name="id">id do tipo evento a ser buscado</param>
    /// <returns>Objeto tipoEvento com as informações do tipo evento buscado</returns>
    public Evento BuscarPorId(Guid id)
    {
        return _context.Eventos.Find(id)!;
    }


    /// <summary>
    /// Cadastra um Evento
    /// </summary>
    /// <param name="evento">Evento a ser cadastrado</param>
    public void Cadastrar(Evento evento)
    {
        _context.Eventos.Add(evento);
        _context.SaveChanges();
    }

    public object? CadastrarEventos()
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Deleta um tipo de evento
    /// </summary>
    /// <param name="id">Id do tipo evento deletado</param>
    public void Deletar(Guid id)
    {
        var eventoBuscado = _context.Eventos.Find(id);
        if (eventoBuscado != null)
        {
            _context.Eventos.Remove(eventoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca a lista de eventos cadastrados
    /// </summary>
    /// <returns>UUma lista de tipo eventos </returns>
    public List<Evento> Listar()
    {
        return _context.Eventos.OrderBy(evento => evento).ToList();

    }


    /// <summary>
    /// Método que busca eventos no qual o usuário confirmou presença
    /// </summary>
    /// <param name="IdUsuario">Id do usuário a ser buscado</param>
    /// <returns>Uma lista de eventos</returns>
    public List<Evento> ListarPorId(Guid IdUsuario)
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.Presencas.Any(p => p.IdUsuario == IdUsuario && p.Situacao == true))
            .ToList();
    }


    /// <summary>
    /// método que traz a lista de próximos eventos
    /// </summary>
    /// <returns>Uma lista de eventos</returns>
    public List<Evento> ProximoEventos()
    {
        return _context.Eventos
            .Include(e => e.IdTipoEventoNavigation)
            .Include(e => e.IdInstituicaoNavigation)
            .Where(e => e.DataEvento >= DateTime.Now)
            .OrderBy(e => e.DataEvento)
            .ToList();
    }

    public object? ProximosEventos()
    {
        throw new NotImplementedException();
    }

    object? IEventoRepository.ListarPorId(Guid idUsuario)
    {
        return ListarPorId(idUsuario);
    }

    List<Evento> IEventoRepository.ProximosEventos()
    {
        throw new NotImplementedException();
    }
}

