using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Quetzal.Domain.Entidades
{

    public class Ambiente
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public string? ImagemUpload { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    }

    /// <summary>
    /// Comentado para incluir o código sugerido pelo copilot.
    /// <summary/>

    //public class Ambiente
    //{
    //    public int Id { get; set; }
    //    public string Nome { get; set; } = string.Empty; // Nome do ambiente
    //    public int PortfolioId { get; set; } // FK da tabela Portfolio
    //    public Portfolio Portfolio { get; set; } = null!; // Referência ao portfolio
    //    public string? ImagemUpload { get; set; }
    //    public bool Ativo { get; set; } // Indica se o ambiente está ativo ou não
    //    public DateTime DataCadastro { get; set; } // Data de cadastro do ambiente
    //    public DateTime? DataAtualizacao { get; set; }
    //    public DateTime? DataExclusao { get; set; }


    //    public ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

    //}

}
