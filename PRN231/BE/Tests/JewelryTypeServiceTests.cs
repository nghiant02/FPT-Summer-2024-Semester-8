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
    public class JewelryTypeServiceTests
    {
        private Mock<IJewelryTypeRepository> _repositoryMock;
        private IJewelryTypeService _jewelryTypeService;

        [TestInitialize]
        public void Setup()
        {
            _repositoryMock = new Mock<IJewelryTypeRepository>();
            _jewelryTypeService = new JewelryTypeService(_repositoryMock.Object);
        }

        [TestMethod]
        public async Task GetJewelry_ShouldReturnJewelryTypes()
        {
            // Arrange
            var jewelryTypes = new List<JewelryType> { new JewelryType { JewelryTypeId = "1", Name = "Ring" } };
            _repositoryMock.Setup(repo => repo.Gets()).ReturnsAsync(jewelryTypes);

            // Act
            var result = await _jewelryTypeService.GetJewelry();

            // Assert
            Assert.AreEqual(jewelryTypes.Count, result.Count());
            Assert.AreEqual(jewelryTypes[0].JewelryTypeId, result.First().JewelryTypeId);
            Assert.AreEqual(jewelryTypes[0].Name, result.First().Name);
        }

        [TestMethod]
        public async Task GetJewelryById_ShouldReturnJewelryType()
        {
            // Arrange
            var jewelryType = new JewelryType { JewelryTypeId = "1", Name = "Ring" };
            _repositoryMock.Setup(repo => repo.GetById("1")).ReturnsAsync(jewelryType);

            // Act
            var result = await _jewelryTypeService.GetJewelryById("1");

            // Assert
            Assert.AreEqual(jewelryType.JewelryTypeId, result.JewelryTypeId);
            Assert.AreEqual(jewelryType.Name, result.Name);
        }

        [TestMethod]
        public async Task CreateJewelry_ShouldReturnCreatedId()
        {
            // Arrange
            var jewelryType = new JewelryType { JewelryTypeId = "1", Name = "Ring" };
            _repositoryMock.Setup(repo => repo.Create(jewelryType)).ReturnsAsync(1);

            // Act
            var result = await _jewelryTypeService.CreateJewelry(jewelryType);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task UpdateJewelry_ShouldReturnUpdatedCount()
        {
            // Arrange
            var jewelryType = new JewelryType { JewelryTypeId = "1", Name = "Ring" };
            _repositoryMock.Setup(repo => repo.Update("1", jewelryType)).ReturnsAsync(1);

            // Act
            var result = await _jewelryTypeService.UpdateJewelry("1", jewelryType);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task GetJewelry_ShouldHandleRepositoryException()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.Gets()).ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _jewelryTypeService.GetJewelry());
        }

        [TestMethod]
        public async Task GetJewelryById_ShouldHandleRepositoryException()
        {
            // Arrange
            _repositoryMock.Setup(repo => repo.GetById("1")).ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _jewelryTypeService.GetJewelryById("1"));
        }

        [TestMethod]
        public async Task CreateJewelry_ShouldHandleRepositoryException()
        {
            // Arrange
            var jewelryType = new JewelryType { JewelryTypeId = "1", Name = "Ring" };
            _repositoryMock.Setup(repo => repo.Create(jewelryType)).ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _jewelryTypeService.CreateJewelry(jewelryType));
        }

        [TestMethod]
        public async Task UpdateJewelry_ShouldHandleRepositoryException()
        {
            // Arrange
            var jewelryType = new JewelryType { JewelryTypeId = "1", Name = "Ring" };
            _repositoryMock.Setup(repo => repo.Update("1", jewelryType)).ThrowsAsync(new Exception("Repository error"));

            // Act & Assert
            await Assert.ThrowsExceptionAsync<Exception>(() => _jewelryTypeService.UpdateJewelry("1", jewelryType));
        }
    }
}
