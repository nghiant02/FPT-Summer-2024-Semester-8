using AutoMapper;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Repositories.Interface;
using Services.Implementation;
using Services.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class GemPriceServiceTests
    {
        private Mock<IMapper> _mapperMock;
        private Mock<IGemPriceRepository> _gemPriceRepositoryMock;
        private IGemPriceService _gemPriceService;

        [TestInitialize]
        public void Setup()
        {
            _mapperMock = new Mock<IMapper>();
            _gemPriceRepositoryMock = new Mock<IGemPriceRepository>();
            _gemPriceService = new GemPriceService(_mapperMock.Object, _gemPriceRepositoryMock.Object);
        }

        [TestMethod]
        public async Task GetGemPrices_ShouldReturnMappedGemPrices()
        {
            // Arrange
            var gemPrices = new List<Gem> { new Gem { GemId = "1", SellPrice = 100 } };
            var gemPriceResponseDtos = new List<GemPriceResponseDto> { new GemPriceResponseDto { GemId = "1", SellPrice = 100 } };

            _gemPriceRepositoryMock.Setup(repo => repo.Gets()).ReturnsAsync(gemPrices);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<GemPriceResponseDto>>(gemPrices)).Returns(gemPriceResponseDtos);

            // Act
            var result = await _gemPriceService.GetGemPrices();

            // Assert
            Assert.AreEqual(gemPriceResponseDtos.Count, result.Count());
            Assert.AreEqual(gemPriceResponseDtos.First().GemId, result.First().GemId);
            Assert.AreEqual(gemPriceResponseDtos.First().SellPrice, result.First().SellPrice);
        }

        [TestMethod]
        public async Task GetGemPrices_ShouldReturnEmptyList()
        {
            // Arrange
            var gemPrices = new List<Gem>();
            var gemPriceResponseDtos = new List<GemPriceResponseDto>();

            _gemPriceRepositoryMock.Setup(repo => repo.Gets()).ReturnsAsync(gemPrices);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<GemPriceResponseDto>>(gemPrices)).Returns(gemPriceResponseDtos);

            // Act
            var result = await _gemPriceService.GetGemPrices();

            // Assert
            Assert.AreEqual(0, result.Count());
        }

        [TestMethod]
        public async Task GetGemPrices_ShouldHandleRepositoryException()
        {
            // Arrange
            _gemPriceRepositoryMock.Setup(repo => repo.Gets()).ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _gemPriceService.GetGemPrices());
        }
    }
}
