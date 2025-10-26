using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Models.DTO;

namespace TrilhaApiDesafio.Repositorios.Interface
{
    public interface ITarefaRepositorio
    {
        Task<Tarefa> AddTarefa(Tarefa tarefa);
        Task<Tarefa> GetTarefaById(int id);
        Task<List<Tarefa>> GetTarefas();
        Task<Tarefa> UpdateTarefa(int id, AtualizarTarefaDTO atualizarTarefaDTO);
        Task<bool> DeleteTarefa(int id);
        Task<Tarefa> GetTarefaByTitulo(string titulo);
        Task<List<Tarefa>> GetTarefasByData(DateTime data);
        Task<List<Tarefa>> GetTarefasByStatus(EnumStatusTarefa status);
    }
}
