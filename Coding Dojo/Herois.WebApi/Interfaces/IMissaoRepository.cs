using Herois.WebApi.Models;
using System.Collections.Generic;

namespace Herois.WebApi.Interfaces;

public interface IMissaoRepository
{
    List<Missao> Listar();
    Missao? BuscarPorId(int id);
    void CadastrarMissao(Missao missao);
    void AtualizarMissao(int id, Missao missao);
    void DeletarMissao(int id);
}