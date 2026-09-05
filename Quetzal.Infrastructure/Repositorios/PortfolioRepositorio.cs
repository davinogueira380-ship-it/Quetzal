using Microsoft.EntityFrameworkCore;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using Quetzal.Infrastructure.Dados;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Infrastructure.Repositorios
{
    public class PortfolioRepositorio : IPortfolioRepositorio
    {
        private readonly QuetzalContexto _context;

        public PortfolioRepositorio(QuetzalContexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Portfolio>> ObterTodosAsync(bool incluirInativos = false)
        {
            IQueryable<Portfolio> query = _context.Portfolios.Include(Portfolio => Portfolio.Ambiente);

            // Se incluirInativos for false, filtra apenas os portfolios ativos
            if (!incluirInativos)
            {
                query = query.Where(Portfolio => Portfolio.Ativo);
            }
            return await query.ToListAsync();
        }

        public async Task<Portfolio?> ObterPorIdAsync(int id)
        {
            return await _context.Portfolios.Include(Portfolio => Portfolio.Ambiente).FirstOrDefaultAsync(Portfolio => Portfolio.Id == id);
        }

        public async Task<IEnumerable<Portfolio>> FiltrarPorAmbienteAsync(string? termo, int? ambienteId = null)
        {
            var query = _context.Portfolios.Include(Portfolio => Portfolio.Ambiente).Where(Portfolio => Portfolio.Ativo);
            
            if (ambienteId.HasValue && ambienteId.Value > 0)
            {
                query = query.Where(Portfolio => Portfolio.AmbienteId == ambienteId.Value);
            }
            if (!string.IsNullOrWhiteSpace(termo))
            {
                var t = $"%{termo}";
                query = query.Where(Portfolio => EF.Functions.Like(Portfolio.NomeProjeto, t) || EF.Functions.Like(Portfolio.Descricao, t));
            }
            return await query.ToListAsync();

        }

        public async Task<IEnumerable<Portfolio>> ObterPorAsync(int ambienteId)
        {
            return await _context.Portfolios.Include(Portfolio => Portfolio.Ambiente).Where(Portfolio => Portfolio.AmbienteId == ambienteId).ToListAsync();
        }

        public async Task<Portfolio> AdicionarAsync(Portfolio portfolio)
        {
            await _context.Portfolios.AddAsync(portfolio);
            await _context.SaveChangesAsync();
            return portfolio;
        }
        
        public async Task AtualizarAsync(Portfolio portfolio)
        {
            _context.Portfolios.Update(portfolio);
            await _context.SaveChangesAsync();
        }
        
        public async Task DesativarAsync(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio != null)
            {
                portfolio.Ativo = false;
                portfolio.DataAtualizacao = DateTime.Now;
                portfolio.DataExclusao = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task ExcluirPermanentementeAsync(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio != null)
            {
                _context.Portfolios.Remove(portfolio);
                await _context.SaveChangesAsync();
            }
        }
        
        public async Task ReativarAsync(int id)
        {
            var portfolio = await _context.Portfolios.FindAsync(id);
            if (portfolio != null)
            {
                portfolio.Ativo = true;
                portfolio.DataExclusao = null;
                portfolio.DataAtualizacao = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

    }
}
