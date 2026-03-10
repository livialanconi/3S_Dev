namespace EventPlus.WebAPI.Interfaces;

public interface IInstituicaoRepository
{
    List<Models.Instituicao> Listar();
    void Cadastrar(Models.Instituicao instituicao);
     void Atualizar(Guid id, Models.Instituicao instituicao);
     void Deletar(Guid id);
    Models.Instituicao BuscarPorId(Guid id);
}
