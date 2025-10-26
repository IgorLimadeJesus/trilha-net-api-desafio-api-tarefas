using Microsoft.AspNetCore.Mvc;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Models.DTO;
using TrilhaApiDesafio.Repositorios.Interface;

namespace TrilhaApiDesafio.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TarefaController : ControllerBase
    {
        private readonly ITarefaRepositorio _tarefaRepositorio;

        public TarefaController(ITarefaRepositorio tarefaRepositorio)
        {
            _tarefaRepositorio = tarefaRepositorio;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterPorId(int id)
        {
            var tarefa = await _tarefaRepositorio.GetTarefaById(id);

            if (tarefa == null)
                return NotFound(new { Mensagem = "Tarefa não encontrada." });

            return Ok(tarefa);
        }

        [HttpGet("ObterTodos")]
        public async Task<IActionResult> ObterTodos()
        {
            var tarefas = await _tarefaRepositorio.GetTarefas();
            return Ok(tarefas);
        }

        [HttpGet("ObterPorTitulo")]
        public async Task<IActionResult> ObterPorTitulo(string titulo)
        {
            var tarefa = await _tarefaRepositorio.GetTarefaByTitulo(titulo);

            if (tarefa == null)
                return NotFound(new { Mensagem = "Nenhuma tarefa encontrada com esse título." });

            return Ok(tarefa);
        }

        [HttpGet("ObterPorData")]
        public async Task<IActionResult> ObterPorData(DateTime data)
        {
            var tarefas = await _tarefaRepositorio.GetTarefasByData(data);

            if (tarefas == null || !tarefas.Any())
                return NotFound(new { Mensagem = "Nenhuma tarefa encontrada para essa data." });

            return Ok(tarefas);
        }

        [HttpGet("ObterPorStatus")]
        public async Task<IActionResult> ObterPorStatus(EnumStatusTarefa status)
        {
            var tarefas = await _tarefaRepositorio.GetTarefasByStatus(status);

            if (tarefas == null || !tarefas.Any())
                return NotFound(new { Mensagem = "Nenhuma tarefa encontrada com esse status." });

            return Ok(tarefas);
        }

        [HttpPost]
        public async Task<IActionResult> Criar(Tarefa tarefa)
        {
            if (tarefa.Data == DateTime.MinValue)
                return BadRequest(new { Erro = "A data da tarefa não pode ser vazia." });

            var novaTarefa = await _tarefaRepositorio.AddTarefa(tarefa);
            return CreatedAtAction(nameof(ObterPorId), new { id = novaTarefa.Id }, novaTarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarTarefaDTO atualizarTarefaDTO)
        {
            var tarefaAtualizada = await _tarefaRepositorio.UpdateTarefa(id, atualizarTarefaDTO);

            if (tarefaAtualizada == null)
                return NotFound(new { Erro = "Tarefa não encontrada." });

            return Ok(tarefaAtualizada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Deletar(int id)
        {
            var sucesso = await _tarefaRepositorio.DeleteTarefa(id);

            if (!sucesso)
                return NotFound(new { Erro = "Tarefa não encontrada." });

            return NoContent();
        }
    }
}
