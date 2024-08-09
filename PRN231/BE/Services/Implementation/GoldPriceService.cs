using System.Globalization;
using AutoMapper;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;
using HtmlAgilityPack;
using Repositories.Interface;
using Services.Interface;
using Tools;

namespace Services.Implementation;

public class GoldPriceService(IGoldPriceRepository goldPriceRepository,IMapper mapper) : IGoldPriceService
{
    public IGoldPriceRepository GoldPriceRepository { get; } = goldPriceRepository;
    public IMapper Mapper { get; } = mapper;

    public async Task<IEnumerable<GoldPriceResponseDto>?> GetGoldPrices()
    {
        var handler = new HttpClientHandler()
        {
            ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
        };
        var httpClient = new HttpClient(handler);
        var response = await httpClient.GetAsync("https://sjc.com.vn/xml/tygiavang.xml");
        var html = await response.Content.ReadAsStringAsync();

        var doc = new HtmlDocument();
        doc.LoadHtml(html);

        var updateTime = doc.DocumentNode.SelectSingleNode("//ratelist")?.Attributes["updated"]?.Value;

        var cities = doc.DocumentNode.SelectNodes("//ratelist/city");

        // Retrieve existing gold prices
        var existingGoldPrices = await GoldPriceRepository.Gets();
        foreach (var cityNode in cities)
        {
            var city = cityNode.Attributes["name"]?.Value;
            var items = cityNode.SelectNodes("item");

            if (items == null) continue;
            foreach (var itemNode in items)
            {
                var buyPrice = itemNode.Attributes["buy"]?.Value;
                var sellPrice = itemNode.Attributes["sell"]?.Value;
                var type = itemNode.Attributes["type"]?.Value;
                
                var existingGoldPrice = existingGoldPrices.FirstOrDefault(gp => gp.City == city && gp.Type == type);

                if (existingGoldPrice != null)
                {
                    // Update existing gold price
                    existingGoldPrice.BuyPrice = float.Parse(buyPrice);
                    existingGoldPrice.SellPrice = float.Parse(sellPrice);
                    existingGoldPrice.LastUpdated = ParseAndUpdateTime(updateTime) ?? DateTime.UtcNow;
                    await GoldPriceRepository.Update(existingGoldPrice);
                }
                else
                {
                    // Create new gold price
                    var newGoldPrice = new Gold
                    {
                        GoldId = Generator.GenerateId(),
                        City = city,
                        BuyPrice = float.Parse(buyPrice),
                        SellPrice = float.Parse(sellPrice),
                        Type = type,
                        LastUpdated = ParseAndUpdateTime(updateTime) ?? DateTime.UtcNow
                    };
                    await GoldPriceRepository.Create(newGoldPrice);
                }
            }
        }
        var finalGoldPrices = await GoldPriceRepository.Gets();
        return Mapper.Map<IEnumerable<GoldPriceResponseDto>>(finalGoldPrices);
    }
    private DateTime? ParseAndUpdateTime(string updateTimeString)
    {
        if (string.IsNullOrEmpty(updateTimeString))
            return null;

        const string format = "hh:mm:ss tt dd/MM/yyyy";
        if (DateTime.TryParseExact(updateTimeString, format, CultureInfo.InvariantCulture, DateTimeStyles.None, out var parsedUpdateTime))
        {
            // Convert parsedUpdateTime to UTC if it's not already in UTC
            if (parsedUpdateTime.Kind != DateTimeKind.Utc)
            {
                parsedUpdateTime = parsedUpdateTime.ToUniversalTime();
            }
            return parsedUpdateTime;
        }
        return null;
    }

}
