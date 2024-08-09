using BusinessObjects.Models;
using DAO;
using DAO.Dao;
using Repositories.Interface;
using Tools;

namespace Repositories.Implementation;

public class JewelryTypeRepository(JewelryTypeDao jewelryTypeDao) : IJewelryTypeRepository
{
    public JewelryTypeDao JewelryTypeDao { get; } = jewelryTypeDao;

    public async Task<IEnumerable<JewelryType?>?> Gets()
    {
        return await JewelryTypeDao.GetJewelryTypes();
    }

    public async Task<JewelryType?> GetById(string id)
    {
        return await JewelryTypeDao.GetJewelryTypeById(id);
    }

    public async Task<int> Create(JewelryType entity)
    {
        entity.JewelryTypeId = Generator.GenerateId();
        return await JewelryTypeDao.CreateJewelryType(entity);
    }

    public async Task<int> Update(string id, JewelryType entity)
    {
        return await JewelryTypeDao.UpdateJewelryType(id, entity);
    }
}