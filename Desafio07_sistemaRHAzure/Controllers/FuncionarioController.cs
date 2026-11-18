using Azure.Data.Tables;
using Microsoft.AspNetCore.Mvc;
using TrilhaNetAzureDesafio.Context;
using TrilhaNetAzureDesafio.Models;

namespace TrilhaNetAzureDesafio.Controllers;

[ApiController]
[Route("[controller]")]
public class FuncionarioController : ControllerBase
{
    private readonly RHContext _context;
    private readonly string _connectionString;
    private readonly string _tableName;

    public FuncionarioController(RHContext context, IConfiguration configuration)
    {
        _context = context;
        _connectionString = configuration.GetValue<string>("ConnectionStrings:SAConnectionString");
        _tableName = configuration.GetValue<string>("ConnectionStrings:AzureTableName");
    }

    private TableClient GetTableClient()
    {
        var serviceClient = new TableServiceClient(_connectionString);
        var tableClient = serviceClient.GetTableClient(_tableName);

        tableClient.CreateIfNotExists();
        return tableClient;
    }

    [HttpGet("{id}")]
    public IActionResult ObterPorId(int id)
    {
        var funcionario = _context.Funcionarios.Find(id);

        if (funcionario == null)
            return NotFound();

        return Ok(funcionario);
    }

    [HttpPost]
    public IActionResult Criar(Funcionario funcionario)
    {
        // MELHORIA: o log do Azure Table usa o Departamento como PartitionKey, que não pode faltar.
        // Sem esta validação, um funcionário sem departamento seria gravado no SQL e depois a
        // gravação do log falharia, deixando o SQL e o Azure Table diferentes. Validar antes evita isso.
        if (string.IsNullOrWhiteSpace(funcionario.Departamento))
            return BadRequest(new { Erro = "O departamento do funcionário não pode ser vazio" });

        _context.Funcionarios.Add(funcionario);

        // IMPLEMENTADO: SaveChanges() grava o funcionário no SQL Server. Precisa vir ANTES de criar o
        // log, porque é nesse momento que o banco gera o Id, e o log copia esse Id do funcionário.
        _context.SaveChanges();

        var tableClient = GetTableClient();
        var funcionarioLog = new FuncionarioLog(funcionario, TipoAcao.Inclusao, funcionario.Departamento, Guid.NewGuid().ToString());

        // IMPLEMENTADO: UpsertEntity grava (insere ou substitui) o log no Azure Table.
        // PartitionKey = departamento; RowKey = um Guid novo, então cada ação gera uma linha única
        // e o histórico nunca é sobrescrito.
        tableClient.UpsertEntity(funcionarioLog);

        return CreatedAtAction(nameof(ObterPorId), new { id = funcionario.Id }, funcionario);
    }

    [HttpPut("{id}")]
    public IActionResult Atualizar(int id, Funcionario funcionario)
    {
        var funcionarioBanco = _context.Funcionarios.Find(id);

        if (funcionarioBanco == null)
            return NotFound();

        // MELHORIA: mesma validação do Criar (a PartitionKey do log vem do departamento).
        if (string.IsNullOrWhiteSpace(funcionario.Departamento))
            return BadRequest(new { Erro = "O departamento do funcionário não pode ser vazio" });

        // IMPLEMENTADO: completadas as propriedades que faltavam. Antes só Nome e Endereco eram
        // atualizados, então mudar o salário ou o departamento era ignorado em silêncio.
        // O Id NÃO é copiado: ele identifica o registro e não deve mudar.
        funcionarioBanco.Nome = funcionario.Nome;
        funcionarioBanco.Endereco = funcionario.Endereco;
        funcionarioBanco.Ramal = funcionario.Ramal;
        funcionarioBanco.EmailProfissional = funcionario.EmailProfissional;
        funcionarioBanco.Departamento = funcionario.Departamento;
        funcionarioBanco.Salario = funcionario.Salario;
        funcionarioBanco.DataAdmissao = funcionario.DataAdmissao;

        // IMPLEMENTADO: Update() marca o funcionário como modificado e SaveChanges() grava no SQL.
        // (Como o Find já deixa o objeto rastreado, o Update é opcional, mas deixa a intenção explícita.)
        _context.Funcionarios.Update(funcionarioBanco);
        _context.SaveChanges();

        var tableClient = GetTableClient();
        var funcionarioLog = new FuncionarioLog(funcionarioBanco, TipoAcao.Atualizacao, funcionarioBanco.Departamento, Guid.NewGuid().ToString());

        // IMPLEMENTADO: registra a atualização no Azure Table (veja a explicação no método Criar).
        tableClient.UpsertEntity(funcionarioLog);

        return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult Deletar(int id)
    {
        var funcionarioBanco = _context.Funcionarios.Find(id);

        if (funcionarioBanco == null)
            return NotFound();

        // IMPLEMENTADO: Remove() marca o funcionário para exclusão e SaveChanges() executa o DELETE.
        // Antes só o SaveChanges estava lá, então nada era realmente apagado.
        _context.Funcionarios.Remove(funcionarioBanco);
        _context.SaveChanges();

        var tableClient = GetTableClient();

        // O objeto funcionarioBanco continua na memória mesmo após a exclusão no banco,
        // por isso ainda podemos usar os dados dele para montar o log de remoção.
        var funcionarioLog = new FuncionarioLog(funcionarioBanco, TipoAcao.Remocao, funcionarioBanco.Departamento, Guid.NewGuid().ToString());

        // IMPLEMENTADO: registra a remoção no Azure Table.
        tableClient.UpsertEntity(funcionarioLog);

        return NoContent();
    }
}
