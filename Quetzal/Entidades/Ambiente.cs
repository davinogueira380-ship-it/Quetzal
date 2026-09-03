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
        public string PortfolioId { get; set; } = string.Empty; // FK da tabela Portfolio
        public Portfolio Portfolio { get; set; } = null!; // Referência ao portfolio

        public bool Ativo { get; set; } // Indica se o ambiente está ativo ou não

        public ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

        public DateTime? DataAtualizacao { get; set; }


    }

}
