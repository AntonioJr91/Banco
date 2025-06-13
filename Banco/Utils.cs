using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    internal class Utils
    {
        public static void VoltarAoMenu()
        {
            Console.Write("\nPressione qualquer botão para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }
        public static void Cabecalho(string titulo, int largura = 35)
        {
            Console.WriteLine(new string('=', largura));
            Console.WriteLine(titulo.PadLeft((largura + titulo.Length) / 2).PadRight(largura));
            Console.WriteLine(new string('=', largura));
            Console.WriteLine();
        }

        public static void ExibirCabecalhoContas()
        {
            Console.WriteLine("{0,-20} | {1,-15} | {2,-12} | {3,-12}", "Nome do titular", "Número da conta", "Tipo de conta", "Saldo");
        }
        public static void ExibirLinhaConta(Conta conta)
        {
            Console.WriteLine("{0,-20} | {1,-15} | {2,-12} | {3,-12}",
             conta.Nome,
             conta.NumeroDaConta,
             conta.TipoDeConta,
             conta.Saldo.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"))
             );
        }
        public static Conta ValidaNumeroDaConta(List<Conta> contas, int numeroDaConta)
        {
            Conta? contaExiste = contas.Find(conta => conta.NumeroDaConta == numeroDaConta);

            if (contaExiste == null)
            {
                Console.WriteLine("\nConta não encontrada! Verifique o número e tente novamente.");
            }
            return contaExiste;
        }
        public static void Transacao(List<Conta> contas, TipoDeTransacao operacao)
        {
            Console.Write("Digite o número da conta: ");
            int numeroDaConta = Convert.ToInt32(Console.ReadLine());

            Conta conta = Utils.ValidaNumeroDaConta(contas, numeroDaConta);

            if (conta != null)
            {
                Console.Write("\nInforme o valor: ");
                string valorInput = Console.ReadLine();
                valorInput = valorInput.Replace('.', ',');
                decimal valor = Convert.ToDecimal(valorInput, new CultureInfo("pt-BR"));


                if (operacao == TipoDeTransacao.Depositar)
                {
                    conta.Deposito(valor);
                    Console.WriteLine($"\nDepósito no valor de {valor.ToString("C")} efetudo com sucesso!");
                }
                else if (operacao == TipoDeTransacao.Sacar)
                {
                    if (conta.Saldo >= valor)
                    {
                        conta.Saque(valor);
                        Console.WriteLine($"\nSaque no valor de {valor.ToString("C")} efetuado com sucesso!");
                    }
                    else
                    {
                        Console.WriteLine("\nSaldo insuficiente para realizar o saque.");
                    }
                }
            }
            else
            {
                Console.WriteLine("Houve algum problema na transação.");
                return;
            }
        }
    }
}
