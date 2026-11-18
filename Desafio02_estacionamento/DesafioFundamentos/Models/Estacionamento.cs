namespace DesafioFundamentos.Models
{
    public class Estacionamento
    {
        private decimal precoInicial = 0;
        private decimal precoPorHora = 0;
        private List<string> veiculos = new List<string>();

        public Estacionamento(decimal precoInicial, decimal precoPorHora)
        {
            this.precoInicial = precoInicial;
            this.precoPorHora = precoPorHora;
        }

        // MELHORIA: método novo. Padroniza a placa (sem espaços nas pontas e em maiúsculas).
        // Assim "abc-1234", " ABC-1234 " e "Abc-1234" passam a ser a MESMA placa, e as comparações
        // ficam simples em todo o resto da classe. Também protege contra Console.ReadLine() retornar null.
        private static string NormalizarPlaca(string placa)
        {
            return (placa ?? string.Empty).Trim().ToUpperInvariant();
        }

        public void AdicionarVeiculo()
        {
            // Pedir para o usuário digitar uma placa (ReadLine) e adicionar na lista "veiculos"
            Console.WriteLine("Digite a placa do veículo para estacionar:");
            string placa = NormalizarPlaca(Console.ReadLine());

            // MELHORIA: antes, uma placa vazia era aceita e entrava na lista.
            if (string.IsNullOrEmpty(placa))
            {
                Console.WriteLine("Placa inválida. Digite uma placa para cadastrar o veículo.");
                return;
            }

            // MELHORIA: antes, a mesma placa podia ser cadastrada várias vezes.
            // Como a placa já está normalizada, Contains() basta para detectar duplicatas.
            if (veiculos.Contains(placa))
            {
                Console.WriteLine($"O veículo {placa} já está estacionado.");
                return;
            }

            veiculos.Add(placa);
            Console.WriteLine($"O veículo {placa} foi cadastrado.");
        }

        public void RemoverVeiculo()
        {
            // Pedir para o usuário digitar a placa e armazenar na variável placa
            Console.WriteLine("Digite a placa do veículo para remover:");

            // CORREÇÃO DO BUG: antes, a verificação usava x.ToUpper() == placa.ToUpper() (ignorava
            // maiúsculas/minúsculas), mas depois veiculos.Remove(placa) comparava o texto exato.
            // Resultado: ao digitar "ABC-1234" para um veículo cadastrado como "abc-1234", o programa
            // cobrava e dizia "removido", mas o veículo continuava na lista.
            // Agora a placa é normalizada na entrada e na remoção, então os dois passos concordam.
            string placa = NormalizarPlaca(Console.ReadLine());

            // Verifica se o veículo existe
            if (veiculos.Contains(placa))
            {
                // Pedir para o usuário digitar a quantidade de horas que o veículo permaneceu estacionado,
                // Realizar o seguinte cálculo: "precoInicial + precoPorHora * horas" para a variável valorTotal
                Console.WriteLine("Digite a quantidade de horas que o veículo permaneceu estacionado:");

                // CORREÇÃO: antes era int.Parse(Console.ReadLine()), que derruba o programa com uma
                // exceção (FormatException) se o usuário digitar texto, deixar vazio ou usar vírgula.
                // TryParse devolve false em vez de lançar exceção. Também rejeitamos horas negativas.
                // Como validamos ANTES de remover, o veículo não se perde em caso de entrada inválida.
                if (!int.TryParse(Console.ReadLine(), out int horas) || horas < 0)
                {
                    Console.WriteLine("Quantidade de horas inválida. O veículo não foi removido, tente novamente.");
                    return;
                }

                decimal valorTotal = precoInicial + precoPorHora * horas;

                // Remover a placa digitada da lista de veículos
                veiculos.Remove(placa);

                // MELHORIA: F2 exibe sempre duas casas decimais (ex.: R$ 11,00 em vez de R$ 11).
                Console.WriteLine($"O veículo {placa} foi removido e o preço total foi de: R$ {valorTotal:F2}");
            }
            else
            {
                Console.WriteLine("Desculpe, esse veículo não está estacionado aqui. Confira se digitou a placa corretamente");
            }
        }

        public void ListarVeiculos()
        {
            // Verifica se há veículos no estacionamento
            if (veiculos.Any())
            {
                Console.WriteLine("Os veículos estacionados são:");
                // Realizar um laço de repetição, exibindo os veículos estacionados
                for (int i = 0; i < veiculos.Count; i++)
                {
                    Console.WriteLine(veiculos[i]);
                }

            }
            else
            {
                Console.WriteLine("Não há veículos estacionados.");
            }
        }
    }
}
