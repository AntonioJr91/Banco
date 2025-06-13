using System;
using System.Collections.Generic;
using System.Globalization;
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
                Console.WriteLine("\nTipo de conta inválido.");
            }
        }
        internal void ListarContas()
        {
            Console.Clear();
            Utils.Cabecalho(" Lista de contas ");

            Utils.ExibirCabecalhoContas();
            foreach (Conta conta in contas)
            {
                Utils.ExibirLinhaConta(conta);
            }
        }
        internal void BuscarContaPorNumero()
        {
            Console.Clear();
            Utils.Cabecalho(" Buscar conta pelo número ");

            Console.Write("Digite o número da conta que deseja buscar: ");
            int numeroDaConta = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine();

            Conta contaExiste = Utils.ValidaNumeroDaConta(contas, numeroDaConta);

            if (contaExiste != null)
            {
                Utils.ExibirCabecalhoContas();
                Utils.ExibirLinhaConta(contaExiste);
            }
            else
            {
                Utils.VoltarAoMenu();
            }
        }
        internal void Transacoes()
        {
            Console.Clear();
            Utils.Cabecalho(" Transações ");

            bool continuar = true;
            while (continuar)
            {
                Console.WriteLine("Qual transação gostaria de efetuar?\n");
                Console.WriteLine("1. Depósito");
                Console.WriteLine("2. Saque");
                Console.WriteLine("0. Sair");
                Console.Write("\nEscolha uma opção: ");
                string opcao = Console.ReadLine();

                switch (opcao)
                {

                    case "0":
                        continuar = false;
                        break;

                    case "1":
                        RealizarTransacao(TipoDeTransacao.Depositar);
                        Utils.VoltarAoMenu();
                        break;

                    case "2":
                        RealizarTransacao(TipoDeTransacao.Sacar);
                        Utils.VoltarAoMenu();
                        break;

                    default:
                        Console.WriteLine("\nOpção inválida!");
                        Utils.VoltarAoMenu();
                        break;
                }
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
        private void RealizarTransacao(TipoDeTransacao operacao)
        {
            Console.Clear();
            Utils.Cabecalho(operacao == TipoDeTransacao.Depositar ? "Transação de Depósito" : "Transação de Saque");
            Utils.Transacao(contas, operacao);
        }
    }
}