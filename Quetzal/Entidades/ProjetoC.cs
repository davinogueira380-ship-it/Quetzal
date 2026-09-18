namespace Quetzal.Domain.Entidades
{
    public class ProjetoC
    {
        public int Id { get; set; }
        public string? NomeProjeto { get; set; } //FK da tabela Usuário
        public string UsuarioId { get; set; } = string.Empty; // FK da tabela Usuario 
        public ApplicationUser Usuario { get; set; } = null!;


        // Descrição do projeto
        public string? Descricao { get; set; }
        // Imagem que vai ser exibida no site
        public string? ImagemUpload { get; set; } 



        public bool Ativo { get; set; } // Indica se o projeto está ativo ou não

        public DateTime? DataAtualizacao { get; set; } // Data de atualização do projeto
        public DateTime? DataExclusao { get; set; } // Data de exclusão do projeto

        public ICollection<Ambiente> Ambientes { get; set; } = new List<Ambiente>();
    }
}