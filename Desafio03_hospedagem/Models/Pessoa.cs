namespace DesafioProjetoHospedagem.Models;

public class Pessoa
{
    public Pessoa() { }

    public Pessoa(string nome)
    {
        Nome = nome;
    }

    public Pessoa(string nome, string sobrenome)
    {
        Nome = nome;
        Sobrenome = sobrenome;
    }

    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    // CORREÇÃO: quando a pessoa tem só o nome (Sobrenome == null), o texto ficava com um espaço
    // sobrando no fim ("HÓSPEDE 1 "). O Trim() remove espaços das pontas.
    public string NomeCompleto => $"{Nome} {Sobrenome}".Trim().ToUpper();
}