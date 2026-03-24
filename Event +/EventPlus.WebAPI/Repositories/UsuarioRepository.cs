using EventPlus.WebAPI.BdContextEvent;
using EventPlus.WebAPI.Interfaces;
using EventPlus.WebAPI.Models;
using EventPlus.WebAPI.Utils;
using Microsoft.EntityFrameworkCore;

namespace EventPlus.WebAPI.Repositories;

public class UsuarioRepository : IUsuarioRepository
{
    private readonly EventContext _context;

    //metodo construtor que aplica a injecao de dependencia
    public UsuarioRepository(EventContext context)
    {
        _context = context;
    }

    public void Atualizar(Guid id, Usuario usuario)
    {
        throw new NotImplementedException();
    }

    /// <summary>
    /// Busca o usuario pelo email e valida o hash da senha
    /// </summary>
    /// <param name="Email">email do usuario a ser buscado</param>
    /// <param name="Senha">senha para validar o usuario</param>
    /// <returns>Usuario buscado</returns>

    public Usuario BuscarPorEmailESenha(string Email, string Senha)
    {
        //Primeiro, buscamos o usuario pelo email
        var usuarioBuscado = _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.Email == Email);

        //Verificamos se o usuario foi encontrado
        if (usuarioBuscado != null)
        {
            //Comparamos o hash da senha digitada com o que esta no banco
            bool confere = Criptografia.CompararHash(Senha, usuarioBuscado.Senha);

            if (confere)
            {
                return usuarioBuscado;
            }
        }

        return null!;

    }

    /// <summary>
    /// Busca um usuario por id, incluindo os dados do seu tipo de usuario
    /// </summary>
    /// <param name="id">id do usuario a ser buscado</param>
    /// <returns>Usuario Buscado</returns>

    public Usuario BuscarPorId(Guid id)
    {
        return _context.Usuarios.Include(usuario => usuario.IdTipoUsuarioNavigation).FirstOrDefault(usuario => usuario.IdUsuario == id)!;
    }

    /// <summary>
    /// Cadastra um novo usuario. A senha é criptografada e o id gerado pelo banco
    /// </summary>
    /// <param name="usuario">Usuario a ser cadastrado</param>

    public void Cadastrar(Usuario usuario)
    {
        usuario.Senha = Criptografia.GerarHash(usuario.Senha);

        _context.Usuarios.Add(usuario);
        _context.SaveChanges();
    }

    public void Deletar(Guid id)
    {
        throw new NotImplementedException();
    }

    public List<Usuario> Listar()
    {
        throw new NotImplementedException();
    }
}
