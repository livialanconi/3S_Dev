using ConnectPlus.Models;

namespace ConnectPlus.Interfaces;

public interface IContatoRepositorty
{
    List<Contato> Listar();
    void Cadastrar(Contato novoContato);
    void Atualizar(Guid id, Contato contato);
    void Deletar(Guid id);
    Contato BuscarPorId(Guid id);

}