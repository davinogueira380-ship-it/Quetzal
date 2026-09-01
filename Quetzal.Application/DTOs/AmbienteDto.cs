using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Quetzal.Application.DTOs
{
    public class AmbienteDto
    {
        public int Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string? Descricao { get; set; }
        public bool Ativo {  get; set; }
        public DateTime DataCastro { get; set; }
        public int TotalProjetos { get; set; }
        public string? ImagemUpload { get; set; }

    }

    // Dto para criação e atualização dos AMBIENTES
    public class CriarAbienteDto
    {
        [Required(ErrorMessage = "O nome do ambiente é obrigatório")]
        [MaxLength(100, ErrorMessage = "O nome não pode execeder 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [MaxLength(500, ErrorMessage = "A descrição não pode exceder 500 caracteres")]
        public string? Descricao { get; set; }
        public string? ImagemUpload { get; set; }

    }
    
}
