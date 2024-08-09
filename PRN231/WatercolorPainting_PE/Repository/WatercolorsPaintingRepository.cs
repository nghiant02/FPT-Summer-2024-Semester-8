using BusinessObject.Dto;
using BusinessObject.Models;
using Dao;

namespace Repository
{
    public class WatercolorsPaintingRepository : IWatercolorsPaintingRepository
    {
        private readonly WatercolorsPaintingDao _watercolorsPaintingDao;
        public WatercolorsPaintingRepository(WatercolorsPaintingDao watercolorsPaintingDao)
        {
            _watercolorsPaintingDao = watercolorsPaintingDao;
        }

        public async Task<bool> AddWatercolorsPainting(WatercolorsPaintingDto watercolorsPaintingDto)
        {
            var result = await _watercolorsPaintingDao.AddWatercolorsPainting(new WatercolorsPainting
            {
                PaintingName = watercolorsPaintingDto.PaintingName,
                PaintingDescription = watercolorsPaintingDto.PaintingDescription,
                PaintingAuthor = watercolorsPaintingDto.PaintingAuthor,
                Price = watercolorsPaintingDto.Price,
                PublishYear = watercolorsPaintingDto.PublishYear,
                StyleId = watercolorsPaintingDto.StyleId
            });
            return result;
        }

        public Task<bool> DeleteWatercolorsPainting(string id)
        {
            var result = _watercolorsPaintingDao.DeleteWatercolorsPainting(id);
            return result;
        }

        public async Task<WatercolorsPaintingResponseDto> GetWatercolorsPaintingId(string id)
        {
            var watercolorsPainting =await _watercolorsPaintingDao.GetWatercolorsPaintingId(id);
            var WatercolorsPaintingResponseDto = new WatercolorsPaintingResponseDto
            {
                PaintingId = watercolorsPainting.PaintingId,
                PaintingName = watercolorsPainting.PaintingName,
                PaintingDescription = watercolorsPainting.PaintingDescription,
                PaintingAuthor = watercolorsPainting.PaintingAuthor,
                Price = watercolorsPainting.Price,
                PublishYear = watercolorsPainting.PublishYear,
                StyleName = watercolorsPainting.Style.StyleName
            };

            return WatercolorsPaintingResponseDto;
        }

        public async Task<IEnumerable<WatercolorsPaintingResponseDto>> GetWatercolorsPaintings()
        {
           var watercolorsPainting = await _watercolorsPaintingDao.GetWatercolorsPaintings();
           
            var watercolorsPaintingResponseDto = watercolorsPainting.Select(p => new WatercolorsPaintingResponseDto
            {
                PaintingId = p.PaintingId,
                PaintingName = p.PaintingName,
                PaintingDescription = p.PaintingDescription,
                PaintingAuthor = p.PaintingAuthor,
                Price = p.Price,
                PublishYear = p.PublishYear,
                StyleName = p.Style.StyleName
            });
            return watercolorsPaintingResponseDto;
        }

        public async Task<bool> UpdateWatercolorsPainting(string id, WatercolorsPaintingDto watercolorsPaintingDto)
        {
            var watercolorsPainting = new WatercolorsPainting
            {
                PaintingName = watercolorsPaintingDto.PaintingName,
                PaintingDescription = watercolorsPaintingDto.PaintingDescription,
                PaintingAuthor = watercolorsPaintingDto.PaintingAuthor,
                Price = watercolorsPaintingDto.Price,
                PublishYear = watercolorsPaintingDto.PublishYear,
                StyleId = watercolorsPaintingDto.StyleId
            };
            return await _watercolorsPaintingDao.UpdateWatercolorsPainting(id, watercolorsPainting);
        }
    }
}
