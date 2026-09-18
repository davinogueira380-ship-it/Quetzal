using Quetzal.Application.DTOs;
using Quetzal.Domain.Entidades;
using System.ComponentModel.DataAnnotations;

namespace Quetzal.Application.DTOs
{
    public class ProjetoCDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;        
        public string Descricao { get; set; } = string.Empty;

        //Voltar aqui STHEFANNY ↓

        //Para dados de cliente e usuário 
        public string UsuarioId { get; set; } = string.Empty;
        public string ClienteId { get; set; } = string.Empty;


        // Dados Ambiente
        public int AmbienteId { get; set; }
        public string AmbienteNome { get; set; } = string.Empty;

        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataExclusao { get; set; }
        public ICollection<AmbienteDto> Ambientes { get; set; } = new List<AmbienteDto>();
    }

    public class CriarProjetoCDto
    {
        [Required(ErrorMessage = "O nome é do Projeto é obrigatório")]
        [MaxLength(200, ErrorMessage = "O nome do Projeto não pode ter mais que 200 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;
        public string ImagemUpload { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ambiente é obrigatório.")]
        public int AmbienteId { get; set; }

        
        public ICollection<AmbienteDto> Ambientes { get; set; } = new List<AmbienteDto>(); 

        //Voltar aqui STHEFANNY ↑
       
    }

    // Dto recebe dados de atualização de um novo projeto existente
    public class AtualizarProjetoCDto : CriarProjetoCDto
    {
        [Required(ErrorMessage = "O Id é obrigatório para atualizar.")]
        public int Id { get; set; }
    }
}