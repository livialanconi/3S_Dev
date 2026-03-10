using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;

namespace EventPlus_.WebAPI.Repositories;

public class InstituicaoRepository : IInstituicaoRepository
{
    private readonly EventContext _context;

    //injeção de dependência: Recebe o contexto pelo contrutor

    public InstituicaoRepository(EventContext context)
    { _context = context; }

    /// <summary>
    /// Atualiza um tipo de instituição usando o rastreamento automático
    /// </summary>
    /// <param name="id">id do tipo instituição a ser atualizado</param>
    /// <param name="instituicao">Novos dados do tipo instituição</param>
    public void Atualizar(Guid id, Instituicao instituicao)
    {
        var instituicaoBuscado = _context.Instituicaos.Find(id);
        if (instituicaoBuscado != null)
        {
            instituicaoBuscado.Endereco = instituicao.Endereco;
            instituicaoBuscado.Cnpj = instituicao.Cnpj;
            instituicao.NomeFantasia = instituicao.NomeFantasia;
        }
        _context.SaveChanges();
    }

    /// <summary>
    /// Busca um Tipo de Instituição por Id
    /// </summary>
    /// <param name="id">id do tipo instituição a ser buscado</param>
    /// <returns>Objeto Instituicao com as informações do tipo instituicao buscado</returns>
    public Instituicao BuscarPorId(Guid id)
    {
        return _context.Instituicaos.Find(id)!;
    }

    /// <summary>
    /// Cadastra um novo tipo de Instituição
    /// </summary>
    /// <param name="instuicao">Tipo de instituição a ser cadastrado</param>
    public void Cadastrar(Instituicao instuicao)
    {
        _context.Instituicaos.Add(instuicao);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        var instituicaoBuscado = _context.Instituicaos.Find(id);
        if (instituicaoBuscado != null)
        {
            _context.Instituicaos.Remove(instituicaoBuscado);
            _context.SaveChanges();
        }
    }

    /// <summary>
    /// Busca a lista de tipo de instuições cadastrados
    /// </summary>
    /// <returns>Uma lista de tipo Instituições</returns>
    public List<Instituicao> Listar()
    {
        return _context.Instituicaos.OrderBy(Instituicao => Instituicao).ToList();
    }
}