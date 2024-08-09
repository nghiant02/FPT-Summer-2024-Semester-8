using AutoMapper;
using BusinessObjects.Dto;
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
    public class WarrantyServiceTests
    {
        private Mock<IWarrantyRepository>? _warrantyRepositoryMock;
        private Mock<IMapper>? _mapperMock;
        private IWarrantyService? _warrantyService;

        [TestInitialize]
        public void Setup()
        {
            _warrantyRepositoryMock = new Mock<IWarrantyRepository>();
            _mapperMock = new Mock<IMapper>();
            _warrantyService = new WarrantyService(
                _warrantyRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [TestMethod]
        public async Task CreateWarranty_ShouldCreateWarranty()
        {
            // Arrange
            var warrantyDto = new WarrantyDto { Description = "Test Warranty" };
            var warranty = new Warranty { WarrantyId = "1", Description = "Test Warranty" };

            _mapperMock!.Setup(mapper => mapper.Map<Warranty>(warrantyDto)).Returns(warranty);
            _warrantyRepositoryMock!.Setup(repo => repo.Create(warranty)).ReturnsAsync(1);

            // Act
            var result = await _warrantyService!.CreateWarranty(warrantyDto);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task GetWarranties_ShouldReturnWarranties()
        {
            // Arrange
            var warranties = new List<Warranty> { new Warranty { WarrantyId = "1", Description = "Test Warranty" } };
            _warrantyRepositoryMock!.Setup(repo => repo.Gets()).ReturnsAsync(warranties);

            // Act
            var result = await _warrantyService!.GetWarranties();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(warranties.Count, result.Count());
            Assert.AreEqual(warranties[0].WarrantyId, result.First()?.WarrantyId);
            Assert.AreEqual(warranties[0].Description, result.First()?.Description);
        }

        [TestMethod]
        public async Task GetWarrantyById_ShouldReturnWarranty()
        {
            // Arrange
            var warrantyId = "1";
            var warranty = new Warranty { WarrantyId = "1", Description = "Test Warranty" };
            _warrantyRepositoryMock!.Setup(repo => repo.GetById(warrantyId)).ReturnsAsync(warranty);

            // Act
            var result = await _warrantyService!.GetWarrantyById(warrantyId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(warranty.WarrantyId, result.WarrantyId);
            Assert.AreEqual(warranty.Description, result.Description);
        }

        [TestMethod]
        public async Task UpdateWarranty_ShouldUpdateWarranty()
        {
            // Arrange
            var warrantyId = "1";
            var warrantyDto = new WarrantyDto { Description = "Updated Warranty" };
            var warranty = new Warranty { WarrantyId = "1", Description = "Updated Warranty" };

            _mapperMock!.Setup(mapper => mapper.Map<Warranty>(warrantyDto)).Returns(warranty);
            _warrantyRepositoryMock!.Setup(repo => repo.Update(warrantyId, warranty)).ReturnsAsync(1);

            // Act
            var result = await _warrantyService!.UpdateWarranty(warrantyId, warrantyDto);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
