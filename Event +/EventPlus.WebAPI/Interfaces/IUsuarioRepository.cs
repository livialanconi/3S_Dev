using EventPlus.WebAPI.Models;

namespace EventPlus.WebAPI.Interfaces;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario usuasrio);
    Usuario BuscarPorId(Guid id);
    Usuario BuscarPorEmailESenha(string email, string senha);
    List<Usuario> Listar();
     void Atualizar(Guid id, Usuario usuario);
     void Deletar(Guid id);
}
