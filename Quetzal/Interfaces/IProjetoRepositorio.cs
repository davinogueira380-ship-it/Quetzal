//======================================================================================
// Nome: IProjetoRepositorio.cs
//
// Objetivo: "Define o contrato (interface) que qualquer implementacao
//            de repositorio dos Projetos que deve seguir no Quetzal.
//            Abstrai o acesso a dados e permite inversao de dependencia."
//  
// Camada: Domain (Interfaces)
//
// Participa em: "Implementada pela camada Infrastructure (EF Core).
//                Injetada e consumida pela camada Application (servicos/casos de uso).
//                Respeita o principio D do SOLID (Dependency Inversion Principle)."
//
//======================================================================================

using Quetzal.Domain.Entidades;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace Quetzal.Domain.Interfaces
{
    public interface IProjetoRepositorio
    {

        Task<IEnumerable<Projeto>> ObterTodosAsync(bool incluirInativos = false);

        Task<Projeto?> ObterPorIdAsync(int id);

        Task<IEnumerable<Projeto>> FiltrarPorAsync(string? termo, int? categoriaId = null);

        Task<IEnumerable<Projeto>> ObterPorCategoriaAsync(int categoriaId);

        Task<Projeto> AdicionarAsync(Projeto filme);

        Task AtualizarAsync(Projeto projeto);

        Task DesativarAsync(int id);

        Task ExcluirPermanentementeAsync(int id);

        Task ReativarAsync(int id);



    }
}
