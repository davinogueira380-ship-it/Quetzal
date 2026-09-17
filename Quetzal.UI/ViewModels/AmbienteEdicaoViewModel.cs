using System.ComponentModel.DataAnnotations;
using System.Security;

namespace Quetzal.UI.ViewModels
{ 
    public class AmbienteEdicaoViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "O nome do ambiente é obrigatório")]
        [StringLength(100, ErrorMessage = "O nome deve ter no máximo 100 caracteres.")]
        public string Nome { get; set; } = string.Empty;

        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string? Descricao { get; set; }
    }
}

// STHEFANNY Aqui ↑