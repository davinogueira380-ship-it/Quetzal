using Microsoft.EntityFrameworkCore;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using Quetzal.Infrastructure.Dados;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Threading.Tasks;

namespace Quetzal.Infrastructure.Repositorios
{
    public class UsuarioRepositorio : IUsuarioRepositorio
    {
        private readonly QuetzalContexto _context;

        public UsuarioRepositorio(QuetzalContexto context)
        {
            _context = context;
        }

        public async Task<IEnumerable<ApplicationUser>> ObterTodosAsync(bool incluirInativos = false)
        {
            IQueryable<ApplicationUser> query = _context.Usuarios.Include(u => u.ProjetosC);

            if (!incluirInativos)
            {
                query = query.Where(u => u.Ativo);
            }

            return await query.ToListAsync();
        }

        public async Task<ApplicationUser?> ObterPorIdAsync(string id)
        {
            return await _context.Usuarios.Include(u => u.ProjetosC)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<ApplicationUser> AdicionarAsync(ApplicationUser usuario)
        {
            _context.Usuarios.Add(usuario);
            await _context.SaveChangesAsync();
            return usuario;
        }

        public async Task<ApplicationUser> AtualizarAsync(string id, ApplicationUser usuario)
        {
            var existente = await ObterPorIdAsync(id);
            if (existente == null)
                throw new KeyNotFoundException("Usuário não encontrado.");

            _context.Entry(existente).CurrentValues.SetValues(usuario);
            await _context.SaveChangesAsync();
            return existente;
        }

        public async Task<ApplicationUser> AtualizarAsync(ApplicationUser usuarioExistente)
        {
            _context.Usuarios.Update(usuarioExistente);
            await _context.SaveChangesAsync();
            return usuarioExistente;
        }

        public async Task DesativarAsync(string id)
        {
            var u = await ObterPorIdAsync(id);
            if (u == null)
                throw new KeyNotFoundException("Usuário não encontrado.");
            u.Ativo = false;
            await _context.SaveChangesAsync();
        }

        public async Task ReativarAsync(string id)
        {
            var u = await ObterPorIdAsync(id);
            if (u == null)
                throw new KeyNotFoundException("Usuário não encontrado.");
            u.Ativo = true;
            await _context.SaveChangesAsync();
        }

        public async Task ExcluirPermanentementeAsync(string id)
        {
            var u = await ObterPorIdAsync(id);
            if (u == null)
                return;
            _context.Usuarios.Remove(u);
            await _context.SaveChangesAsync();
        }
    }
}
