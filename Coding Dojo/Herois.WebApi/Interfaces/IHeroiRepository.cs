using Herois.WebApi.Models;

public interface IHeroiRepository
{
    void CadastrarHeroi(Heroi heroi);
    void AtualizarHeroi(Heroi heroi);
    void DeletarHeroi(int id);

}