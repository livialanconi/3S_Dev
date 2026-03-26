using ConnectPlus.BdContextConnect;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories;

public class TipoContatoRepository : ITipoContatoRepository
{
    private readonly ConnectContext _context;

    public TipoContatoRepository(ConnectContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Atualiza um tipo de contato já existente no banco de dados
    /// </summary>
    /// <param name="id">id do tipo de contato a ser a atualizado</param>
    /// <param name="tipoContato">tipo de contato com os dados atualizados</param>
    public void Atualizar(Guid id, TipoContato tipoContato)
    {
        var tipoContatoBuscado = _context.TipoContatos.Find(id);
        if (tipoContatoBuscado != null)
        {
            tipoContatoBuscado.Titulo = tipoContato.Titulo;
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Busca um tipo de contato pelo seu id
    /// </summary>
    /// <param name="id">id do tipo de contato buscado</param>
    /// <returns>tipo de contato buscado</returns>
    public TipoContato BuscarPorId(Guid id)
    {
        return _context.TipoContatos.Find(id)!;
    }
    /// <summary>
    /// Cadastra um novo tipo de contato
    /// </summary>
    /// <param name="tipoContato">tipo de contato a ser cadastrado</param>
    public void Cadastrar(TipoContato tipoContato)
    {
        _context.TipoContatos.Add(tipoContato);
        _context.SaveChanges();
    }
    /// <summary>
    /// Deleta um tipo de contato
    /// </summary>
    /// <param name="id">id do tipo de contato a ser deletado</param>
    public void Deletar(Guid id)
    {
        var tipoContatoBuscado = _context.TipoContatos.Find(id);
        if (tipoContatoBuscado != null)
        {
            _context.TipoContatos.Remove(tipoContatoBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Busca uma lista de tipos de contatos cadastrados
    /// </summary>
    /// <returns>Uma lista de tipos de contato</returns>
    public List<TipoContato> Listar()
    {
        return _context.TipoContatos.OrderBy(tipoContato => tipoContato.Titulo).ToList();
    }
}
