using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Banco
{
    internal class Conta
    {
        public int NumeroDaConta { get; }
        private static readonly Random rmd = new();

        //Conjunto de números aleatorios gerados.
        private static readonly HashSet<int> contasGeradas = new();
        public string Nome { get; }

        public TipoDeConta TipoDeConta { get; }

        private readonly decimal saldoInicial;
        public decimal SaldoInicial => saldoInicial;

        public decimal Saldo { get; private set; }

        internal Conta(string nome, TipoDeConta tipoDeConta)
        {

            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("Nome não pode ser vazio.", nameof(nome));
            }

            NumeroDaConta = GerarContaAleatoria();
            Nome = nome;
            TipoDeConta = tipoDeConta;
            saldoInicial = ObterSaldoInicial(tipoDeConta);
            Saldo = saldoInicial;

        }

        //Valida qual o tipo de conta na hora da criação para atribuir o valor corretamente
        private static decimal ObterSaldoInicial(TipoDeConta tipoDeConta)
        {
            if (tipoDeConta == TipoDeConta.Poupança)
            {
                return 100m;
            }
            else if (tipoDeConta == TipoDeConta.Corrente)
            {
                return 50m;
            }
            else
            {
                return 0m;
            }
        }
        private static int GerarContaAleatoria()
        {
            int novaConta;
            const int MAXIMO = 101;
            int tentativas = 0;
            do
            {
                if (++tentativas >= MAXIMO)
                {
                    throw new InvalidOperationException("Número máximo de tentativas para gerar conta foi atingido.");
                }

                int sufixo = rmd.Next(1, MAXIMO);
                string numeroConta = $"3055{sufixo:D3}";
                novaConta = int.Parse(numeroConta);

            } while (contasGeradas.Contains(novaConta));

            contasGeradas.Add(novaConta);
            return novaConta;
        }
    }
}

