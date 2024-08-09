using BusinessObjects.Dto.BuyBack;

namespace Services.Interface
{
    public interface IPurchaseService
    {
        Task<ProcessBuybackByIdResponse> ProcessBuybackById(string jewelryId);
        Task<ProcessBuybackByNameResponse> ProcessBuybackByName(BuybackByNameRequest request);
        Task<CountProcessBuybackByNameResponse> CountProcessBuybackByName(CountBuybackByNameRequest request);
        Task<CountProcessBuybackByIdResponse> CountProcessBuybackById(string jewelryId);
    }
}
