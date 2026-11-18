namespace DesafioPOO.Models
{
    // Herda da classe "Smartphone"
    public class Iphone : Smartphone
    {
        // CORREÇÃO: mesmo problema do Nokia. Sem este construtor, o código não compilava,
        // porque a classe mãe exige quatro valores e as filhas não herdam construtores.
        public Iphone(string numero, string modelo, string imei, int memoria)
            : base(numero, modelo, imei, memoria)
        {
        }

        // Sobrescreve o método "InstalarAplicativo"
        public override void InstalarAplicativo(string nomeApp)
        {
            Console.WriteLine($"{nomeApp} instalado no iPhone...");
        }
    }
}
