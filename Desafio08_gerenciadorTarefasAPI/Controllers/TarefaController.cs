using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly OrganizadorContext _context;

        public TarefaController(OrganizadorContext context)
        {
            _context = context;
        }

        [HttpGet("{id}")]
        public IActionResult ObterPorId(int id)
        {
            // IMPLEMENTADO: Find() busca pela chave primária (Id). Ele retorna null quando não
            // existe nenhuma tarefa com aquele Id, e é isso que usamos para decidir o status HTTP.
            var tarefa = _context.Tarefas.Find(id);

            // 404 Not Found quando não encontra; caso contrário, 200 OK com a tarefa.
            if (tarefa == null)
                return NotFound();

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public IActionResult ObterTodos()
        {
            // IMPLEMENTADO: ToList() executa a consulta no banco agora e devolve todas as tarefas.
            // Uma lista vazia também é um resultado válido (200 OK com []), não é erro.
            var tarefas = _context.Tarefas.ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public IActionResult ObterPorTitulo(string titulo)
        {
            // MELHORIA: sem esta validação, titulo == null faria Contains() lançar exceção (erro 500).
            // 400 Bad Request avisa o cliente de que o parâmetro é obrigatório.
            if (string.IsNullOrWhiteSpace(titulo))
                return BadRequest(new { Erro = "O título não pode ser vazio" });

            // IMPLEMENTADO: Contains() é traduzido pelo EF para um LIKE '%titulo%' no SQL Server,
            // então traz todas as tarefas cujo título contém o texto (não só as idênticas).
            // Se o Titulo guardado no banco for nulo, o EF trata isso no SQL sem quebrar.
            var tarefas = _context.Tarefas.Where(x => x.Titulo.Contains(titulo)).ToList();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorData")]
        public IActionResult ObterPorData(DateTime data)
        {
            // MELHORIA: adicionado ToList(). Antes o Where() devolvia a consulta ainda "não executada"
            // e ela só rodava durante a serialização da resposta. Executar aqui, dentro do método,
            // deixa o comportamento previsível e igual ao dos outros endpoints.
            var tarefa = _context.Tarefas.Where(x => x.Data.Date == data.Date).ToList();
            return Ok(tarefa);
        }

        [HttpGet("ObterPorStatus")]
        public IActionResult ObterPorStatus(EnumStatusTarefa status)
        {
            // IMPLEMENTADO (seguindo o exemplo de ObterPorData): filtra pelo status recebido.
            // O ToList() foi adicionado pelo mesmo motivo explicado em ObterPorData.
            var tarefa = _context.Tarefas.Where(x => x.Status == status).ToList();
            return Ok(tarefa);
        }

        [HttpPost]
        public IActionResult Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // IMPLEMENTADO: Add() só marca a tarefa como "nova" na memória do EF.
            // Nada vai para o banco até chamar SaveChanges().
            _context.Tarefas.Add(tarefa);
            _context.SaveChanges();

            // Depois do SaveChanges, o banco gerou o Id e o EF já o preencheu em "tarefa".
            // Por isso o CreatedAtAction (201 Created) consegue montar a URL correta da nova tarefa.
            return CreatedAtAction(nameof(ObterPorId), new { id = tarefa.Id }, tarefa);
        }

        [HttpPut("{id}")]
        public IActionResult Atualizar(int id, Tarefa tarefa)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia" });

            // IMPLEMENTADO: copiamos os campos recebidos para a tarefa que veio do banco.
            // O Id NÃO é copiado: ele identifica o registro e não deve mudar.
            tarefaBanco.Titulo = tarefa.Titulo;
            tarefaBanco.Descricao = tarefa.Descricao;
            tarefaBanco.Data = tarefa.Data;
            tarefaBanco.Status = tarefa.Status;

            // Update() marca a tarefa como modificada; SaveChanges() grava no banco.
            // (Como tarefaBanco já vem "rastreada" pelo Find, o Update é opcional, mas deixa a intenção explícita.)
            _context.Tarefas.Update(tarefaBanco);
            _context.SaveChanges();

            // MELHORIA: antes retornava Ok() vazio. Devolver a tarefa atualizada é útil para o cliente.
            return Ok(tarefaBanco);
        }

        [HttpDelete("{id}")]
        public IActionResult Deletar(int id)
        {
            var tarefaBanco = _context.Tarefas.Find(id);

            if (tarefaBanco == null)
                return NotFound();

            // IMPLEMENTADO: Remove() marca a tarefa para exclusão e SaveChanges() executa o DELETE.
            _context.Tarefas.Remove(tarefaBanco);
            _context.SaveChanges();

            // 204 No Content: a exclusão deu certo e não há nada para devolver.
            return NoContent();
        }
    }
}
