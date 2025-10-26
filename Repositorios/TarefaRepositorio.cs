using Microsoft.EntityFrameworkCore;
using TrilhaApiDesafio.Context;
using TrilhaApiDesafio.Models;
using TrilhaApiDesafio.Models.DTO;
using TrilhaApiDesafio.Repositorios.Interface;

namespace TrilhaApiDesafio.Repositorios
{
    public class TarefaRepositorio : ITarefaRepositorio
    {
        private readonly OrganizadorContext _context;

        public TarefaRepositorio(OrganizadorContext context)
        {
            _context = context;
        }

        public async Task<Tarefa> AddTarefa(Tarefa tarefa)
        {
            await _context.Tarefas.AddAsync(tarefa);
            await _context.SaveChangesAsync();
            return tarefa;
        }

        public async Task<bool> DeleteTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return false;

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<Tarefa> GetTarefaById(int id)
        {
            return await _context.Tarefas.FindAsync(id);
        }

        public async Task<Tarefa> GetTarefaByTitulo(string titulo)
        {
            return await _context.Tarefas.FirstOrDefaultAsync(t => t.Titulo == titulo);
        }

        public async Task<List<Tarefa>> GetTarefas()
        {
            return await _context.Tarefas.ToListAsync();
        }

        public async Task<List<Tarefa>> GetTarefasByData(DateTime data)
        {
            return await _context.Tarefas
                .Where(t => t.Data.Date == data.Date)
                .ToListAsync();
        }

        public async Task<List<Tarefa>> GetTarefasByStatus(EnumStatusTarefa status)
        {
            return await _context.Tarefas
                .Where(t => t.Status == status)
                .ToListAsync();
        }

        public async Task<Tarefa> UpdateTarefa(int id, AtualizarTarefaDTO atualizarTarefaDTO)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
                return null;

            tarefa.Titulo = atualizarTarefaDTO.Titulo;
            tarefa.Descricao = atualizarTarefaDTO.Descricao;
            tarefa.Status = atualizarTarefaDTO.Status;
            tarefa.Data = atualizarTarefaDTO.Data;

            _context.Tarefas.Update(tarefa);
            await _context.SaveChangesAsync();

            return tarefa;
        }
    }
}
