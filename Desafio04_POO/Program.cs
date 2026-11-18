using DesafioPOO.Models;

// Realiza os testes com as classes Nokia e Iphone
class Program
{
    static void Main(string[] args)
    {
        // MELHORIA: UTF8 no console para exibir acentuação (ex.: "Número", "Memória") também no
        // Windows. Os outros desafios do repositório já fazem isso. Fica DENTRO do Main porque,
        // fora da classe, viraria um "top-level statement" e o C# deixaria de usar este Main.
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        // Nokia
        // MELHORIA: a memória estava como 00 (que é só o número 0, e imprimia "0 GB").
        // Troquei por um valor ilustrativo; os números são exemplos, não as especificações reais.
        Nokia meuNokia = new Nokia("1234-5678", "Nokia 3310", "111222333444555", 1);
        Console.WriteLine("Teste Nokia");
        Console.WriteLine($"Número: {meuNokia.Numero}");
        Console.WriteLine($"Modelo: {meuNokia.Modelo}");
        Console.WriteLine($"IMEI: {meuNokia.IMEI}");
        Console.WriteLine($"Memória: {meuNokia.Memoria} GB");
        meuNokia.Ligar();
        meuNokia.ReceberLigacao();
        meuNokia.InstalarAplicativo("WhatsApp");

        Console.WriteLine();

        // iPhone
        // MELHORIA: memória 000 trocada por 128 (valor ilustrativo, igual à lógica do Nokia acima).
        Iphone meuIphone = new Iphone("991413637-", "iPhone 14", "555444333222111", 128);
        Console.WriteLine("Teste iPhone");
        Console.WriteLine($"Número: {meuIphone.Numero}");
        Console.WriteLine($"Modelo: {meuIphone.Modelo}");
        Console.WriteLine($"IMEI: {meuIphone.IMEI}");
        Console.WriteLine($"Memória: {meuIphone.Memoria} GB");
        meuIphone.Ligar();
        meuIphone.ReceberLigacao();
        meuIphone.InstalarAplicativo("TikTok");

        Console.WriteLine();

        // Polimorfismo: o array é do tipo da classe mãe (Smartphone), mas guarda um Nokia e um
        // Iphone. Na chamada de InstalarAplicativo, o C# executa a versão de cada classe filha.
        Smartphone[] celulares = { meuNokia, meuIphone };
        Console.WriteLine("Teste Polimorfismo");
        foreach (var celular in celulares)
        {
            celular.Ligar();
            celular.InstalarAplicativo("Spotify");
        }
    }
}
