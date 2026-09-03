using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quetzal.Application.DTOs
{
    public class PortfolioDto
    {
        public int Id { get; set; }
        public string NomeProjeto { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string ImagemUpload { get; set; } = string.Empty;
        // Dados Ambiente
        public int AmbienteId { get; set; }
        public string AmbienteNome { get; set; } = string.Empty;

        // Dados da situação do projeto
        public bool Ativo { get; set; }
        public DateTime DataCadastro { get; set; }
        public DateTime? DataAtualizacao { get; set; }
        public DateTime? DataExclusao { get; set; }
    }


    // DTO utilizado para receber dados de criação de um novo ambiente

    public class CriarPortfolioDto
{
        [Required(ErrorMessage = "O título é obrigatório")]
        [MaxLength(200, ErrorMessage = "O Título não pode ter mais que 200 caracteres.")]
    public string Titulo { get; set; } = string.Empty;

        [Required(ErrorMessage = "A Descrição é obrigatória.")]
    public string Descricao { get; set; } = string.Empty;
        public string ImagemUpload { get; set; } = string.Empty;

        [Required(ErrorMessage = "O ambiente é obrigatoria.")]
        public int AmbienteId { get; set; }
    }

    // Dto recebe dados de atualização de um novo projeto existente
    // Herda de CriarPortfolioDto e adiciona apenas o Id.
    public class AtualizarPortfolioDto : CriarPortfolioDto
    {
        [Required(ErrorMessage = "O Id é obrigatório para atualizar.")]
        public int Id { get; set; }
    }


}
