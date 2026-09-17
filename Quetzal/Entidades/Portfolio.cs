using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Domain.Entidades;

// O cliente vai ter acesso a vários projetos, e cada projeto vai ter um cliente associado a ele.
// A relação entre Cliente e Projeto é de um para muitos, ou seja, um cliente pode ter vários projetos, mas cada projeto pertence a apenas um cliente.

public class Portfolio
{
    public int Id { get; set; }
    public string? NomeProjeto { get; set; } //FK da tabela Usuario


    public Ambiente Ambiente { get; set; } = null!; 
    public int AmbienteId { get; set; } 


    public string Descricao { get; set; } = string.Empty; 
    public string? ImagemUpload { get; set; } 


    public bool Ativo { get; set; }

    public DateTime DataCriacao { get; set; } // Data de criação do projeto
    public DateTime? DataAtualizacao { get; set; } // Data de atualização do projeto
    public DateTime?  DataExclusao { get; set; }

}
