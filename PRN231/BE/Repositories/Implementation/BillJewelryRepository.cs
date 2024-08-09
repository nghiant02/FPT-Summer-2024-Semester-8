using BusinessObjects.Dto.Dashboard;
using BusinessObjects.Models;
using DAO;
using DAO.Dao;
using Repositories.Interface;
using Repositories.Interface.GenericRepository;

namespace Repositories.Implementation;

public class BillJewelryRepository(BillJewelryDao billJewelryDao) : IBillJewelryRepository
{
    public BillJewelryDao BillJewelryDao { get; } = billJewelryDao;

    public async Task<int> Create(BillJewelry entity)
    {
        return await BillJewelryDao.CreateBillJewelry(entity);
    }

    public async Task<IEnumerable<BestSellingProductDto>> GetBestSellingProducts()
    {
        return await BillJewelryDao.GetBestSellingProducts();
    }

    public async Task<IEnumerable<BestSellingProductTypeDto>> GetBestSellingProductTypes()
    {
        return await BillJewelryDao.GetBestSellingProductTypes();
    }

    public async Task<IEnumerable<ProductRevenueDto>> GetTotalRevenueByProducts()
    {
        return await BillJewelryDao.GetTotalRevenueByProducts();
    }

    public async Task<IEnumerable<ProductTypeRevenueDto>> GetTotalRevenueByProductTypes()
    {
        return await BillJewelryDao.GetTotalRevenueByProductTypes();
    }

    public async Task<decimal> GetTotalRevenueByMonth(int month, int year)
    {
        return await BillJewelryDao.GetTotalRevenueByMonth(month, year);
    }
}