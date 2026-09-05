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
    public interface IPortfolioRepositorio
    {

        Task<IEnumerable<Portfolio>> ObterTodosAsync(bool incluirInativos = false);

        Task<Portfolio?> ObterPorIdAsync(int id);

        Task<IEnumerable<Portfolio>> FiltrarPorAmbienteAsync(string? termo, int? categoriaId = null);

        Task<IEnumerable<Portfolio>> ObterPorAsync(int ambienteId);

        Task<Portfolio> AdicionarAsync(Portfolio filme);

        Task AtualizarAsync(Portfolio projeto);

        Task DesativarAsync(int id);

        Task ExcluirPermanentementeAsync(int id);

        Task ReativarAsync(int id);



    }
}
