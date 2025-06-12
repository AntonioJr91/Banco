using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    internal class GerenciadorDeContas
    {
        List<Conta> contas = new();

        internal void CriarConta()
        {
            Console.Clear();
            Utils.Cabecalho(" Criação de conta ");

            Console.Write("Informe o nome do titular da conta: ");
            string? nomeInput = Console.ReadLine();

            string nome = ObterNomeValido(nomeInput);

            Console.Write("Informe o tipo de conta (0 = Poupança, 1 = Corrente): ");
            int tipoDeContaInt = Convert.ToInt32(Console.ReadLine());

            if (TipoDeContaEhValido(tipoDeContaInt))
            {
                TipoDeConta tipoDeConta = ConversaoDeTipo(tipoDeContaInt);
                Conta novaConta = new(nome, tipoDeConta);
                contas.Add(novaConta);
                Console.WriteLine("\nConta criada com sucesso!");

            }
            else
            {
                throw new ArgumentException("Tipo de conta inválido.");
            }
        }

        internal void ListarContas()
        {
            Console.Clear();
            Utils.Cabecalho(" Lista de contas ");

            Console.WriteLine("{0,-20} | {1,-15} | {2,-12} | {3,-12}", "Nome do titular", "Número da conta", "Tipo de conta", "Saldo");
            foreach (Conta conta in contas)
            {
                Console.WriteLine("{0,-20} | {1,-15} | {2,-13} | {3,-12}",
                    conta.Nome,
                    conta.NumeroDaConta,
                    conta.TipoDeConta,
                    conta.Saldo.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"))
                );
            }
        }
        private static string ObterNomeValido(string? nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new Exception("Nome do titular não pode ser vazio.");
            }
            return nome;
        }
        private static bool TipoDeContaEhValido(int tipoDeConta)
        {
            return Enum.IsDefined(typeof(TipoDeConta), tipoDeConta);
        }
        private static TipoDeConta ConversaoDeTipo(int tipoDeConta)
        {
            return (TipoDeConta)tipoDeConta;
        }
    }
}