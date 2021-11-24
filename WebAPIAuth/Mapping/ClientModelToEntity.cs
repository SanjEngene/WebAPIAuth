using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using WebAPIAuth.ClientModels;
using WebAPIAuth.Models;

namespace WebAPIAuth.Mapping
{
    public class ClientModelToEntity : Profile
    {
        public ClientModelToEntity()
        {
            CreateMap<MangaArtistPostModel, MangaArtist>()
                .ForMember(artist => artist.Name, opt =>
                    opt.MapFrom(model => model.FirstName + " " + model.LastName))
                .ForMember(artist => artist.BirthDate, opt =>
                    opt.MapFrom(model => (DateTime)(model.BirthdayDate)));

            CreateMap<MangaArtistPutModel, MangaArtist>()
                .ForMember(artist => artist.Name, opt => 
                    opt.MapFrom(model => model.FirstName + " " + model.LastName))
                .ForMember(artist => artist.BirthDate, opt => 
                    opt.MapFrom(model => (DateTime)(model.BirthdayDate)));

            CreateMap<MangaCreationPostModel, MangaCreation>()
                .ForMember(manga => manga.DidBecomeAnime, opt => 
                    opt.MapFrom(model => model.DidBecomeAnime.ToBoolean()));

            CreateMap<MangaCreationPutModel, MangaCreation>()
                .ForMember(manga => manga.DidBecomeAnime, opt => 
                    opt.MapFrom(model => model.DidBecomeAnime.ToBoolean()));
        }
    }
}
