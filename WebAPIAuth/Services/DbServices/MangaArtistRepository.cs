using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using WebAPIAuth.Models;

namespace WebAPIAuth.Services
{
    public class MangaArtistRepository : IRepository<MangaArtist>
    {
        private ApplicationContext _context;
        public MangaArtistRepository(ApplicationContext context) => _context = context;
        public async Task<IEnumerable<MangaArtist>> GetAllAsync()
        {
            return await _context.MangaArtists.Include(i => i.Mangas).ToListAsync();
        }
        public async Task<MangaArtist> GetAsync(long id)
        {
            return await _context.MangaArtists.Include(i => i.Mangas).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<MangaArtist> CreateAsync(MangaArtist entity)
        {
            await _context.MangaArtists.AddAsync(entity);
            return _context.MangaArtists.Include(i => i.Mangas).FirstOrDefault(i => i.Id == entity.Id);
        }
        public MangaArtist Edit(MangaArtist entity)
        {
            _context.MangaArtists.Update(entity);
            return _context.MangaArtists.Include(i => i.Mangas).FirstOrDefault(i => i.Id == entity.Id);
        }
        public MangaArtist Delete(long id)
        {
            MangaArtist entity = _context.MangaArtists.Include(i => i.Mangas).FirstOrDefault(i => i.Id == id);
            _context.MangaArtists.Remove(entity);
            return entity;
        }
        public async Task<MangaArtist> GetAsync(Expression<Func<MangaArtist, bool>> expression)
        {
            return await _context.MangaArtists.FirstOrDefaultAsync(expression);
        }
    }
}
