using System.Globalization;
using System.Xml.Schema;

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
            Console.WriteLine("{0,-20} | {1,-15} | {2,-15} | {3,-11}", "Nome do titular", "Número da conta", "Tipo de conta", "Saldo");
        }
        public static void ExibirLinhaConta(Conta conta)
        {
            Console.WriteLine("{0,-20} | {1,-15} | {2,-15} | {3,-11}",
             conta.Nome,
             conta.NumeroDaConta,
             conta.TipoDeConta,
             conta.Saldo.ToString("C", System.Globalization.CultureInfo.GetCultureInfo("pt-BR"))
             );
        }
        public static decimal ConverteParaDecimalPtBr(string? valorInput)
        {
            valorInput = valorInput?.Replace('.', ',');
            decimal valor = Convert.ToDecimal(valorInput, new CultureInfo("pt-BR"));
            return valor;
        }
        public static bool ValorEhValido(decimal valor)
        {
            if (valor > 0)
            {
                return true;
            } else
            {
                Console.WriteLine("\nValor inválido!");
                return false;
            }
        }
    }
}
