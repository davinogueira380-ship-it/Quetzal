using Microsoft.AspNetCore.Identity;



namespace Quetzal.Domain.Entidades;

// Vai fazer o login no sistema, onde ele pode ter acesso aos seus projetos caso feche com a empresa, caso contrário,
// ele não pode ter acesso a area de clientes, onde ele pode ver os projetos que ele fechou com a empresa, e também pode ver o andamento dos projetos.

public class ApplicationUser : IdentityUser

{
    public required string NomeCompleto { get; set; } 
    public string Telefone { get; set; } = string.Empty;
    public string SenhaHash { get; set; } = string.Empty;
    public bool Ativo { get; set; }
    public DateTime DataCadastro { get; set; }
    public DateTime? DataAtualizacao { get; set; }
    public DateTime? DataExclusao { get; set; }

}
