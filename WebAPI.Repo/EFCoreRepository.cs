using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebAPI.Dominio;

namespace WebAPI.Repo
{
    public class EFCoreRepository : IEFCoreRepository
    {
        private readonly HeroiContexto _contexto;

        public EFCoreRepository(HeroiContexto contexto)
        {
            _contexto = contexto;
        }
        public void Add<T>(T entity) where T : class
        {
            _contexto.Add(entity);
        }
        public void Update<T>(T entity) where T : class
        {
            _contexto.Update(entity);
        }
        public void Delete<T>(T entity) where T : class
        {
            _contexto.Remove(entity);
        }
        public async Task<bool> SaveChangeAsync()
        {
            return (await _contexto.SaveChangesAsync()) > 0;
        }

        public async Task<Heroi[]> GetAllHerois(bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _contexto.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas)
                             .ThenInclude(hb => hb.Batalha);
            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Heroi> GetHeroiById(int id, bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _contexto.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas)
                             .ThenInclude(hb => hb.Batalha);
            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<Heroi[]> GetHeroisByName(string nome, bool incluirBatalha = false)
        {
            IQueryable<Heroi> query = _contexto.Herois
                .Include(h => h.Identidade)
                .Include(h => h.Armas);

            if (incluirBatalha)
            {
                query = query.Include(h => h.HeroisBatalhas)
                             .ThenInclude(hb => hb.Batalha);
            }
            query = query.AsNoTracking()
                         .Where(h => h.Nome.Contains(nome))
                         .OrderBy(h=>h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Batalha[]> GetAllBatalhas(bool incluirHerois = false)
        {
            IQueryable<Batalha> query = _contexto.Batalhas;

            if (incluirHerois)
            {
                query = query.Include(h => h.HeroisBatalhas)
                             .ThenInclude(hb => hb.Heroi);
            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.ToArrayAsync();
        }

        public async Task<Batalha> GetBatalhaById(int id, bool incluirHerois = false)
        {
            IQueryable<Batalha> query = _contexto.Batalhas;

            if (incluirHerois)
            {
                query = query.Include(h => h.HeroisBatalhas)
                             .ThenInclude(hb => hb.Heroi);
            }
            query = query.AsNoTracking().OrderBy(h => h.Id);

            return await query.FirstOrDefaultAsync();
        }
    }
}
