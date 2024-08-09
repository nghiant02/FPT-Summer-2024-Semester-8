using AutoMapper;
using BusinessObjects.Dto.BillReqRes;
using BusinessObjects.Dto.Other;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using Repositories.Interface;
using Tools;

namespace Repositories.Implementation;

public class BillDetailRepository : IBillDetailRepository
{
    private readonly IMongoCollection<BillDetailDto> _collection;
    private readonly IMapper _mapper;

    public BillDetailRepository(IMongoClient client, IConfiguration configuration, IMapper mapper)
    {
        var databaseName = configuration.GetSection("MongoDb:DatabaseName:JSSATS").Value;
        var database = client.GetDatabase(databaseName);
        _collection = database.GetCollection<BillDetailDto>("BillDetail");
        _mapper = mapper;
    }

    public async Task AddBillDetail(BillDetailDto billDetail)
    {
        billDetail.Id = Generator.GenerateId();
        await _collection.InsertOneAsync(billDetail);
    }
    
    public async Task<PagingResponse> GetBillDetails(int pageNumber, int pageSize)
    {
        var totalRecord = await _collection.CountDocumentsAsync(FilterDefinition<BillDetailDto>.Empty);
        var totalPage = (int)Math.Ceiling((double)totalRecord / pageSize);
        var result = await _collection.Find(FilterDefinition<BillDetailDto>.Empty)
            .Skip((pageNumber - 1) * pageSize)
            .Limit(pageSize)
            .ToListAsync();
        var response = new PagingResponse
        {
            PageNumber = pageNumber,
            PageSize = pageSize,
            TotalPage = totalPage,
            TotalRecord = (int)totalRecord,
            Data = result
        };
        return response;
    }

    public async Task<BillDetailDto> GetBillDetail(string billId)
    {
        return await _collection.Find(x => x.BillId == billId).FirstOrDefaultAsync();
    }
}