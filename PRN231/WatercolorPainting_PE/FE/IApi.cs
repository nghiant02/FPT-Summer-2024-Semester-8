using Refit;
using BusinessObject.Dto;
namespace FE
{
    public interface IApi
    {
        [Post("/api/UserAccount/LoginToken")]
        Task<string> LoginToken ([Body] LoginDto loginDto);
        [Get("/api/WatercolorsPainting")]
        Task<IEnumerable<WatercolorsPaintingResponseDto>> GetWatercolorPaintings();
    }
}
