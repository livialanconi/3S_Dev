namespace Exercicio5;
    public class Funcionario : Pessoa
    {
        public double Salario;

        public Funcionario(string nome, int idade, double salario) : base(nome, idade)
    
        {
            Salario = salario;
        }

        public override void ExibirDados()
        {
            base.ExibirDados();
            Console.WriteLine($"Salário: {Salario}");
        }
    }