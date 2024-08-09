using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using AutoMapper;
using BusinessObjects.Dto;
using BusinessObjects.Dto.ResponseDto;
using BusinessObjects.Models;
using Repositories.Interface;
using Services.Implementation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Tests
{
    [TestClass]
    public class UserServiceTests
    {
        private Mock<IUserRepository> _userRepositoryMock;
        private Mock<ICounterRepository> _counterRepositoryMock;
        private Mock<IMapper> _mapperMock;
        private UserService _userService;

        [TestInitialize]
        public void Setup()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _counterRepositoryMock = new Mock<ICounterRepository>();
            _mapperMock = new Mock<IMapper>();
            _userService = new UserService(_userRepositoryMock.Object, _counterRepositoryMock.Object, _mapperMock.Object);
        }

        [TestMethod]
        public async Task Login_ShouldReturnUser_WhenCredentialsAreValid()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
            var user = new User { UserId = "1" };
            _userRepositoryMock.Setup(repo => repo.GetUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);

            // Act
            var result = await _userService.Login(loginDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public async Task Login_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "invalid@example.com", Password = "wrongpassword" };
            _userRepositoryMock.Setup(repo => repo.GetUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await _userService.Login(loginDto);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task Login_ShouldAssignCounterToUser_WhenCounterIdIsProvided()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password", CounterId = "2" };
            var user = new User { UserId = "1" };
            _userRepositoryMock.Setup(repo => repo.GetUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.AssignCounterToUser(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _userService.Login(loginDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result);
            _userRepositoryMock.Verify(repo => repo.AssignCounterToUser(user.UserId, loginDto.CounterId), Times.Once);
        }

        [TestMethod]
        public async Task Logout_ShouldReturnUser_WhenUserIdIsValid()
        {
            // Arrange
            var userId = "1";
            var user = new User { UserId = userId };
            _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<string>())).ReturnsAsync(user);
            _userRepositoryMock.Setup(repo => repo.ReleaseCounterFromUser(It.IsAny<User>())).ReturnsAsync(true);

            // Act
            var result = await _userService.Logout(userId);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(user, result);
        }

        [TestMethod]
        public async Task Logout_ShouldReturnNull_WhenUserIdIsInvalid()
        {
            // Arrange
            var userId = "999";
            _userRepositoryMock.Setup(repo => repo.GetUserById(It.IsAny<string>())).ReturnsAsync((User)null);

            // Act
            var result = await _userService.Logout(userId);

            // Assert
            Assert.IsNull(result);
        }

        [TestMethod]
        public async Task UpdateCounterByUserId_ShouldReturnTrue_WhenUpdateIsSuccessful()
        {
            // Arrange
            var userId = "1";
            var counterId = "2";
            _userRepositoryMock.Setup(repo => repo.UpdateCounterByUserId(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(true);

            // Act
            var result = await _userService.UpdateCounterByUserId(userId, counterId);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task UpdateCounterByUserId_ShouldReturnFalse_WhenUpdateIsUnsuccessful()
        {
            // Arrange
            var userId = "1";
            var counterId = "2";
            _userRepositoryMock.Setup(repo => repo.UpdateCounterByUserId(It.IsAny<string>(), It.IsAny<string>())).ReturnsAsync(false);

            // Act
            var result = await _userService.UpdateCounterByUserId(userId, counterId);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task GetUsers_ShouldReturnUserResponseDtos()
        {
            // Arrange
            var users = new List<User> { new User { UserId = "1" } };
            var userResponseDtos = new List<UserResponseDto> { new UserResponseDto { UserId = "1" } };
            _userRepositoryMock.Setup(repo => repo.Gets()).ReturnsAsync(users);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserResponseDto>>(It.IsAny<IEnumerable<User>>())).Returns(userResponseDtos);

            // Act
            var result = await _userService.GetUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userResponseDtos.Count, result.Count());
        }

        [TestMethod]
        public async Task GetUsers_ShouldReturnEmptyList_WhenNoUsersExist()
        {
            // Arrange
            var users = new List<User>();
            var userResponseDtos = new List<UserResponseDto>();
            _userRepositoryMock.Setup(repo => repo.Gets()).ReturnsAsync(users);
            _mapperMock.Setup(mapper => mapper.Map<IEnumerable<UserResponseDto>>(It.IsAny<IEnumerable<User>>())).Returns(userResponseDtos);

            // Act
            var result = await _userService.GetUsers();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userResponseDtos.Count, result.Count());
        }

        [TestMethod]
        public async Task IsUser_ShouldReturnTrue_WhenUserExists()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
            var users = new List<User> { new User { UserId = "1" } };
            _userRepositoryMock.Setup(repo => repo.Find(It.IsAny<Func<User, bool>>())).ReturnsAsync(users);

            // Act
            var result = await _userService.IsUser(loginDto);

            // Assert
            Assert.IsTrue(result);
        }

        [TestMethod]
        public async Task IsUser_ShouldReturnFalse_WhenUserDoesNotExist()
        {
            // Arrange
            var loginDto = new LoginDto { Email = "test@example.com", Password = "password" };
            var users = new List<User>();
            _userRepositoryMock.Setup(repo => repo.Find(It.IsAny<Func<User, bool>>())).ReturnsAsync(users);

            // Act
            var result = await _userService.IsUser(loginDto);

            // Assert
            Assert.IsFalse(result);
        }

        [TestMethod]
        public async Task UpdateUser_ShouldReturnNumberOfAffectedRows()
        {
            // Arrange
            var id = "1";
            var userDto = new UserDto { Email = "test@example.com" };
            var user = new User { UserId = id };
            _mapperMock.Setup(mapper => mapper.Map<User>(It.IsAny<UserDto>())).Returns(user);
            _userRepositoryMock.Setup(repo => repo.Update(It.IsAny<string>(), It.IsAny<User>())).ReturnsAsync(1);

            // Act
            var result = await _userService.UpdateUser(id, userDto);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task AddUser_ShouldReturnNumberOfAffectedRows()
        {
            // Arrange
            var userDto = new UserDto { Email = "test@example.com" };
            var user = new User { UserId = "1" };
            _mapperMock.Setup(mapper => mapper.Map<User>(It.IsAny<UserDto>())).Returns(user);
            _userRepositoryMock.Setup(repo => repo.Create(It.IsAny<User>())).ReturnsAsync(1);

            // Act
            var result = await _userService.AddUser(userDto);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task GetUserById_ShouldReturnUserResponseDto()
        {
            // Arrange
            var id = "1";
            var user = new User { UserId = id };
            var userResponseDto = new UserResponseDto { UserId = id };
            _userRepositoryMock.Setup(repo => repo.GetById(It.IsAny<string>())).ReturnsAsync(user);
            _mapperMock.Setup(mapper => mapper.Map<UserResponseDto>(It.IsAny<User>())).Returns(userResponseDto);

            // Act
            var result = await _userService.GetUserById(id);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(userResponseDto, result);
        }

        [TestMethod]
        public async Task DeleteUser_ShouldReturnNumberOfAffectedRows()
        {
            // Arrange
            var id = "1";
            _userRepositoryMock.Setup(repo => repo.Delete(It.IsAny<string>())).ReturnsAsync(1);

            // Act
            var result = await _userService.DeleteUser(id);

            // Assert
            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public async Task LoginCustomer_ShouldReturnCustomer()
        {
            // Arrange
            var customerLoginDto = new CustomerLoginDto { Phone = "0123456789", Password = "password" };
            var customer = new Customer { CustomerId = "1" };
            _mapperMock.Setup(mapper => mapper.Map<Customer>(It.IsAny<CustomerLoginDto>())).Returns(customer);
            _userRepositoryMock.Setup(repo => repo.GetCustomer(It.IsAny<Customer>())).ReturnsAsync(customer);

            // Act
            var result = await _userService.LoginCustomer(customerLoginDto);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(customer, result);
        }

        [TestMethod]
        public async Task LoginCustomer_ShouldReturnNull_WhenCredentialsAreInvalid()
        {
            // Arrange
            var customerLoginDto = new CustomerLoginDto { Phone = "0123456789", Password = "wrongpassword" };
            _userRepositoryMock.Setup(repo => repo.GetCustomer(It.IsAny<Customer>())).ReturnsAsync((Customer)null);

            // Act
            var result = await _userService.LoginCustomer(customerLoginDto);

            // Assert
            Assert.IsNull(result);
        }
    }
}
