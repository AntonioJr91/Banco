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

            if (contas.Count() > 0)
            {
                Utils.ExibirCabecalhoContas();
                foreach (Conta conta in contas)
                {
                    Utils.ExibirLinhaConta(conta);
                }
            } else
            {
                Console.WriteLine("Não há contas cadastradas");
            }
        }
        internal void BuscarContaPorNumero()
        {
            Console.Clear();
            Utils.Cabecalho(" Buscar conta pelo número ");

            Console.Write("Digite o número da conta que deseja buscar: ");
            int numeroDaConta = Convert.ToInt32(Console.ReadLine());

            Conta? contaExiste = ObterContaPorNumero(contas, numeroDaConta);

            if (contaExiste != null)
            {
                Utils.ExibirCabecalhoContas();
                Utils.ExibirLinhaConta(contaExiste);
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
                string? opcao = Console.ReadLine();

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
        internal void Transferencia()
        {
            Console.Clear();
            Utils.Cabecalho(" Transferência ");

            Console.Write("Digite a conta de origem: ");
            int contaOrigemInput = Convert.ToInt32(Console.ReadLine());

            Conta? contaOrigem = ObterContaPorNumero(contas, contaOrigemInput);

            if (contaOrigem != null)
            {
                Console.Write("Informe o valor da transferência: ");
                string? valorInput = Console.ReadLine();
                decimal valor = Utils.ConverteParaDecimalPtBr(valorInput);

                if (!Utils.ValorEhValido(valor)) return;

                if (!TemSaldoSuficiente(contaOrigem, valor))
                {
                    Console.WriteLine("Saldo insuficiente para este tipo de transação.");
                    return;
                }

                Console.Write("Informe a conta de destino: ");
                int contaDestinoInput = Convert.ToInt32(Console.ReadLine());

                Conta? contaDestino = ObterContaPorNumero(contas, contaDestinoInput);

                if (contaDestino != null)
                {
                    Console.WriteLine("\nVerifique os dados da transferência");
                    Console.WriteLine($"\nNome do titular: {contaDestino.Nome}" +
                        $"\nNúmero da conta: {contaDestino.NumeroDaConta}" +
                        $"\nValor: {valor.ToString("C")}");

                    Console.Write("Confirmar operação? (Sim / Não): ");
                    string? confirmacao = Console.ReadLine()?.ToLower();

                    if (confirmacao == "sim")
                    {
                        contaOrigem.Saque(valor);
                        contaDestino.Deposito(valor);
                        Console.WriteLine("\nTransferência efetuada com sucesso!");
                    }
                }
            }
        }
        private static Conta? ObterContaPorNumero(List<Conta> contas, int numeroDaConta)
        {
            Conta? conta = contas.Find(conta => conta.NumeroDaConta == numeroDaConta);

            if (conta != null)
            {
                return conta;
            }
            else
            {
                Console.WriteLine("\nConta inválida! Confira seus dados e tente novamente.");
                return null;
            }
        }
        private static TipoDeConta ConversaoDeTipo(int tipoDeConta)
        {
            return (TipoDeConta)tipoDeConta;
        }
        private static string ObterNomeValido(string? nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
            {
                Console.WriteLine("Nome do titular não pode ser vazio.");
                return ObterNomeValido(Console.ReadLine());
            }
            return nome;
        }
        private static bool TipoDeContaEhValido(int tipoDeConta)
        {
            return Enum.IsDefined(typeof(TipoDeConta), tipoDeConta);
        }
        private static void Transacao(List<Conta> contas, TipoDeTransacao operacao)
        {
            Console.Write("Digite o número da conta: ");
            int numeroDaConta = Convert.ToInt32(Console.ReadLine());

            Conta? conta = ObterContaPorNumero(contas, numeroDaConta);

            if (conta != null)
            {
                Console.Write("\nInforme o valor: ");
                string? valorInput = Console.ReadLine();
                decimal valor = Utils.ConverteParaDecimalPtBr(valorInput);

                if (Utils.ValorEhValido(valor))
                {
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
            }
            else
            {
                Console.WriteLine("Houve algum problema na transação.");
                return;
            }
        }
        private static bool TemSaldoSuficiente(Conta conta, decimal valor)
        {
            return conta.Saldo >= valor;
        }
        private void RealizarTransacao(TipoDeTransacao operacao)
        {
            Console.Clear();
            Utils.Cabecalho(operacao == TipoDeTransacao.Depositar ? "Transação de Depósito" : "Transação de Saque");
            Transacao(contas, operacao);
        }
    }
}