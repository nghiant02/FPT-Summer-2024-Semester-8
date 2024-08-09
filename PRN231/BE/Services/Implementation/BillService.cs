using AutoMapper;
using BusinessObjects.Dto.BillReqRes;
using BusinessObjects.Dto.Other;
using BusinessObjects.Models;
using Repositories.Interface;
using Services.Interface;
using Tools;

namespace Services.Implementation;

public class BillService(
    IMapper mapper,
    IBillRepository billRepository,
    IBillPromotionRepository billPromotionRepository,
    IBillJewelryRepository billJewelryRepository,
    IPromotionRepository promotionRepository,
    IBillDetailRepository billDetailRepository,
    ICustomerRepository customerRepository,
    IUserRepository userRepository,
    IPurchaseRepository purchaseRepository,
    IJewelryRepository jewelryRepository) : IBillService
{
    private IMapper Mapper { get; } = mapper;
    private IBillRepository BillRepository { get; } = billRepository;
    private IBillPromotionRepository BillPromotionRepository { get; } = billPromotionRepository;
    private IBillJewelryRepository BillJewelryRepository { get; } = billJewelryRepository;
    private IPromotionRepository PromotionRepository { get; } = promotionRepository;
    private IBillDetailRepository BillDetailRepository { get; } = billDetailRepository;
    private ICustomerRepository CustomerRepository { get; } = customerRepository;
    private IUserRepository UserRepository { get; } = userRepository;
    private IJewelryRepository JewelryRepository { get; } = jewelryRepository;
    private IPurchaseRepository PurchaseRepository { get; } = purchaseRepository;

    public async Task<BillResponseDto> Create(BillRequestDto billRequestDto)
    {
        double totalAmount = 0;
        double totalDiscountRate = 0;
        double finalAmount = 0;

        // Create bill

        var bill = new Bill
        {
            BillId = Generator.GenerateId(),
            CustomerId = billRequestDto.CustomerId,
            CounterId = billRequestDto.CounterId,
            UserId = billRequestDto.UserId,
            SaleDate = DateTime.Now.ToUniversalTime(),
            CreatedAt = DateTime.UtcNow,
        };

        var billId = await BillRepository.CreateBill(bill);

        // Check if bill is created
        if (billId == null)
        {
            throw new InvalidOperationException("Failed to create the bill.");
        }

        // Add bill items
        foreach (var item in billRequestDto.Jewelries)
        {
            var billJewelry = new BillJewelry
            {
                BillJewelryId = Generator.GenerateId(),
                BillId = billId,
                JewelryId = item.JewelryId,
            };
            await BillJewelryRepository.Create(billJewelry);
        }

        // Add bill promotions
        foreach (var promotion in billRequestDto.Promotions)
        {
            if (promotion.PromotionId == null) continue;
            var billPromotion = new BillPromotion
            {
                BillPromotionId = Generator.GenerateId(),
                BillId = billId,
                PromotionId = promotion.PromotionId,
            };
            await BillPromotionRepository.Create(billPromotion);
        }

        // Generate response
        var items = new List<BillItemResponse>();
        foreach (var i in billRequestDto.Jewelries)
        {
            var jewelryPrice = await JewelryRepository.GetById(i.JewelryId);
            var billItemResponse = new BillItemResponse
            {
                JewelryId = i.JewelryId,
                Name = jewelryPrice?.Name,
                LaborCost = jewelryPrice.LaborCost,
                JewelryPrice = jewelryPrice.JewelryPrice,
                TotalPrice = jewelryPrice.TotalPrice,
            };
            items.Add(billItemResponse);
        }

        var promotions = new List<BillPromotionResponse>();
        foreach (var p in billRequestDto.Promotions)
        {
            var promotionDiscountRate = await PromotionRepository.GetById(p.PromotionId);
            var billPromotionResponse = new BillPromotionResponse
            {
                PromotionId = p.PromotionId,
                Discount = (float)promotionDiscountRate.DiscountRate
            };
            promotions.Add(billPromotionResponse);
        }

        foreach (var totalItemPrice in items)
        {
            totalAmount += totalItemPrice.TotalPrice;
        }

        foreach (var promotion in billRequestDto.Promotions)
        {
            var promotionDiscount = await PromotionRepository.GetById(promotion.PromotionId);
            totalDiscountRate += (double)promotionDiscount.DiscountRate;
        }

        bill.TotalAmount = CalculateFinalAmount(totalAmount, totalDiscountRate);
        await BillRepository.UpdateBill(bill);

        var billResponseDto = new BillResponseDto
        {
            BillId = billId,
            CustomerName = CustomerRepository.GetById(billRequestDto.CustomerId).Result?.FullName,
            CounterId = billRequestDto.CounterId,
            StaffName = UserRepository.GetById(billRequestDto.UserId).Result?.Username,
            TotalAmount = totalAmount,
            TotalDiscount = totalDiscountRate,
            SaleDate = bill.SaleDate,
            Items = items,
            Promotions = promotions,
            AdditionalDiscount = billRequestDto.AdditionalDiscount,
            PointsUsed = 0, // Calculate points used
            FinalAmount = CalculateFinalAmount(totalAmount, (float)totalDiscountRate)
        };
        await BillDetailRepository.AddBillDetail(Mapper.Map<BillDetailDto>(billResponseDto));
        return billResponseDto;
    }

    public async Task<PagingResponse> GetBills(int pageNumber, int pageSize)
    {
        return await BillDetailRepository.GetBillDetails(pageNumber, pageSize);
    }

    public async Task<BillDetailDto?> GetById(string id)
    {
        return await BillDetailRepository.GetBillDetail(id);
    }

    public async Task<BillCashCheckoutResponseDto> CheckoutBill(string id, float cashAmount)
    {
        var bill = await BillRepository.GetById(id);
        if (bill == null) throw new InvalidOperationException("Bill not found");
        if (bill.IsPaid) throw new InvalidOperationException("Bill is already paid.");
        bill.IsPaid = true;
        await BillRepository.UpdateBill(bill);

        var billDetail = await BillDetailRepository.GetBillDetail(id);

        foreach (var item in billDetail.Items)
        {
            var jewelry = await JewelryRepository.GetJewelryById(item.JewelryId);
            if (jewelry == null)
            {
                throw new InvalidOperationException("Jewelry not found.");
            }

            jewelry.IsSold = true;
            await JewelryRepository.Update(item.JewelryId, jewelry);
        }

        if (billDetail.FinalAmount > cashAmount)
        {
            throw new InvalidOperationException(
                $"Cash amount is not enough, It must be greater than {billDetail.FinalAmount}VND .");
        }

        var cashBack = cashAmount - (float)billDetail.FinalAmount;
        var finalAmount = cashAmount - cashBack;
        var billCheckoutResponse = new BillCashCheckoutResponseDto
        {
            BillId = billDetail.BillId,
            CustomerName = billDetail.CustomerName,
            InitialAmount = cashAmount,
            CashBack = cashBack,
            FinalAmount = finalAmount,
            CreatedAt = DateTime.UtcNow.ToUniversalTime(),
            Status = "Success"
        };
        // Purchase
        foreach (var item in billDetail.Items)
        {
            var purchase = new Purchase
            {
                PurchaseId = Generator.GenerateId(),
                BillId = billDetail.BillId,
                IsBuyBack = 0,
                CustomerId = await CustomerRepository.GetCustomerIdByName(billDetail.CustomerName),
                JewelryId = item.JewelryId,
                PurchaseDate = DateTime.UtcNow.ToUniversalTime(),
                PurchasePrice = item.TotalPrice,
                UserId = await UserRepository.GetUserIdByName(billDetail.StaffName)

            };
            await PurchaseRepository.CreatePurchase(purchase);
         }
        return billCheckoutResponse;
    }

    private static double CalculateFinalAmount(double totalAmount, double discountRate)
    {
        return totalAmount - (totalAmount * (discountRate / 100));
    }
}