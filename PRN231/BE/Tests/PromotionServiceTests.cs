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
    public class PromotionServiceTests
    {
        private Mock<IPromotionRepository>? _promotionRepositoryMock;
        private Mock<IUserRepository>? _userRepositoryMock;
        private Mock<IRoleRepository>? _roleRepositoryMock;
        private Mock<IMapper>? _mapperMock;
        private IPromotionService? _promotionService;

        [TestInitialize]
        public void Setup()
        {
            _promotionRepositoryMock = new Mock<IPromotionRepository>();
            _userRepositoryMock = new Mock<IUserRepository>();
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _mapperMock = new Mock<IMapper>();
            _promotionService = new PromotionService(
                _promotionRepositoryMock.Object,
                _userRepositoryMock.Object,
                _roleRepositoryMock.Object,
                _mapperMock.Object
            );
        }

        [TestMethod]
        public async Task CreatePromotion_ShouldCreatePromotion()
        {
            // Arrange
            var userId = "1";
            var promotionDto = new PromotionDto { Type = "Discount" };
            var user = new User { UserId = "1", Username = "manager", RoleId = "2" };
            var role = new Role { RoleId = "2", RoleName = "Manager" };
            var promotion = new Promotion { PromotionId = "1", Type = "Discount", ApproveManager = "manager" };

            _userRepositoryMock!.Setup(repo => repo.GetById(userId)).ReturnsAsync(user);
            _roleRepositoryMock!.Setup(repo => repo.GetById(user.RoleId)).ReturnsAsync(role);
            _mapperMock!.Setup(mapper => mapper.Map<Promotion>(promotionDto)).Returns(promotion);
            _promotionRepositoryMock!.Setup(repo => repo.Create(promotion)).ReturnsAsync(1);

            // Act
            var result = await _promotionService!.CreatePromotion(userId, promotionDto);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task CreatePromotion_ShouldThrowUnauthorizedAccessException()
        {
            // Arrange
            var userId = "1";
            var promotionDto = new PromotionDto { Type = "Discount" };
            var user = new User { UserId = "1", Username = "employee", RoleId = "3" };
            var role = new Role { RoleId = "3", RoleName = "Employee" };

            _userRepositoryMock!.Setup(repo => repo.GetById(userId)).ReturnsAsync(user);
            _roleRepositoryMock!.Setup(repo => repo.GetById(user.RoleId)).ReturnsAsync(role);

            // Act & Assert
            await Assert.ThrowsExceptionAsync<UnauthorizedAccessException>(() => _promotionService!.CreatePromotion(userId, promotionDto));
        }

        [TestMethod]
        public async Task DeletePromotion_ShouldDeletePromotion()
        {
            // Arrange
            var promotionId = "1";
            _promotionRepositoryMock!.Setup(repo => repo.Delete(promotionId)).ReturnsAsync(1);

            // Act
            var result = await _promotionService!.DeletePromotion(promotionId);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task GetPromotions_ShouldReturnPromotions()
        {
            // Arrange
            var promotions = new List<Promotion> { new Promotion { PromotionId = "1", Type = "Discount" } };
            _promotionRepositoryMock!.Setup(repo => repo.Gets()).ReturnsAsync(promotions);

            // Act
            var result = await _promotionService!.GetPromotions();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(promotions.Count, result.Count());
            Assert.AreEqual(promotions[0].PromotionId, result.First().PromotionId);
            Assert.AreEqual(promotions[0].Type, result.First().Type);
        }

        [TestMethod]
        public async Task GetPromotionById_ShouldReturnPromotion()
        {
            // Arrange
            var promotionId = "1";
            var promotion = new Promotion { PromotionId = "1", Type = "Discount" };
            _promotionRepositoryMock!.Setup(repo => repo.GetById(promotionId)).ReturnsAsync(promotion);

            // Act
            var result = await _promotionService!.GetPromotionById(promotionId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(promotion.PromotionId, result.PromotionId);
            Assert.AreEqual(promotion.Type, result.Type);
        }

        [TestMethod]
        public async Task UpdatePromotion_ShouldUpdatePromotion()
        {
            // Arrange
            var promotionId = "1";
            var promotionDto = new PromotionDto { Type = "Discount" };
            var promotion = new Promotion { PromotionId = "1", Type = "Discount" };

            _mapperMock!.Setup(mapper => mapper.Map<Promotion>(promotionDto)).Returns(promotion);
            _promotionRepositoryMock!.Setup(repo => repo.Update(promotionId, promotion)).ReturnsAsync(1);

            // Act
            var result = await _promotionService!.UpdatePromotion(promotionId, promotionDto);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
