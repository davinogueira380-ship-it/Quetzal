using Microsoft.EntityFrameworkCore;
using Quetzal.Domain.Entidades;
using Quetzal.Domain.Interfaces;
using Quetzal.Infrastructure.Dados;
using System;
using System.Collections.Generic;
using System.Text;

namespace Quetzal.Infrastructure.Repositorios
{
    //public class UsuarioRepositorio : IUsuarioRepositorio
    //{
    //    private readonly QuetzalContexto _context;
        
    //    public UsuarioRepositorio(QuetzalContexto context)
    //    {
    //        _context = context;
    //    }

    //    public async Task<IEnumerable<ApplicationUser>> ObterTodosAsync(bool incluirInativos = false)
    //    {
    //        IQueryable<ApplicationUser> query = _context.Usuarios.Include(pc => pc.ProjetosC);

    //        // Se incluirInativos for false, filtra apenas os projetos de usuarios ativos
    //        if (!incluirInativos)
    //        {
    //            query = query.Where(pc => pc.Ativo);
    //        }
    //        return await query.ToListAsync();
    //    }

    //    public async Task<ApplicationUser?> ObterPorIdAsync(int id)
    //    {
    //        return await _context.Usuarios.Include(ApplicationUser => ApplicationUser.ProjetosC)
    //    }



    //}
}
