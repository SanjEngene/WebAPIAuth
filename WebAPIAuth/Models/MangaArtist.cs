using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAuth.Models
{
    public class MangaArtist
    {
        public long Id { get; set; }
        [MaxLength(50)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public List<MangaCreation> Mangas { get; set; } = new List<MangaCreation>();

    }
}
