using System;
using TrilhaApiDesafio.Models;

namespace TrilhaApiDesafio.Models.DTO
{
    public class AtualizarTarefaDTO
    {
        public string Titulo { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public EnumStatusTarefa Status { get; set; }
    }
}
