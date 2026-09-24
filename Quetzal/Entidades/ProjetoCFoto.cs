using System;
using System.Collections.Generic;
using System.Text;

    
        namespace Quetzal.Domain.Entidades
    {
        public class ProjetoCFoto
        {
            public int Id { get; set; }

            // Projeto ao qual esta foto pertence
            public int ProjetoCId { get; set; }

            // Aqui ficará a informação da foto
            public string Foto { get; set; } = string.Empty;

            // Permite manter a ordem das fotos
            public int Ordem { get; set; }

            // Relacionamento com ProjetoC
            public ProjetoC ProjetoC { get; set; } = null!;
        }
    }

