using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using NoventiqAssignment.API.Controllers;
using NoventiqAssignment.APP.Dtos;
using NoventiqAssignment.APP.Services;
using NoventiqAssignment.Framework.Framework;

namespace NoventiqAssignment.TEST.Tests
{
    public class UserControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IUserService> _serviceMock;
        private readonly UserController _controller;

        public UserControllerTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _serviceMock = new Mock<IUserService>();
            _controller = new UserController(_serviceMock.Object);
        }

        [Theory]
        [AutoData]
        public async Task Register_ReturnsOk(RegisterDto model)
        {
            // Arrange
            var data = _fixture.Create<TokenResponseDto>();

            var serviceResult = new ValueTask<Result<TokenResponseDto, DefaultFailure>>(
                Result.Success<TokenResponseDto, DefaultFailure>(data)
            );

            _serviceMock.Setup(s => s.RegisterAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.Register(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(data, result.Value as TokenResponseDto);
        }

        [Theory]
        [AutoData]
        public async Task Register_ReturnsBadRequest(RegisterDto model)
        {
            // Arrange
            var serviceResult = new ValueTask<Result<TokenResponseDto, DefaultFailure>>(
                Result.Failure<TokenResponseDto, DefaultFailure>(new DefaultFailure([""]))
            );

            _serviceMock.Setup(s => s.RegisterAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.Register(model) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task Login_ReturnsOk(LoginDto model)
        {
            // Arrange
            var data = _fixture.Create<TokenResponseDto>();

            var serviceResult = new ValueTask<Result<TokenResponseDto, DefaultFailure>>(
                Result.Success<TokenResponseDto, DefaultFailure>(data)
            );

            _serviceMock.Setup(s => s.LoginUserAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.Login(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(data, result.Value as TokenResponseDto);
        }

        [Theory]
        [AutoData]
        public async Task Login_ReturnsUnauthorize(LoginDto model)
        {
            // Arrange
            var serviceResult = new ValueTask<Result<TokenResponseDto, DefaultFailure>>(
                Result.Failure<TokenResponseDto, DefaultFailure>(new DefaultFailure([""]))
            );

            _serviceMock.Setup(s => s.LoginUserAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.Login(model) as UnauthorizedResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(401, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task Refresh_ReturnsOk(RefreshTokenDto model)
        {
            // Arrange
            var data = _fixture.Create<TokenResponseDto>();

            var serviceResult = new ValueTask<Result<TokenResponseDto, DefaultFailure>>(
                Result.Success<TokenResponseDto, DefaultFailure>(data)
            );

            _serviceMock.Setup(s => s.RefreshTokenAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.Refresh(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(data, result.Value as TokenResponseDto);
        }

        [Theory]
        [AutoData]
        public async Task Refresh_ReturnsBadRequest(RefreshTokenDto model)
        {
            // Arrange
            var serviceResult = new ValueTask<Result<TokenResponseDto, DefaultFailure>>(
                Result.Failure<TokenResponseDto, DefaultFailure>(new DefaultFailure([""]))
            );

            _serviceMock.Setup(s => s.RefreshTokenAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.Refresh(model) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task GetUserByUserName_ReturnsOk(string username)
        {
            // Arrange
            var data = _fixture.Create<GetUserDto>();

            var serviceResult = new ValueTask<Result<GetUserDto, DefaultFailure>>(
                Result.Success<GetUserDto, DefaultFailure>(data)
            );

            _serviceMock.Setup(s => s.GetUserByUserNameAsync(username)).Returns(serviceResult);

            // Act
            var result = await _controller.GetUserByUserName(username) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(data, result.Value as GetUserDto);
        }

        [Theory]
        [AutoData]
        public async Task GetUserByUserName_ReturnsNotFound(string username)
        {
            // Arrange
            var serviceResult = new ValueTask<Result<GetUserDto, DefaultFailure>>(
                Result.Failure<GetUserDto, DefaultFailure>(new DefaultFailure([""]))
            );

            _serviceMock.Setup(s => s.GetUserByUserNameAsync(username)).Returns(serviceResult);

            // Act
            var result = await _controller.GetUserByUserName(username) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task UpdateUserAsync_ReturnsNoContent(UpdateUserDto model)
        {
            // Arrange
            var data = _fixture.Create<bool>();

            var serviceResult = new ValueTask<Result<bool, DefaultFailure>>(
                Result.Success<bool, DefaultFailure>(data)
            );

            _serviceMock.Setup(s => s.UpdateUserAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.UpdateUserAsync(model) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task UpdateUserAsync_ReturnsBadRequest(UpdateUserDto model)
        {
            // Arrange
            var serviceResult = new ValueTask<Result<bool, DefaultFailure>>(
                Result.Failure<bool, DefaultFailure>(new DefaultFailure([""]))
            );

            _serviceMock.Setup(s => s.UpdateUserAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.UpdateUserAsync(model) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task DeleteUserAsync_ReturnsNoContent(string username)
        {
            // Arrange
            var data = _fixture.Create<bool>();

            var serviceResult = new ValueTask<Result<bool, DefaultFailure>>(
                Result.Success<bool, DefaultFailure>(data)
            );

            _serviceMock.Setup(s => s.DeleteUserAsync(username)).Returns(serviceResult);

            // Act
            var result = await _controller.DeleteUserAsync(username) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task DeleteUserAsync_ReturnsBadRequest(string username)
        {
            // Arrange
            var serviceResult = new ValueTask<Result<bool, DefaultFailure>>(
                Result.Failure<bool, DefaultFailure>(new DefaultFailure([""]))
            );

            _serviceMock.Setup(s => s.DeleteUserAsync(username)).Returns(serviceResult);

            // Act
            var result = await _controller.DeleteUserAsync(username) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
