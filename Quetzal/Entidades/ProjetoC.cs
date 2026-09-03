using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Domain.Entidades
{
    public class ProjetoC
    {
        public int Id { get; set; }
        public string? NomeProjeto { get; set; } //FK da tabela Usuario
        public int UsuarioId { get; set; } // FK da tabela Usuario
        public ApplicationUser Usuario { get; set; } = null!;

        public string? Descricao { get; set; } // Descrição do projeto
        public string? ImagemUpload { get; set; } // Imagem que vai ser exibida no site

        public bool Ativo { get; set; } // Indica se o projeto está ativo ou não

        public DateTime? DataAtualizacao { get; set; } // Data de atualização do projeto
    }
}
