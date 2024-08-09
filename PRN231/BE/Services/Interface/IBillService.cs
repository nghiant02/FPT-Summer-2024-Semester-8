using BusinessObjects.Dto.BillReqRes;
using BusinessObjects.Dto.Other;

namespace Services.Interface
{
    public interface IBillService
    {
        public Task<BillResponseDto> Create(BillRequestDto entity);
        public Task<PagingResponse> GetBills(int pageNumber, int pageSize);
        public Task<BillDetailDto?> GetById(string id);
        
        public Task<BillCashCheckoutResponseDto> CheckoutBill(string id, float cashAmount);
    }
}
