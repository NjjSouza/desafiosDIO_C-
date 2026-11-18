namespace DesafioPOO.Models
{
    // Herda da classe "Smartphone"
    public class Nokia : Smartphone
    {
        // CORREÇÃO: esta classe não tinha construtor, mas o Program.cs cria o objeto com quatro
        // argumentos (new Nokia(numero, modelo, imei, memoria)), e o código nem compilava
        // (erros CS7036 e CS1729). A classe mãe exige esses quatro valores no construtor, e as
        // classes filhas não herdam construtores: é preciso declará-lo e repassar os valores com
        // ": base(...)".
        public Nokia(string numero, string modelo, string imei, int memoria)
            : base(numero, modelo, imei, memoria)
        {
        }

        // Sobrescreve o método "InstalarAplicativo"
        public override void InstalarAplicativo(string nomeApp)
        {
            Console.WriteLine($"{nomeApp} instalado no Nokia...");
        }
    }
}
