string titulo = " MENU PRINCIPAL ";
bool continuar = true;

while (continuar)
{
    int largura = 35;
    Console.WriteLine(new string('=', largura));
    Console.WriteLine(titulo.PadLeft((largura + titulo.Length) / 2).PadRight(largura));
    Console.WriteLine(new string('=', largura));
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
        default:
            Console.WriteLine("\nOpção inválida!");
            VoltarAoMenu();
            break;
    }
}
static void VoltarAoMenu()
{
    Console.Write("\nPressione qualquer botão para voltar ao menu...");
    Console.ReadKey();
    Console.Clear();
}