using AutoMapper;
using BusinessObject.Dto.RequestDto;
using BusinessObject.Dto.ResponseDto;
using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Repository.Interface;

namespace API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PublisherController : ControllerBase
{
    private readonly IPublisherRepository? _publisherRepository;
    private readonly IMapper _mapper;
    public PublisherController(IPublisherRepository? publisherRepository, IMapper mapper)
    {
        _publisherRepository = publisherRepository;
        _mapper = mapper;
    }
    [HttpGet("GetPublishers")]
    public async Task<IActionResult> GetPublishers()
    {
        if (_publisherRepository == null) return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        var publishers = await _publisherRepository.Gets();
        var response = _mapper.Map<IEnumerable<PublisherResponseDto>>(publishers);
        if (publishers == null) return NotFound();
        return Ok(response);
    }
    [HttpGet("GetPublisherById/{publisherId}")]
    public async Task<IActionResult> GetPublisherById(string publisherId)
    {
        if (_publisherRepository == null) return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        var publisher = await _publisherRepository.GetById(publisherId);
        var response = _mapper.Map<PublisherResponseDto>(publisher);
        if (publisher == null) return NotFound();
        return Ok(response);
    }
    [HttpPost("CreatePublisher")]
    public async Task<IActionResult> CreatePublisher(PublisherRequestDto publisher)
    {
        if (_publisherRepository == null) return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        var publisherEntity = _mapper.Map<Publisher>(publisher);
        var result = await _publisherRepository.Create(publisherEntity);
        var response = _mapper.Map<PublisherResponseDto>(result);
        if (result == null) return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        return Ok(response);
    }
    [HttpPut("UpdatePublisher/{publisherId}")]
    public async Task<IActionResult> UpdatePublisher(string publisherId, PublisherRequestDto publisher)
    {
        var publisherEntity = _mapper.Map<Publisher>(publisher);
        var result = await _publisherRepository.Update(publisherId, publisherEntity);
        return Ok(result);
    }
    [HttpDelete("DeletePublisher/{publisherId}")]
    public async Task<IActionResult> DeletePublisher(string publisherId)
    {
        if (_publisherRepository == null) return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        var result = await _publisherRepository.Delete(publisherId);
        if (result == 0) return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
        return Ok(result!=0);
    }
}
