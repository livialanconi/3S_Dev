using FIlmes.WebAPI.Controllers;
using FIlmes.WebAPI.Models;

namespace FILmes.WebAPI.Interface;

public interface IFilmeRepository
{
    void Cadastrar(Filme novoFilme);
    void AtualizarIdCorpo(Filme filmeAtualizado);
    void AtualizarIdUrl(Guid id, Filme filmeAtualizado);
    List<Filme> Listar();
    void Deletar(Guid id);

    Filme BuscarPorId(Guid id);
    void Cadastrar(FilmeController novoFilme);
}
