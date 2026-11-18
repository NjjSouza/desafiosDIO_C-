namespace DesafioPOO.Models
{
    public abstract class Smartphone
    {
        public string Numero { get; set; }
        // Propriedades faltantes de acordo com o diagrama
        public string Modelo { get; set; }
        public string IMEI { get; set; }
        public int Memoria { get; set; }

        public Smartphone(string numero, string modelo, string imei, int memoria)
        {
            Numero = numero;
            Modelo = modelo;

            // CORREÇÃO DO BUG: antes estava "imei = imei;", que atribui o parâmetro a ele mesmo.
            // O compilador avisa isso (CS1717), e o resultado era a propriedade IMEI ficar sempre
            // nula, porque o valor recebido nunca chegava nela. Agora o parâmetro "imei" (minúsculo)
            // é copiado para a propriedade "IMEI" (maiúsculo).
            IMEI = imei;

            Memoria = memoria;
        }

        public void Ligar()
        {
            Console.WriteLine("Ligando...");
        }

        public void ReceberLigacao()
        {
            Console.WriteLine("Recebendo ligação...");
        }

        // Método abstrato: não tem corpo aqui. Cada classe filha é OBRIGADA a implementá-lo
        // com a sua própria versão (é isso que permite o polimorfismo).
        public abstract void InstalarAplicativo(string nomeApp);
    }
}
