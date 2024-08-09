using BusinessObjects.Dto.ResponseDto;

namespace Services.Interface;

public interface IGoldPriceService
{
    Task<IEnumerable<GoldPriceResponseDto>?> GetGoldPrices();
}