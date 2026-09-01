using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Domain.Entidades;

// O cliente vai ter acesso a vários projetos, e cada projeto vai ter um cliente associado a ele.
// A relação entre Cliente e Projeto é de um para muitos, ou seja, um cliente pode ter vários projetos, mas cada projeto pertence a apenas um cliente.

public class Projeto
{
    public int Id { get; set; }
    public string? NomeCompleto { get; set; } //FK da tabela Usuario
    public int UsuarioId { get; set; } // FK da tabela Usuario
    public Usuario Usuario { get; set; } = null!;
    public string Descricao { get; set; } = string.Empty; // Descrição do projeto
    public string? ImagemUpload { get; set; } // Imagem que vai ser exibida no site
   
}
