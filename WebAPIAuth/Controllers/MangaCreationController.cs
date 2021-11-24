using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebAPIAuth.ClientModels;
using WebAPIAuth.Models;
using WebAPIAuth.Services;

namespace WebAPIAuth.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class MangaCreationController : ControllerBase
    {
        private UnitOfWork _unitOfWork;
        private IMapper _mapper;
        public MangaCreationController(UnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<ActionResult<IEnumerable<MangaCreation>>> Get()
        {
            return Ok(await _unitOfWork.MangaCreations.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<MangaArtist>> Get(int id)
        {
            MangaCreation mangaCreation = await _unitOfWork.MangaCreations.GetAsync(id);
            if (mangaCreation != null)
            {
                return Ok(mangaCreation);
            }

            return NotFound(mangaCreation);
        }

        [HttpPost]
        public async Task<ActionResult<MangaArtist>> Create(MangaCreationPostModel model)
        {
            if (ModelState.IsValid)
            {
                MangaCreation mangaCreation = await _unitOfWork.MangaCreations.CreateAsync(_mapper.Map<MangaCreation>(model));
                await _unitOfWork.SaveAsync();

                return Ok(mangaCreation);
            }
            return BadRequest(model);
        }

        [HttpPut]
        public async Task<ActionResult<MangaArtist>> Edit(MangaCreationPutModel model)
        {
            if (ModelState.IsValid)
            {
                MangaCreation mangaCreation = await _unitOfWork.MangaCreations.GetAsync(i => i.Id == model.Id);
                if (mangaCreation == null)
                    return NotFound(mangaCreation);
                mangaCreation = _mapper.Map<MangaCreation>(model);
                _unitOfWork.MangaCreations.Edit(mangaCreation);
                await _unitOfWork.SaveAsync();

                return Ok(mangaCreation);
            }
            return BadRequest(model);
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult<MangaArtist>> Delete(int id)
        {
            MangaCreation mangaCreation = await _unitOfWork.MangaCreations.GetAsync(i => i.Id == id);
            if (mangaCreation == null)
                return NotFound(mangaCreation);

            _unitOfWork.MangaCreations.Delete(id);
            await _unitOfWork.SaveAsync();
            return Ok(mangaCreation);
        }
    }
}
