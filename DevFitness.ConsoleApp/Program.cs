using System;
using System.Collections.Generic;
using System.Linq;

namespace DevFitness.ConsoleApp
{
    class Program
    {
        static void Main(string[] arg)
        {
            Console.Write("Digite seu nome: ");
            var nome = Console.ReadLine();

            Console.Write("Digite sua altura: ");
            var altura = Console.ReadLine();

            Console.Write("Digite seu peso?: ");
            var peso = Console.ReadLine();

            var listaRefeicoes = new List<Refeicao>();

            var opcao = "-1";

            while (opcao != "0")
            {
                ExibirOpcoes();

                opcao = Console.ReadLine();

                switch (opcao)
                {
                    case "1":
                        Console.WriteLine($"Nome: {nome}, Altura: {altura}, Peso: {peso}");
                        break;
                    case "2":
                        Cadastrar(listaRefeicoes);
                        break;
                    case "3":
                        ListaRefeicoes(listaRefeicoes);
                        break;
                    default:
                        Console.WriteLine("Por favor, digite outra opção.");
                        break;
                }
            }

            Console.WriteLine("Fechando o app.");

            Console.Read();
        }

        public static void ExibirOpcoes()
        {
            Console.WriteLine("#---------------------------------------#");
            Console.WriteLine("#--- Seja bem-vindo(a) ao DevFitness ---#"); 
            Console.WriteLine("1- Exibir detalhes de usuário.");
            Console.WriteLine("2- Cadastrar nova refeição.");
            Console.WriteLine("3- Listar todas refeições");
            Console.WriteLine("");
            Console.WriteLine("0- Finalizar aplicação.");
            Console.WriteLine("#---------------------------------------#");
        }

        public static void Cadastrar(List<Refeicao> refeicoes)
        {
            Console.WriteLine("Digite a descricao da refeição:");
            var descricao = Console.ReadLine();

            Console.WriteLine("Digite a quantidade de calorias:");
            var calorias = Console.ReadLine();

            if (int.TryParse(calorias, out int caloriasInt))
            {
                var refeicao = new Refeicao(descricao, caloriasInt);
                refeicoes.Add(refeicao);

                Console.WriteLine("Refeição adicionada com sucesso.");
            }
            else
            {
                Console.WriteLine("Valor de calorias inválida.");
            }
        }

        public static void ListaRefeicoes(List<Refeicao> refeicoes)
        {
            foreach (var refeicao in refeicoes)
            {
                refeicao.ImprimirMensagem();
            }
        }
    }
}