using BusinessObjects.Models;
using DAO.Dao;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repositories.Implementation
{
    public class PurchaseRepository : IPurchaseRepository
    {
        private readonly PurchaseDao _purchaseDao;

        public PurchaseRepository(PurchaseDao purchaseDao)
        {
            _purchaseDao = purchaseDao;
        }

        public async Task<Purchase> GetPurchaseByJewelryId(string jewelryId)
        {
            return await _purchaseDao.GetPurchaseByJewelryId(jewelryId);
        }

        public async Task<Purchase> GetPurchaseByJewelryIdWithBuyBack0(string jewelryId)
        {
            return await _purchaseDao.GetPurchaseByJewelryIdWithBuyBack0(jewelryId);
        }

        public async Task<Jewelry?> GetJewelryById(string jewelryId)
        {
            return await _purchaseDao.GetJewelryById(jewelryId);
        }

        public async Task<JewelryMaterial?> GetJewelryMaterialByJewelryId(string jewelryId)
        {
            return await _purchaseDao.GetJewelryMaterialByJewelryId(jewelryId);
        }

        public async Task<Gold?> GetGoldPriceById(string goldPriceId)
        {
            return await _purchaseDao.GetGoldPriceById(goldPriceId);
        }

        public async Task<Gem?> GetStonePriceById(string stonePriceId)
        {
            return await _purchaseDao.GetStonePriceById(stonePriceId);
        }

        public async Task CreateJewelry(Jewelry jewelry)
        {
            await _purchaseDao.CreateJewelry(jewelry);
        }

        public async Task CreateJewelryMaterial(JewelryMaterial jewelryMaterial)
        {
            await _purchaseDao.CreateJewelryMaterial(jewelryMaterial);
        }

        public async Task CreatePurchase(Purchase purchase)
        {
            await _purchaseDao.CreatePurchase(purchase);
        }

        public async Task<Purchase?> UpdatePurchase(Purchase purchase)
        {
            return await _purchaseDao.UpdatePurchase(purchase.PurchaseId, purchase);
        }
    }
}
