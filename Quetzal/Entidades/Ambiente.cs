using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Domain.Entidades
{
    internal class Ambiente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; // Nome do ambiente
        public string ProjetoId { get; set; } = string.Empty; // FK da tabela Projeto
        public Projeto Projeto { get; set; } = null!; // Referência ao projeto
    }
}
