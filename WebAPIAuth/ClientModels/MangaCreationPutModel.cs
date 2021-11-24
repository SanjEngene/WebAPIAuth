using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAuth.ClientModels
{
    public class MangaCreationPutModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(50)]
        public string Name { get; set; }
        [Required]
        public DateTime CreatedAt { get; set; }
        [Required]
        public int MangaArtistId { get; set; }
        [Required]
        [StringLength(3)]
        public string DidBecomeAnime { get; set; }
    }
}
