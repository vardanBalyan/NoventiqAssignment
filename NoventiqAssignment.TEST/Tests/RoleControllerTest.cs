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
    public class RoleControllerTest
    {
        private readonly IFixture _fixture;
        private readonly Mock<IRoleService> _serviceMock;
        private readonly RoleController _controller;

        public RoleControllerTest()
        {
            _fixture = new Fixture().Customize(new AutoMoqCustomization());
            _serviceMock = new Mock<IRoleService>();
            _controller = new RoleController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAllRolesAsync_ReturnsOk()
        {
            // Arrange
            var roleList = _fixture.CreateMany<GetRoleDto>(3).ToList();

            var roles = new ValueTask<Result<List<GetRoleDto>, DefaultFailure>>(
                Result.Success<List<GetRoleDto>, DefaultFailure>(roleList)
            );

            _serviceMock.Setup(s => s.GetAllRolesAsync()).Returns(roles);

            // Act
            var result = await _controller.GetAllRolesAsync() as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(roleList, result.Value as List<GetRoleDto>);
        }

        [Theory]
        [AutoData]
        public async Task GetRolesByNameAsync_ReturnsOk(string name)
        {
            // Arrange
            var role = _fixture.Create<GetRoleDto>();

            var serviceResult = new ValueTask<Result<GetRoleDto, DefaultFailure>>(
                Result.Success<GetRoleDto, DefaultFailure>(role)
            );

            _serviceMock.Setup(s => s.GetRolesByNameAsync(name)).Returns(serviceResult);

            // Act
            var result = await _controller.GetRolesByNameAsync(name) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(role, result.Value as GetRoleDto);
        }

        [Theory]
        [AutoData]
        public async Task GetRolesByNameAsync_ReturnsNotFound(string name)
        {
            // Arrange
            var serviceResult = new ValueTask<Result<GetRoleDto, DefaultFailure>>(
                Result.Failure<GetRoleDto, DefaultFailure>(new DefaultFailure([""]))
            );

            _serviceMock.Setup(s => s.GetRolesByNameAsync(name)).Returns(serviceResult);

            // Act
            var result = await _controller.GetRolesByNameAsync(name) as NotFoundObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(404, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task CreateRoleAsync_ReturnsOk(RoleDto model)
        {
            // Arrange
            var role = _fixture.Create<string>();

            var serviceResult = new ValueTask<Result<string, DefaultFailure>>(
                Result.Success<string, DefaultFailure>(role)
            );

            _serviceMock.Setup(s => s.CreateRoleAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.CreateRoleAsync(model) as OkObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(role, result.Value as string);
        }

        [Theory]
        [AutoData]
        public async Task CreateRoleAsync_ReturnsBadRequest(RoleDto model)
        {
            // Arrange
            var serviceResult = new ValueTask<Result<string, DefaultFailure>>(
                Result.Failure<string, DefaultFailure>(new DefaultFailure([""]))
            );

            _serviceMock.Setup(s => s.CreateRoleAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.CreateRoleAsync(model) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task UpdateRoleAsync_ReturnsOk(UpdateRoleDto model)
        {
            // Arrange
            var role = _fixture.Create<bool>();

            var serviceResult = new ValueTask<Result<bool, DefaultFailure>>(
                Result.Success<bool, DefaultFailure>(role)
            );

            _serviceMock.Setup(s => s.UpdateRoleAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.UpdateRoleAsync(model) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task UpdateRoleAsync_ReturnsBadRequest(UpdateRoleDto model)
        {
            // Arrange
            var serviceResult = new ValueTask<Result<bool, DefaultFailure>>(
                Result.Failure<bool, DefaultFailure>(new DefaultFailure([""]))
            );

            _serviceMock.Setup(s => s.UpdateRoleAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.UpdateRoleAsync(model) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task DeleteRoleAsync_ReturnsOk(RoleDto model)
        {
            // Arrange
            var role = _fixture.Create<bool>();

            var serviceResult = new ValueTask<Result<bool, DefaultFailure>>(
                Result.Success<bool, DefaultFailure>(role)
            );

            _serviceMock.Setup(s => s.DeleteRoleAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.DeleteRoleAsync(model) as NoContentResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(204, result.StatusCode);
        }

        [Theory]
        [AutoData]
        public async Task DeleteRoleAsync_ReturnsBadRequest(RoleDto model)
        {
            // Arrange
            var serviceResult = new ValueTask<Result<bool, DefaultFailure>>(
                Result.Failure<bool, DefaultFailure>(new DefaultFailure([""]))
            );

            _serviceMock.Setup(s => s.DeleteRoleAsync(model)).Returns(serviceResult);

            // Act
            var result = await _controller.DeleteRoleAsync(model) as BadRequestObjectResult;

            // Assert
            Assert.NotNull(result);
            Assert.Equal(400, result.StatusCode);
        }
    }
}
