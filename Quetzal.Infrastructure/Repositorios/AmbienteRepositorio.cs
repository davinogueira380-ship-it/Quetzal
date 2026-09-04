using Microsoft.EntityFrameworkCore;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using Quetzal.Infrastructure.Dados;


namespace Quetzal.Infrastructure.Repositorios
{
    public class AmbienteRepositorio : IAmbienteRepositorio
    {
        private readonly QuetzalContexto _contexto;

        public AmbienteRepositorio(QuetzalContexto contexto)
        {
            _contexto = contexto;
        }
        public async Task<IEnumerable<Ambiente>> ObterTodasAsync(bool incluirInativas = false)
        {
           IQueryable<Ambiente> query = _contexto.Ambientes.Include(a => a.Portfolio);
            if (!incluirInativas)
            {
                query = query.Where(a => a.Ativo);
            }
            return await query.ToListAsync();
        }

        public async Task<Ambiente?> ObterPorIdAsync(int id)
        {
            return await _contexto.Ambientes.Include(a => a.Portfolio).FirstOrDefaultAsync(a => a.Id == id);
        }
        
        public async Task<IEnumerable<Ambiente>> FiltrarPorAsync(string? termo, int? ambienteId = null)
        {
            IQueryable<Ambiente> query = _contexto.Ambientes.Include(a => a.Portfolio);
            if (!string.IsNullOrEmpty(termo))
            {
                query = query.Where(a => a.Nome.Contains(termo));
            }
            if (ambienteId.HasValue)
            {
                query = query.Where(a => a.Id == ambienteId.Value);
            }
            return await query.ToListAsync();
        }

        public async Task<Ambiente> AdicionarAsync(Ambiente ambiente)
        {
            // O método AddAsync é usado para adicionar a categoria ao contexto do Entity Framework
            await _contexto.Ambientes.AddAsync(ambiente);
            await _contexto.SaveChangesAsync();
            return ambiente;
        }

        public async Task AtualizarAsync(Ambiente ambiente)
        {
            _contexto.Ambientes.Update(ambiente);
            await _contexto.SaveChangesAsync();
        }

        public async Task DesativarAsync(int id)
        {
            var ambiente = await _contexto.Ambientes.FindAsync(id);
            if (ambiente != null)
            {
                ambiente.Ativo = false;
                ambiente.DataExclusao = DateTime.UtcNow;
                ambiente.DataAtualizacao = DateTime.UtcNow;
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task ReativarAsync(int id)
        {
            var ambente = await _contexto.Ambientes.FindAsync(id);
            if (ambente != null)
            {
                ambente.Ativo = true;
                ambente.DataExclusao = null;
                ambente.DataAtualizacao = DateTime.UtcNow;
                await _contexto.SaveChangesAsync();
            }
        }

        public async Task ExcluirPermanentementeAsync(int id)
        {
            var ambiente = await _contexto.Ambientes.FindAsync(id);
            if (ambiente != null)
            {
                _contexto.Ambientes.Remove(ambiente);
                await _contexto.SaveChangesAsync();
            }
            
        }

    }
}
