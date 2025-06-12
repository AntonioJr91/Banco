using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    public class Utils
    {
        public static void Cabecalho(string titulo, int largura = 35)
        {
            Console.WriteLine(new string('=', largura));
            Console.WriteLine(titulo.PadLeft((largura + titulo.Length) / 2).PadRight(largura));
            Console.WriteLine(new string('=', largura));
            Console.WriteLine();
        }
        public static void VoltarAoMenu()
        {
            Console.Write("\nPressione qualquer botão para voltar ao menu...");
            Console.ReadKey();
            Console.Clear();
        }
    }
}
