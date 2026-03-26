using ConnectPlus.BdContextConnect;
using ConnectPlus.Interfaces;
using ConnectPlus.Models;

namespace ConnectPlus.Repositories;

public class ContatoRepository : IContatoRepositorty
{
    private readonly ConnectContext _context;
    public ContatoRepository(ConnectContext context)
    {
        _context = context;
    }
    /// <summary>
    /// Atualiza m contato existente pelo seu id
    /// </summary>
    /// <param name="id">Id do contato a ser atualizado</param>
    /// <param name="contato">Contato com os dados atualizados</param>
    public void Atualizar(Guid id, Contato contato)
    {
        var contatoBuscado = _context.Contatos.Find(id);
        if (contatoBuscado != null)
        {
            contatoBuscado.Nome = String.IsNullOrWhiteSpace(contato.Nome) ? contatoBuscado.Nome : contato.Nome;
            contatoBuscado.FormaContato = String.IsNullOrWhiteSpace(contato.FormaContato) ?
            contatoBuscado.FormaContato : contato.FormaContato;           
            contatoBuscado.Imagem = String.IsNullOrWhiteSpace(contato.Imagem) ? contatoBuscado.Imagem : contato.Imagem;
            _context.Contatos.Update(contatoBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Busca um contato existente pelo seu id
    /// </summary>
    /// <param name="id">Id do contato buscado</param>
    /// <returns>Contato buscado</returns>
    public Contato BuscarPorId(Guid id)
    {
        return _context.Contatos.Find(id)!;
    }
    /// <summary>
    /// Cadastra um novo contato
    /// </summary>
    /// <param name="novoContato">Novo contato cadastrado</param>
    public void Cadastrar(Contato novoContato)
    {
        _context.Contatos.Add(novoContato);
        _context.SaveChanges();
    }
    /// <summary>
    /// Deleta um contato existente pelo seu id
    /// </summary>
    /// <param name="id">Id do contato a ser deletado</param>
    public void Deletar(Guid id)
    {
        var contatoBuscado = _context.Contatos.Find(id);
        if (contatoBuscado != null)
        {
            _context.Contatos.Remove(contatoBuscado);
            _context.SaveChanges();
        }
    }
    /// <summary>
    /// Uma lista de todos os contatos cadastrados
    /// </summary>
    /// <returns>Lista de todos os contatos cadastrados</returns>
    public List<Contato> Listar()
    {
        return _context.Contatos.ToList();
    }
}