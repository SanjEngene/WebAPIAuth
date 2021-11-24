using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using WebAPIAuth.ClientModels;
using WebAPIAuth.Models;
using WebAPIAuth.Services;

namespace WebAPIAuth.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MangaArtistController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        public MangaArtistController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }
        
        public async Task<ActionResult<IEnumerable<MangaArtist>>> Get()
        {
            return Ok(await _unitOfWork.MangaArtists.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MangaArtist>> Get(int id)
        {
            MangaArtist mangaArtist = await _unitOfWork.MangaArtists.GetAsync(id);
            if (mangaArtist != null)
            {
                return Ok(mangaArtist);
            }

            return NotFound(mangaArtist);
        }
        
        [HttpPost]
        public async Task<ActionResult<MangaArtist>> Create(MangaArtistPostModel model)
        {
            if (ModelState.IsValid)
            {
                MangaArtist mangaArtist = await _unitOfWork.MangaArtists.CreateAsync(_mapper.Map<MangaArtist>(model));
                await _unitOfWork.SaveAsync();

                return Ok(mangaArtist);
            }
            return BadRequest(model);
        }

        [HttpPut]
        public async Task<ActionResult<MangaArtist>> Edit(MangaArtistPutModel model)
        {
            if (ModelState.IsValid)
            {
                MangaArtist mangaArtist = await _unitOfWork.MangaArtists.GetAsync(i => i.Id == model.Id);
                if (mangaArtist == null)
                    return NotFound(mangaArtist);
                mangaArtist = _mapper.Map<MangaArtist>(model);
                _unitOfWork.MangaArtists.Edit(mangaArtist);
                await _unitOfWork.SaveAsync();

                return Ok(mangaArtist);
            }
            return BadRequest(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MangaArtist>> Delete(int id)
        {
            MangaArtist mangaArtist = await _unitOfWork.MangaArtists.GetAsync(i => i.Id == id);
            if (mangaArtist == null)
                return NotFound(mangaArtist);

            _unitOfWork.MangaArtists.Delete(id);
            await _unitOfWork.SaveAsync();
            return Ok(mangaArtist);
        }
    }
}
