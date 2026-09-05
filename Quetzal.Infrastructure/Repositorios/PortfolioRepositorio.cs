using Microsoft.EntityFrameworkCore;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using Quetzal.Infrastructure.Dados;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Infrastructure.Repositorios
{
    //public class PortfolioRepositorio : IPortfolioRepositorio
    //{
    //    private readonly QuetzalContexto _context;

    //    public PortfolioRepositorio(QuetzalContexto context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<IEnumerable<Portfolio>> ObterTodosAsync(bool incluirInativos = false)
    //    {
    //        IQueryable<Portfolio> query = _context.Portfolios.Include(Portfolio => Portfolio.Ambiente);

    //        // Se incluirInativos for false, filtra apenas os portfolios ativos
    //        if (!incluirInativos)
    //        {
    //            query = query.Where(Portfolio => Portfolio.Ativo);
    //        }
    //        return await query.ToListAsync();
    //    }

    //    public async Task<Portfolio?> ObterPorIdAsync(int id)
    //    {
    //        return await _context.Portfolios.Include(Portfolio => Portfolio.Ambiente).FirstOrDefaultAsync(Portfolio => Portfolio.Id == id);
    //    }

    //    public async Task<IEnumerable<Portfolio>> FiltrarPorAmbienteAsync(bool incluirInativos, int? ambienteId = null)
    //    {
    //        var query = _context.Portfolios.Include(Portfolio => Portfolio.Ambiente).AsQueryable();
    //        if (ambienteId.)
    //        {
                
    //        }
    //    }




    //}
}
