using Microsoft.EntityFrameworkCore;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using Quetzal.Infrastructure.Dados;

namespace Quetzal.Infrastructure.Repositorios
{
    public class ProjetoCRepositorio : IProjetoCRepositorio
    {
        private readonly QuetzalContexto _context;

        public ProjetoCRepositorio(QuetzalContexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ProjetoC>> ObterTodosAsync(bool incluirInativos = false)
        {
            IQueryable<ProjetoC> query = _context.ProjetoC.Include(ProjetoC => ProjetoC.Usuario);
            if (!incluirInativos)
            {
                query = query.Where(ProjetoC => ProjetoC.Ativo);
            }
            return await query.ToListAsync();
        }

        public async Task<ProjetoC?> ObterPorIdAsync(int id)
        {
            return await _context.ProjetoC.Include(ProjetoC => ProjetoC.Usuario).FirstOrDefaultAsync(ProjetoC => ProjetoC.Id == id);
        }

        //public async Task<IEnumerable<ProjetoC>> FiltrarPorAmbienteAsync(string? termo, string? usuarioId = null)
        //{
        //    var query = _context.ProjetoC.Include(ProjetoC => ProjetoC.Usuario).Where(ProjetoC => ProjetoC.Ativo);
        //    if (!string.IsNullOrWhiteSpace(usuarioId))
        //    {
        //        query = query.Where(ProjetoC => ProjetoC.UsuarioId == usuarioId);
        //    }
        //    if (!string.IsNullOrWhiteSpace(termo))
        //    {
        //        var t = $"%{termo}";
        //        query = query.Where(ProjetoC => EF.Functions.Like(ProjetoC.NomeProjeto, t) || EF.Functions.Like(ProjetoC.Descricao, t));
        //    }
        //    return await query.ToListAsync();
        //}

        //public async Task<IEnumerable<ProjetoC>> ObterPorAmbienteAsync(string usuarioId)
        //{
        //    return await _context.ProjetoC.Include(ProjetoC => ProjetoC.Usuario).Where(ProjetoC => ProjetoC.UsuarioId == usuarioId).ToListAsync();
        //}

        public async Task<ProjetoC> AdicionarAsync(ProjetoC projetoC)
        {
            _context.ProjetoC.Add(projetoC);
            await _context.SaveChangesAsync();
            return projetoC;
        }

        public async Task AtualizarAsync(ProjetoC projetoC)
        {
            _context.ProjetoC.Update(projetoC);
            await _context.SaveChangesAsync();
        }

        public async Task DesativarAsync(int id)
        {
            var projetoC = await _context.ProjetoC.FindAsync(id);
            if (projetoC != null)
            {
                projetoC.Ativo = false;
                projetoC.DataAtualizacao = DateTime.Now;
                projetoC.DataExclusao = DateTime.Now;
                await _context.SaveChangesAsync();
            }
        }

        public async Task ExcluirPermanentementeAsync(int id)
        {
            var projetoC = await _context.ProjetoC.FindAsync(id);
            if (projetoC != null)
            {
                _context.ProjetoC.Remove(projetoC);
                await _context.SaveChangesAsync();
            }
        }

        public async Task ReativarAsync(int id)
        {
            var projetoC = await _context.ProjetoC.FindAsync(id);
            if (projetoC != null)
            {
                projetoC.Ativo = true;
                projetoC.DataAtualizacao = DateTime.Now;
                projetoC.DataExclusao = null;
                await _context.SaveChangesAsync();
            }
        }

        //public Task<IEnumerable<ProjetoC>> FiltrarPorAmbienteAsync(string? termo, int? ambienteId = null)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IEnumerable<ProjetoC>> ObterPorAmbienteAsync(int ambienteId)
        //{
        //    throw new NotImplementedException();
        //}
    }
}