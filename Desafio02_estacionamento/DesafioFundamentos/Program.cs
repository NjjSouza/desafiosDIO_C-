using DesafioFundamentos.Models;

// Coloca o encoding para UTF8 para exibir acentuação
Console.OutputEncoding = System.Text.Encoding.UTF8;

decimal precoInicial = 0;
decimal precoPorHora = 0;

// CORREÇÃO: antes era Convert.ToDecimal(Console.ReadLine()), que derruba o programa com uma
// exceção se o usuário digitar texto ou deixar em branco. Agora LerValor() repete a pergunta
// até receber um número válido (a função está no fim deste arquivo).
Console.WriteLine("Seja bem vindo ao sistema de estacionamento!");
precoInicial = LerValor("Digite o preço inicial:");

precoPorHora = LerValor("Agora digite o preço por hora:");

// Instancia a classe Estacionamento, já com os valores obtidos anteriormente
Estacionamento es = new Estacionamento(precoInicial, precoPorHora);

string opcao = string.Empty;
bool exibirMenu = true;

// Realiza o loop do menu
while (exibirMenu)
{
    Console.Clear();
    Console.WriteLine("Digite a sua opção:");
    Console.WriteLine("1 - Cadastrar veículo");
    Console.WriteLine("2 - Remover veículo");
    Console.WriteLine("3 - Listar veículos");
    Console.WriteLine("4 - Encerrar");

    switch (Console.ReadLine())
    {
        case "1":
            es.AdicionarVeiculo();
            break;

        case "2":
            es.RemoverVeiculo();
            break;

        case "3":
            es.ListarVeiculos();
            break;

        case "4":
            exibirMenu = false;
            break;

        default:
            Console.WriteLine("Opção inválida");
            break;
    }

    Console.WriteLine("Pressione uma tecla para continuar");
    Console.ReadLine();
}

Console.WriteLine("O programa se encerrou");

// MELHORIA: função nova. Pede um valor monetário e só aceita número não negativo.
// decimal.TryParse usa a cultura do computador, então no Brasil a vírgula (2,50) é aceita.
static decimal LerValor(string mensagem)
{
    while (true)
    {
        Console.WriteLine(mensagem);

        if (decimal.TryParse(Console.ReadLine(), out decimal valor) && valor >= 0)
            return valor;

        Console.WriteLine("Valor inválido. Digite um número maior ou igual a zero (ex.: 2,50).");
    }
}
