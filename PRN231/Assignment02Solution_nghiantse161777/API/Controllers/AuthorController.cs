using AutoMapper;
using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto.ResponseDto;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthorController : ControllerBase
    {
        private readonly IAuthorRepository? _authorRepository;
        private readonly IMapper? _mapper;
        public AuthorController(IAuthorRepository? authorRepository, IMapper? mapper)
        {
            _authorRepository = authorRepository;
            _mapper = mapper;
        }
        [HttpGet("GetAuthors")]
        public async Task<IActionResult> GetAuthors()
        {
            if (_authorRepository == null || _mapper == null) return BadRequest("Invalid request");
            var authors = await _authorRepository.Gets();
            if(authors == null) return NotFound();
            var authorsResponse = _mapper.Map<IEnumerable<AuthorResponseDto>>(authors);
            return Ok(authorsResponse);
        }
        [HttpGet("GetAuthorById/{authorId}")]
        public async Task<IActionResult> GetAuthorById(string authorId)
        {
            if (_authorRepository == null || _mapper == null) return BadRequest("Invalid request");
            var author = await _authorRepository.GetById(authorId);
            if (author == null) return NotFound();
            var authorResponse = _mapper.Map<AuthorResponseDto>(author);
            return Ok(authorResponse);
        }
        [HttpPost("CreateAuthor")]
        public async Task<IActionResult> AddAuthor(AuthorRequestDto authorRequestDto)
        {
            if (_authorRepository == null || _mapper == null) return BadRequest("Invalid request");
            var author = _mapper.Map<Author>(authorRequestDto);
            var result = await _authorRepository.Create(author);
            var authorResponse = _mapper.Map<AuthorResponseDto>(result);
            return Ok(authorResponse);
        }
        [HttpDelete("DeleteAuthor/{authorId}")]
        public async Task<IActionResult> DeleteAuthor(string authorId)
        {
            if (_authorRepository == null) return BadRequest("Invalid request");
            var result = await _authorRepository.Delete(authorId);
            if (result == 0) return NotFound();
            return Ok("Author deleted successfully");
        }
        [HttpPut("UpdateAuthor/{authorId}")]
        public async Task<IActionResult> UpdateAuthor(string authorId, AuthorUpdateRequestDto authorRequestDto)
        {
            if (_authorRepository == null || _mapper == null) return BadRequest("Invalid request");
            var author = _mapper.Map<Author>(authorRequestDto);
            var result = await _authorRepository.Update(authorId, author);
            if (!result) return NotFound();
            return Ok("Author updated successfully");
        }
    }
}
