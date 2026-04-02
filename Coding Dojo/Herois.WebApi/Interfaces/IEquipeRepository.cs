using Herois.WebApi.Models;

namespace Herois.WebApi.Interfaces;

public interface IEquipeRepository
{
    List<Equipe> Listar();
    Equipe? BuscarPorId(int id);
    void CadastrarEquipe(Equipe equipe);
    void AtualizarEquipe(int id, Equipe equipe);
    void DeletarEquipe(int id);
}