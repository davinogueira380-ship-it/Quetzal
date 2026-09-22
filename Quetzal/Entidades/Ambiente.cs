using System;
using System.Collections.Generic;

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

        // Relação 1:N com Portfolio (cada Ambiente pode ter vários Portfolios)
        public ICollection<Portfolio> Portfolios { get; set; } = new List<Portfolio>();

        // Relação N:N com ProjetoC (cada Ambiente pode ter vários ProjetosC e vice-versa)
        public ICollection<ProjetoC> ProjetosC { get; set; } = new List<ProjetoC>();
    }
}