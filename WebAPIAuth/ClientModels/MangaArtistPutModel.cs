using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace WebAPIAuth.ClientModels
{
    public class MangaArtistPutModel
    {
        [Required]
        public long Id { get; set; }
        [Required]
        [StringLength(45)]
        public string FirstName { get; set; }
        [Required]
        [StringLength(45)]
        public string LastName { get; set; }
        [Required]
        public StringDate BirthdayDate { get; set; }
    }
}
