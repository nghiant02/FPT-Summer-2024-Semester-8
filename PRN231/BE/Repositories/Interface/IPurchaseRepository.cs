using BusinessObjects.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Interface
{
    public interface IPurchaseRepository
    {
        Task<Purchase> GetPurchaseByJewelryId(string jewelryId);
        Task<Jewelry> GetJewelryById(string jewelryId);
        Task<JewelryMaterial> GetJewelryMaterialByJewelryId(string jewelryId);
        Task<Purchase> GetPurchaseByJewelryIdWithBuyBack0(string jewelryId);
        Task<Gold> GetGoldPriceById(string goldPriceId);
        Task<Gem> GetStonePriceById(string stonePriceId);
        Task CreateJewelry(Jewelry jewelry);
        Task CreateJewelryMaterial(JewelryMaterial jewelryMaterial);
        Task CreatePurchase(Purchase purchase);
        Task<Purchase> UpdatePurchase(Purchase purchase);
    }
}
