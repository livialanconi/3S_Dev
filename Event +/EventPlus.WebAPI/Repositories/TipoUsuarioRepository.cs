using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus_.WebAPI.Repositories;

public class TipoUsuarioRepository : ITipoUsuarioRepository
{
    private readonly EventContext _context;

    public TipoUsuarioRepository(EventContext context)
    { _context = context; }


    /// <summary>
    /// Busca a lista de tipo de usuários cadastrados
    /// </summary>
    /// <returns></returns>
    public List<TipoUsuario> Listar()
    {
        return _context.TipoUsuarios.OrderBy(tipoUsuario => tipoUsuario).ToList();
    }

    /// <summary>
    /// Cadastrar um novo tipo de usuário
    /// </summary>
    /// <param name="tipoUsuario">Tipo de usuário a ser cadastrado</param>
    public void Cadastrar(TipoUsuario tipoUsuario)
    {
        _context.TipoUsuarios.Add(tipoUsuario);
        _context.SaveChanges();
    }

    /// <summary>
    /// Atualiza um tipo de usuário usando o rastreamento automático
    /// </summary>
    /// <param name="id">id do tipo usuário a ser atualizado</param>
    /// <param name="tipoUsuario">Novos dados do tipo usuário</param>
    public void Atualizar(Guid id, TipoUsuario tipoUsuario)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);
        if (tipoUsuarioBuscado != null)
        {
            tipoUsuarioBuscado.Titulo = tipoUsuario.Titulo;
        }

        _context.SaveChanges();
    }


    /// <summary>
    /// Deleta um tipo de usuário
    /// </summary>
    /// <param name="id">id do tipo usuario a ser deletado</param>
    public void Deletar(Guid id)
    {
        var tipoUsuarioBuscado = _context.TipoUsuarios.Find(id);
        if (tipoUsuarioBuscado != null)
        {
            _context.TipoUsuarios.Remove(tipoUsuarioBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca usuário pelo id
    /// </summary>
    /// <param name="id">id do tipo de usuário a ser buscado</param>
    /// <returns>Objeto do tipo usuário com as infomrmações</returns>
    public TipoUsuario BuscarPorId(Guid id)
    {
        return _context.TipoUsuarios.Find(id);
    }

}