using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Application.DTOs;

internal class ProjetoDto
{
    public int Id { get; set; }
    public string Titulo { get; set; } = string.Empty;
    public string Descricao { get; set; } = string.Empty;
    public int Imagem { get; set; }

}
