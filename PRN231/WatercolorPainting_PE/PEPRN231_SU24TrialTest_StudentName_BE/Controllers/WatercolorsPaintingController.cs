using BusinessObject.Dto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Repository;

namespace PEPRN231_SU24TrialTest_StudentName_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class WatercolorsPaintingController : ControllerBase
    {
        private readonly IWatercolorsPaintingRepository _watercolorsPaintingRepository;

        public WatercolorsPaintingController(IWatercolorsPaintingRepository watercolorsPaintingRepository)
        {
            _watercolorsPaintingRepository = watercolorsPaintingRepository;
        }

        [HttpGet]
        [Authorize(Roles = "2,3")]
        [EnableQuery]
        public async Task<IActionResult> Get()
        {
            var result = await _watercolorsPaintingRepository.GetWatercolorsPaintings();
            return Ok(result.AsQueryable());
        }
        
        [HttpGet("{id}")]
        [Authorize(Roles = "2,3")]
        public async Task<IActionResult> Get(string id)
        {
            var result = await _watercolorsPaintingRepository.GetWatercolorsPaintingId(id);
            return Ok(result);
        }
        
        [HttpPost]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Post(WatercolorsPaintingDto watercolorsPaintingDto)
        {
            if (watercolorsPaintingDto.PublishYear < 1000 )
            {
                   return BadRequest("Publish year must be greater than 1000");
            }
            if (watercolorsPaintingDto.Price < 0)
            {
                return BadRequest("Price must be greater than 0");
            }
            var result = await _watercolorsPaintingRepository.AddWatercolorsPainting(watercolorsPaintingDto);
            return Ok(result);
        }
        

        [HttpPut("{id}")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Put(string id, WatercolorsPaintingDto watercolorsPaintingDto)
        {
            var result = await _watercolorsPaintingRepository.UpdateWatercolorsPainting(id, watercolorsPaintingDto);
            return Ok(result);
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "3")]
        public async Task<IActionResult> Delete(string id)
        {
            var result = await _watercolorsPaintingRepository.DeleteWatercolorsPainting(id);
            return Ok(result);
        }
    }
}
