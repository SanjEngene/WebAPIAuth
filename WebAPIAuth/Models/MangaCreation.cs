using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAuth.Models
{
    public class MangaCreation
    {
        public long Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool DidBecomeAnime { get; set; }
        public MangaArtist MangaArtist { get; set; }
        public int MangaArtistId { get; set; }
    }
}
