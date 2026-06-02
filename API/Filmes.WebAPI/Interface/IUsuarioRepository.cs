using FIlmes.WebAPI.Models;

namespace FIlmes.WebAPI.Interface;

public interface IUsuarioRepository
{
    void Cadastrar(Usuario novoUsuario);
    Usuario BuscarPorId(Guid id);

    Usuario BuscarPorEmailESenha(string email, string senha);

}
