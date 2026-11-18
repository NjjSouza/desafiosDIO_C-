namespace DesafioProjetoHospedagem.Models
{
    public class Reserva
    {
        // CORREÇÃO: antes a lista começava como null. Se alguém chamasse ObterQuantidadeHospedes()
        // antes de CadastrarHospedes(), o programa quebrava com NullReferenceException.
        // Inicializar com uma lista vazia faz o método simplesmente retornar 0.
        public List<Pessoa> Hospedes { get; set; } = new List<Pessoa>();
        public Suite Suite { get; set; }
        public int DiasReservados { get; set; }

        public Reserva() { }

        public Reserva(int diasReservados)
        {
            DiasReservados = diasReservados;
        }

        public void CadastrarHospedes(List<Pessoa> hospedes)
        {
            // CORREÇÃO: antes, se a suíte ainda não tivesse sido cadastrada, a linha que lê
            // Suite.Capacidade lançava NullReferenceException (um erro difícil de entender).
            // Agora a falha é explícita e diz o que fazer. InvalidOperationException é a exceção
            // indicada quando "o objeto não está em um estado que permita esta operação".
            if (Suite == null)
                throw new InvalidOperationException("Cadastre a suíte antes de cadastrar os hóspedes.");

            // MELHORIA: também protege contra receber null ou uma lista vazia como hóspedes.
            if (hospedes == null || hospedes.Count == 0)
                throw new ArgumentException("É preciso informar ao menos um hóspede.", nameof(hospedes));

            // Verifica se a capacidade é maior ou igual ao número de hóspedes sendo recebido
            if (hospedes.Count <= Suite.Capacidade)
            {
                Hospedes = hospedes;
            }
            else
            {
                // MELHORIA: ArgumentException é mais específica que Exception e continua sendo
                // capturada por quem fizer catch (Exception). A mensagem agora traz os números.
                throw new ArgumentException(
                    $"A quantidade de hóspedes ({hospedes.Count}) excede a capacidade da suíte ({Suite.Capacidade}).",
                    nameof(hospedes));
            }
        }

        public void CadastrarSuite(Suite suite)
        {
            Suite = suite;
        }

        public int ObterQuantidadeHospedes()
        {
            // Retorna a quantidade de hóspedes (propriedade Hospedes)
            return Hospedes.Count;
        }

        public decimal CalcularValorDiaria()
        {
            // CORREÇÃO: mesmo problema da suíte nula, agora no cálculo do valor.
            if (Suite == null)
                throw new InvalidOperationException("Cadastre a suíte antes de calcular o valor da diária.");

            // Retorna o valor da diária
            // Cálculo: DiasReservados X Suite.ValorDiaria
            decimal valor = DiasReservados * Suite.ValorDiaria;

            // Regra: Caso os dias reservados forem maior ou igual a 10, conceder um desconto de 10%
            if (DiasReservados >= 10)
            {
                valor *= 0.9m; // mesmo que valor -= valor * 0.1m
            }

            return valor;
        }
    }
}
