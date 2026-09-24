using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Quetzal.Application.DTOs
{
    // Representa uma foto selecionada para publicação no portfólio.
    // A imagem continua armazenada em ProjetoCFoto.
    public class PortfolioFotoDto
    {
        public int ProjetoCFotoId { get; set; }

        public string Foto { get; set; } = string.Empty;

        public int Ordem { get; set; }
    }

    public class PortfolioDto
    {
        public int Id { get; set; }

        public string NomeProjeto { get; set; } = string.Empty;

        public string Descricao { get; set; } = string.Empty;

        // Mantido temporariamente durante a transição.
        public string ImagemUpload { get; set; } = string.Empty;

        public int AmbienteId { get; set; }

        public string AmbienteNome { get; set; } = string.Empty;

        public int? ProjetoCId { get; set; }

        public string ProjetoCNome { get; set; } = string.Empty;

        // IDs das fotos escolhidas para publicação.
        public List<int> ProjetoCFotosIds { get; set; }
            = new List<int>();

        // Fotos selecionadas que serão devolvidas pela API.
        public List<PortfolioFotoDto> FotosSelecionadas { get; set; }
            = new List<PortfolioFotoDto>();

        public bool Ativo { get; set; }

        public DateTime DataCadastro { get; set; }

        public DateTime? DataAtualizacao { get; set; }

        public DateTime? DataExclusao { get; set; }
    }

    public class CriarPortfolioDto
    {
        [Required(ErrorMessage = "O título é obrigatório.")]
        [MaxLength(
            200,
            ErrorMessage = "O título não pode ter mais que 200 caracteres.")]
        public string NomeProjeto { get; set; } = string.Empty;

        [Required(ErrorMessage = "A descrição é obrigatória.")]
        public string Descricao { get; set; } = string.Empty;

        // Mantido temporariamente durante a transição.
        public string ImagemUpload { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ambiente é obrigatório.")]
        public int AmbienteId { get; set; }

        public int? ProjetoCId { get; set; }

        // Várias fotos podem ser escolhidas.
        public List<int> ProjetoCFotosIds { get; set; }
            = new List<int>();
    }

    public class AtualizarPortfolioDto : CriarPortfolioDto
    {
        [Required(ErrorMessage = "O Id é obrigatório para atualizar.")]
        public int Id { get; set; }
    }
}