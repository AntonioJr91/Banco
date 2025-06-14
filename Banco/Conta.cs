namespace Banco
{
    internal class Conta
    {
        public int NumeroDaConta { get; }

        private static readonly Random rmd = new();

        private static readonly HashSet<int> contasGeradas = new();
        public string Nome { get; }

        public TipoDeConta TipoDeConta { get; }

        private readonly decimal saldoInicial;
        public decimal Saldo { get; private set; }
        internal Conta(string nome, TipoDeConta tipo)
        {

            if (string.IsNullOrWhiteSpace(nome))
            {
                throw new ArgumentException("Nome não pode ser vazio.", nameof(nome));
            }

            NumeroDaConta = GerarContaAleatoria();
            Nome = nome;
            TipoDeConta = tipo;
            saldoInicial = DefinirSaldoInicialDaConta(tipo);
            Saldo = saldoInicial;

        }
        private static decimal DefinirSaldoInicialDaConta(TipoDeConta tipo)
        {
            if (tipo == TipoDeConta.Poupança)
            {
                return 100m;
            }
            else if (tipo == TipoDeConta.Corrente)
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
        public void Deposito(decimal valor)
        {
            if (valor > 0)
            {
                Saldo += valor;
            }else
            {
                Console.WriteLine("\nValor incorreto. Tente novamente.");
                return;
            }
        }
        public void Saque(decimal valor)
        {
            if (Saldo >= valor)
            {
                Saldo -= valor;
            }else
            {
                Console.WriteLine("\nVocê não tem saldo suficiente para realizar este saque.");
                return;
            }
        }
    }
}

