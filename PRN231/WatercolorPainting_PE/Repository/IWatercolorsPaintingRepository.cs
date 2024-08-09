using BusinessObject.Dto;

namespace Repository
{
    public interface IWatercolorsPaintingRepository
    {
        Task<IEnumerable<WatercolorsPaintingResponseDto>> GetWatercolorsPaintings();
        Task<WatercolorsPaintingResponseDto> GetWatercolorsPaintingId(string id);
        Task<bool> AddWatercolorsPainting(WatercolorsPaintingDto watercolorsPaintingDto);
        Task<bool> UpdateWatercolorsPainting(string id, WatercolorsPaintingDto watercolorsPaintingDto);
        Task<bool> DeleteWatercolorsPainting(string id);
    }
}
