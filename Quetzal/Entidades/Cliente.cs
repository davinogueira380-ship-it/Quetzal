using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Domain.Entidades;

// Vai fazer o login no sistema, onde ele pode ter acesso aos seus projetos caso feche com a empresa, caso contrário,
// ele não pode ter acesso a area de clientes, onde ele pode ver os projetos que ele fechou com a empresa, e também pode ver o andamento dos projetos.

public class Cliente
{
    public int Id { get; set; }
    public required string Nome { get; set; } 
    public required string Email { get; set; } 
    public int Telefone { get; set; }
    public string SenhaHash { get; set; } = string.Empty;
    public bool Ativo { get; set; }
}
