using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Quetzal.Domain.Entidades
{
    public class Ambiente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty; // Nome do ambiente
        public string ProjetoId { get; set; } = string.Empty; // FK da tabela Projeto
        public Projeto Projeto { get; set; } = null!; // Referência ao projeto

        public ICollection<Ambiente> Ambientes { get; set; } = new List<Ambiente>();


    }

}
