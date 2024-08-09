using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Models;
using Repositories.Interface;
using Services.Implementation;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class RoleServiceTests
    {
        private Mock<IRoleRepository> _roleRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private RoleService _roleService;

        [TestInitialize]
        public void Setup()
        {
            _roleRepositoryMock = new Mock<IRoleRepository>();
            _mapperMock = new Mock<IMapper>();
            _roleService = new RoleService(_roleRepositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task Gets_ShouldReturnRoles()
        {
            // Arrange
            var roles = new List<Role>
            {
                new Role { RoleId = "1", RoleName = "Admin" },
                new Role { RoleId = "2", RoleName = "Manager" },
                new Role { RoleId = "3", RoleName = "Staff" }
            };
            _roleRepositoryMock.Setup(repo => repo.Gets()).ReturnsAsync(roles);

            // Act
            var result = await _roleService.Gets();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(roles.Count, result.Count());
        }

        [TestMethod]
        public async Task GetById_ShouldReturnRole_WhenIdIsValid()
        {
            // Arrange
            var id = "1";
            var role = new Role { RoleId = id, RoleName = "Admin" };
            _roleRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync(role);

            // Act
            var result = await _roleService.GetById(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(role, result);
        }

        [TestMethod]
        public async Task GetById_ShouldReturnNull_WhenIdIsInvalid()
        {
            // Arrange
            var id = "999";
            _roleRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync((Role)null);

            // Act
            var result = await _roleService.GetById(id);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Create_ShouldReturnNumberOfAffectedRows()
        {
            // Arrange
            var roleDto = new RoleDto { RoleName = "Admin" };
            var role = new Role { RoleId = "1" };
            _mapperMock.Setup(mapper => mapper.Map<Role>(It.IsAny<RoleDto>())).Returns(role);
            _roleRepositoryMock.Setup(repo => repo.Create(It.IsAny<Role>())).ReturnsAsync(1);

            // Act
            var result = await _roleService.Create(roleDto);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task Create_ShouldReturnNumberOfAffectedRows_ForManagerRole()
        {
            // Arrange
            var roleDto = new RoleDto { RoleName = "Manager" };
            var role = new Role { RoleId = "2" };
            _mapperMock.Setup(mapper => mapper.Map<Role>(It.IsAny<RoleDto>())).Returns(role);
            _roleRepositoryMock.Setup(repo => repo.Create(It.IsAny<Role>())).ReturnsAsync(1);

            // Act
            var result = await _roleService.Create(roleDto);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task Create_ShouldReturnNumberOfAffectedRows_ForStaffRole()
        {
            // Arrange
            var roleDto = new RoleDto { RoleName = "Staff" };
            var role = new Role { RoleId = "3" };
            _mapperMock.Setup(mapper => mapper.Map<Role>(It.IsAny<RoleDto>())).Returns(role);
            _roleRepositoryMock.Setup(repo => repo.Create(It.IsAny<Role>())).ReturnsAsync(1);

            // Act
            var result = await _roleService.Create(roleDto);

            // Assert
            Assert.AreEqual(1, result);
        }
    }
}
