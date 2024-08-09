using BusinessObjects.Models;

namespace Services.Interface;

public interface IJewelryTypeService
{
    Task<IEnumerable<JewelryType?>?> GetJewelry();
    Task<JewelryType?> GetJewelryById(string id);
    Task<int> CreateJewelry(JewelryType jewelryType);
    Task<int> UpdateJewelry(string id, JewelryType jewelryType);
}