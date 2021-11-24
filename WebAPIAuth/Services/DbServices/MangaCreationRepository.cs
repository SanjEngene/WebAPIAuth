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
    public class MangaCreationRepository : IRepository<MangaCreation>
    {
        private ApplicationContext _context;
        public MangaCreationRepository(ApplicationContext context) => _context = context;
        public async Task<IEnumerable<MangaCreation>> GetAllAsync()
        {
            return await _context.MangaCreations.Include(i => i.MangaArtist).ToListAsync();
        }
        public async Task<MangaCreation> GetAsync(long id)
        {
            return await _context.MangaCreations.Include(i => i.MangaArtist).FirstOrDefaultAsync(i => i.Id == id);
        }
        public async Task<MangaCreation> CreateAsync(MangaCreation entity)
        {
            await _context.MangaCreations.AddAsync(entity);
            return _context.MangaCreations.Include(i => i.MangaArtist).FirstOrDefault(i => i.Id == entity.Id);
        }
        public MangaCreation Edit(MangaCreation entity)
        {
            _context.MangaCreations.Update(entity);
            return _context.MangaCreations.Include(i => i.MangaArtist).FirstOrDefault(i => i.Id == entity.Id);
        }
        public MangaCreation Delete(long id)
        {
            MangaCreation entity = _context.MangaCreations.Include(i => i.MangaArtist).FirstOrDefault(i => i.Id == id);
            _context.MangaCreations.Remove(entity);
            return entity;
        }
        public async Task<MangaCreation> GetAsync(Expression<Func<MangaCreation, bool>> expression)
        {
            return await _context.MangaCreations.FirstOrDefaultAsync(expression);
        }
    }
}
