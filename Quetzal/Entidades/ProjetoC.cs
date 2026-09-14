using Quetzal.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Domain.Entidades
{
    public class ProjetoC
    {
        public int Id { get; set; }
        public string? NomeProjeto { get; set; } //FK da tabela Usuario
        public string UsuarioId { get; set; } = string.Empty; // FK da tabela Usuario (ajustado para string)
        public ApplicationUser Usuario { get; set; } = null!;

        public string? Descricao { get; set; } // Descrição do projeto
        public string? ImagemUpload { get; set; } // Imagem que vai ser exibida no site

        public bool Ativo { get; set; } // Indica se o projeto está ativo ou não

        public DateTime? DataAtualizacao { get; set; } // Data de atualização do projeto
        public DateTime? DataExclusao { get; set; } // Data de exclusão do projeto

        public ICollection<Ambiente> Ambientes { get; set; } = new List<Ambiente>();
    }
}