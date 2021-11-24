using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAuth.Models;

namespace WebAPIAuth.Services
{
    public class UnitOfWork
    {
        private IRepository<MangaArtist> _mangaArtistRepository;
        private IRepository<MangaCreation> _mangaCreationRepository;
        private ApplicationContext _context;
        public UnitOfWork(ApplicationContext context)
        {
            _context = context;
        }
        public IRepository<MangaArtist> MangaArtists
        {
            get
            {
                if (_mangaArtistRepository == null)
                    _mangaArtistRepository = new MangaArtistRepository(_context);
                return _mangaArtistRepository;
            }
        }
        public IRepository<MangaCreation> MangaCreations
        {
            get
            {
                if (_mangaCreationRepository == null)
                    _mangaCreationRepository = new MangaCreationRepository(_context);
                return _mangaCreationRepository;
            }
        }
        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
