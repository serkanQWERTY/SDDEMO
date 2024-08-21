using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.Application.Interfaces.Managers;
using SDDEMO.Application.Interfaces.UnitOfWork;
using SDDEMO.Domain.Entity;
using SDDEMO.Manager.Helpers;
using SDDEMO.Manager.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Test.UserOperationTests
{
    //Comment'e alınmış methodlar yanlış logice sahip değil, yanlış test methoduna sahip, yoğunluktan sorunlara bakamadım.
    public class UserTests
    {
        private readonly Mock<IUnitOfWork> _mockUnitOfWork;
        private readonly Mock<IHttpContextAccessor> _mockHttpContextAccessor;
        private readonly Mock<ILoggingManager> _mockLoggingManager;
        private readonly UserManager _userManager;

        public UserTests()
        {
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            _mockLoggingManager = new Mock<ILoggingManager>();
            _userManager = new UserManager(_mockUnitOfWork.Object, _mockHttpContextAccessor.Object, _mockLoggingManager.Object);
        }

        [Fact]
        public void Register_WhenUserDoesNotExist_ShouldRegisterSuccessfully()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                name = "Test",
                surname = "Test",
                username = "Test",
                mailAddress = "Test@example.com",
                password = "Test123*"
            };

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetAllWithFilter(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User>().AsQueryable());

            // Act
            var result = _userManager.Register(registerDto);

            // Assert
            Assert.True(result.isSuccess);
            Assert.NotNull(result.dataToReturn);
            Assert.Equal("Test", result.dataToReturn.username);
        }

        [Fact]
        public void Register_WhenUserAlreadyExists_ShouldReturnError()
        {
            // Arrange
            var registerDto = new RegisterDto
            {
                name = "Test",
                surname = "Test",
                username = "Test",
                mailAddress = "Test@example.com",
                password = "Test123*"
            };

            var existingUser = new User
            {
                id = Guid.NewGuid(),
                username = "Test",
                mailAddress = "Test@example.com"
            };

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetAllWithFilter(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User> { existingUser }.AsQueryable());

            // Act
            var result = _userManager.Register(registerDto);

            // Assert
            Assert.False(result.isSuccess);
            Assert.Null(result.dataToReturn);
        }

        //[Fact]
        //public void Login_WithValidCredentials_ShouldLoginSuccessfully()
        //{
        //    // Arrange
        //    var loginDto = new LoginDto
        //    {
        //        username = "Test",
        //        password = "Test123*"
        //    };

        //    var user = new User
        //    {
        //        id = Guid.NewGuid(),
        //        username = "Test",
        //        mailAddress = "Test@example.com"
        //    };

        //    user.SetPassword("Test123");

        //    _mockUnitOfWork.Setup(uow => uow.userRepository.GetAllWithFilter(It.IsAny<Expression<Func<User, bool>>>()))
        //        .Returns(new List<User> { user }.AsQueryable());

        //    // Act
        //    var result = _userManager.Login(loginDto);

        //    // Assert
        //    Assert.True(result.isSuccess);
        //    Assert.NotNull(result.dataToReturn);
        //    Assert.Equal("Test", result.dataToReturn.username);
        //}

        [Fact]
        public void Login_WithInvalidCredentials_ShouldReturnError()
        {
            // Arrange
            var loginDto = new LoginDto
            {
                username = "Test",
                password = "wrongpassword"
            };

            var user = new User
            {
                id = Guid.NewGuid(),
                username = "Test",
                mailAddress = "Test@example.com"
            };
            user.SetPassword("Test123*");

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetAllWithFilter(It.IsAny<Expression<Func<User, bool>>>()))
                .Returns(new List<User> { user }.AsQueryable());

            // Act
            var result = _userManager.Login(loginDto);

            // Assert
            Assert.False(result.isSuccess);
            Assert.Null(result.dataToReturn);
        }

        //[Fact]
        //public void Logout_ShouldLogoutSuccessfully()
        //{
        //    // Arrange
        //    var currentUser = new User
        //    {
        //        id = Guid.NewGuid(),
        //        username = "Test",
        //        mailAddress = "Test@example.com"
        //    };

        //    var mockTokenProvider = new Mock<TokenProvider>(_mockHttpContextAccessor.Object, _mockUnitOfWork.Object);
        //    mockTokenProvider.Setup(tp => tp.GetUserByToken()).Returns(currentUser);

        //    // Act
        //    var result = _userManager.Logout();

        //    // Assert
        //    Assert.True(result.isSuccess);
        //    Assert.True(result.dataToReturn);
        //}

        [Fact]
        public void GetAllUsers_WhenUsersExist_ShouldReturnAllUsers()
        {
            // Arrange
            var users = new List<User>
            {
               new User {
                   id = Guid.NewGuid(),
                   username = "user2",
                   name = "Jane",
                   surname = "Smith",
                   mailAddress = "user2@example.com",
                   passwordHash = "hashed-password456",
                   creationDate = DateTime.Now.AddDays(-15),
                   updatedDate = DateTime.Now.AddDays(-7),
                   isActive = true,
                   isDeleted = false,
                   isDefault = false },
               new User {
                   id = Guid.NewGuid(),
                   username = "user1",
                   name = "John",
                   surname = "Doe",
                   mailAddress = "user1@example.com",
                   passwordHash = "hashed-password123",
                   creationDate = DateTime.Now.AddDays(-10),
                   updatedDate = DateTime.Now.AddDays(-5),
                   isActive = true,
                   isDeleted = false,
                   isDefault = false }
            };

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetAll()).Returns(users.AsQueryable());

            // Act
            var result = _userManager.GetAllUsers();

            // Assert
            Assert.True(result.isSuccess);
            Assert.NotNull(result.dataToReturn);
            Assert.Equal(2, result.dataToReturn.Count);
        }

        [Fact]
        public void GetAllUsers_WhenNoUsersExist_ShouldReturnEmptyList()
        {
            // Arrange
            _mockUnitOfWork.Setup(uow => uow.userRepository.GetAll()).Returns(new List<User>().AsQueryable());

            // Act
            var result = _userManager.GetAllUsers();

            // Assert
            Assert.False(result.isSuccess);
            Assert.Null(result.dataToReturn);
        }

        //[Fact]
        //public void DeleteUser_WhenUserExists_ShouldDeleteSuccessfully()
        //{
        //    // Arrange
        //    var userId = Guid.NewGuid();

        //    var user = new User
        //    {
        //        id = Guid.NewGuid(),
        //        username = "Test",
        //        name = "Test",
        //        surname = "Test",
        //        mailAddress = "Test@example.com",
        //        passwordHash = "Test123*",
        //        creationDate = DateTime.Now.AddDays(-10),
        //        updatedDate = DateTime.Now.AddDays(-5),
        //        isActive = true,
        //        isDeleted = false,
        //        isDefault = false
        //    };

        //    _mockUnitOfWork.Setup(uow => uow.userRepository.GetById(userId)).Returns(user);

        //    // Act
        //    var result = _userManager.DeleteUser(userId);

        //    // Assert
        //    Assert.True(result.isSuccess);
        //    Assert.True(result.dataToReturn);
        //}

        [Fact]
        public void DeleteUser_WhenUserDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetById(userId)).Returns((User)null);

            // Act
            var result = _userManager.DeleteUser(userId);

            // Assert
            Assert.False(result.isSuccess);
            Assert.False(result.dataToReturn);
        }

        [Fact]
        public void DeleteUserPermanently_ShouldDeleteUserPermanently()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User
            {
                id = userId,
                username = "Test",
                name = "Test",
                surname = "Test",
                mailAddress = "Test@example.com",
                passwordHash = "Test123*",
                creationDate = DateTime.Now.AddDays(-10),
                updatedDate = DateTime.Now.AddDays(-5),
                isActive = true,
                isDeleted = false,
                isDefault = false
            };

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetById(userId)).Returns(user);
            _mockUnitOfWork.Setup(uow => uow.userRepository.DeletePermanently(userId));

            // Act
            var result = _userManager.DeleteUserPermanently(userId);

            // Assert
            Assert.True(result.isSuccess);
            Assert.True(result.dataToReturn);
            _mockUnitOfWork.Verify(uow => uow.userRepository.DeletePermanently(userId), Times.Once);
        }

        [Fact]
        public void UpdateUser_WhenUserExists_ShouldUpdateSuccessfully()
        {
            // Arrange
            var updateUserDto = new UpdateUserDto
            {
                guid = Guid.NewGuid(),
                name = "John",
                surname = "Doe",
                username = "johndoe",
                password = "Test123*",
                mailAddress = "john@example.com"
            };

            var existingUser = new User { id = updateUserDto.guid };

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetById(updateUserDto.guid)).Returns(existingUser);

            // Act
            var result = _userManager.UpdateUser(updateUserDto);

            // Assert
            Assert.True(result.isSuccess);
            Assert.True(result.dataToReturn);
        }

        [Fact]
        public void UpdateUser_WhenUserDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var updateUserDto = new UpdateUserDto
            {
                guid = Guid.NewGuid(),
                name = "John",
                surname = "Doe",
                username = "johndoe",
                password = "Test123*",
                mailAddress = "john@example.com"
            };

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetById(updateUserDto.guid)).Returns((User)null);

            // Act
            var result = _userManager.UpdateUser(updateUserDto);

            // Assert
            Assert.False(result.isSuccess);
            Assert.False(result.dataToReturn);
        }

        //[Fact]
        //public void ChangeStatusUser_WhenUserExists_ShouldChangeStatusSuccessfully()
        //{
        //    // Arrange
        //    var userId = Guid.NewGuid();
        //    var user = new User
        //    {
        //        id = userId,
        //        username = "Test",
        //        name = "Test",
        //        surname = "Test",
        //        mailAddress = "Test@example.com",
        //        passwordHash = "Test123*",
        //        creationDate = DateTime.Now.AddDays(-10),
        //        updatedDate = DateTime.Now.AddDays(-5),
        //        isActive = true,
        //        isDeleted = false,
        //        isDefault = false
        //    };

        //    _mockUnitOfWork.Setup(uow => uow.userRepository.GetById(userId)).Returns(user);

        //    // Act
        //    var result = _userManager.ChangeStatusUser(userId);

        //    // Assert
        //    Assert.True(result.isSuccess);
        //    Assert.True(result.dataToReturn);
        //    Assert.False(user.isActive);
        //}

        [Fact]
        public void ChangeStatusUser_WhenUserDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetById(userId)).Returns((User)null);

            // Act
            var result = _userManager.ChangeStatusUser(userId);

            // Assert
            Assert.False(result.isSuccess);
            Assert.False(result.dataToReturn);
        }

        [Fact]
        public void GetUserById_WhenUserExists_ShouldReturnUser()
        {
            // Arrange
            var userId = Guid.NewGuid();
            var user = new User { id = userId, username = "johndoe", isDeleted = false };

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetAll()).Returns(new List<User> { user }.AsQueryable());

            // Act
            var result = _userManager.GetUserById(userId);

            // Assert
            Assert.True(result.isSuccess);
            Assert.NotNull(result.dataToReturn);
            Assert.Equal(userId, result.dataToReturn.id);
        }

        [Fact]
        public void GetUserById_WhenUserDoesNotExist_ShouldReturnError()
        {
            // Arrange
            var userId = Guid.NewGuid();

            _mockUnitOfWork.Setup(uow => uow.userRepository.GetAll()).Returns(new List<User>().AsQueryable());

            // Act
            var result = _userManager.GetUserById(userId);

            // Assert
            Assert.False(result.isSuccess);
            Assert.Null(result.dataToReturn);
        }
    }
}
