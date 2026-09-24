using System;
using System.Collections.Generic;

namespace Quetzal.Domain.Entidades
{
    public class Portfolio
    {
        public int Id { get; set; }

        public string? NomeProjeto { get; set; }

        public int AmbienteId { get; set; }

        public Ambiente Ambiente { get; set; } = null!;

        public string Descricao { get; set; } = string.Empty;

        // Mantido temporariamente para não quebrar outras partes do projeto.
        // Depois poderemos remover quando toda a nova estrutura estiver funcionando.
        public string? ImagemUpload { get; set; }

        // Projeto de origem
        public int? ProjetoCId { get; set; }

        public ProjetoC? ProjetoC { get; set; }

        // Fotos do ProjetoC escolhidas para aparecer no portfólio
        public ICollection<PortfolioFoto> Fotos { get; set; }
            = new List<PortfolioFoto>();

        public bool Ativo { get; set; }

        public DateTime DataCriacao { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public DateTime? DataExclusao { get; set; }
    }
}