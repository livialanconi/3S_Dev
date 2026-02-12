namespace Exercicio8;
    public class Usuario : IAutenticavel
    {
        private string Senha = "1234";

        public bool Autenticar(string senha)
        {
            return senha == Senha;
        }
    }

