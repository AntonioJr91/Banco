using Banco;

string titulo = " MENU PRINCIPAL ";
bool continuar = true;
int largura = 35;
GerenciadorDeContas gerenciador = new();

while (continuar)
{
    Utils.Cabecalho(titulo);
    Console.WriteLine(new string('-', largura));
    Console.WriteLine($"| {"1. Criar nova conta bancária".PadRight(largura - 4)} |");
    Console.WriteLine($"| {"2. Listar contas existentes".PadRight(largura - 4)} |");
    Console.WriteLine($"| {"3. Buscar conta por número".PadRight(largura - 4)} |");
    Console.WriteLine($"| {"4. Transações (Depósito/Saque)".PadRight(largura - 4)} |");
    Console.WriteLine($"| {"5. Transferência entre contas".PadRight(largura - 4)} |");
    Console.WriteLine($"| {"0. Sair".PadRight(largura - 4)} |");
    Console.WriteLine(new string('-', largura));
    Console.Write("Escolha uma opção: ");
    string? opcao = Console.ReadLine();

    switch (opcao)
    {
        case "1":
            gerenciador.CriarConta();
            Utils.VoltarAoMenu();
            break;

        case "2":
            gerenciador.ListarContas();
            Utils.VoltarAoMenu();
            break;
        default:
            Console.WriteLine("\nOpção inválida!");
            Utils.VoltarAoMenu();
            break;
    }
}