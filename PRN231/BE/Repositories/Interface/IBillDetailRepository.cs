using BusinessObjects.Dto.BillReqRes;
using BusinessObjects.Dto.Other;

namespace Repositories.Interface;

public interface IBillDetailRepository
{
    Task AddBillDetail(BillDetailDto billDetail);
    Task<PagingResponse> GetBillDetails(int pageNumber, int pageSize);
    Task<BillDetailDto> GetBillDetail(string billId);
}