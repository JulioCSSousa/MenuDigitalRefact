using Xunit;
using Moq;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MenuDigital.Application.Services;
using MenuDigital.Domain.Entities;
using MenuDigital.Domain.Entities.MenuModels;
using MenuDigitalApi.Controllers.MenuController;
using MenuDigitalApi.DTOs.Menu.Products.Request.Create;
using MenuDigitalApi.DTOs.Menu.Products.Request.Update;
using MenuDigitalApi.DTOs.Menu.Products.Response.ProductMenu;

namespace MenuDigital.Api.Tests.Controllers
{
    public class ProductControllerTests
    {
        private readonly Mock<ProductService> _serviceMock;
        private readonly ProductController _controller;

        public ProductControllerTests()
        {
            _serviceMock = new Mock<ProductService>(Array.Empty<object>());
            _controller = new ProductController(_serviceMock.Object);
        }

        [Fact]
        public async Task GetAll_ShouldReturnOk_WithListOfProducts()
        {
            // Arrange
            var products = new List<ProductModel>
            {
                new() { ProductId = Guid.NewGuid(), Name = "Pizza" },
                new() { ProductId = Guid.NewGuid(), Name = "Hamburger" }
            };

            _serviceMock.Setup(s => s.GetAllAsync(It.IsAny<CancellationToken>()))
                        .ReturnsAsync(products);

            // Act
            var result = await _controller.GetAll(CancellationToken.None);

            // Assert
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().BeOfType<List<ProductGetAllReponseDto>>();
            ((List<ProductGetAllReponseDto>)okResult.Value!).Should().HaveCount(2);
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenProductExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var product = new ProductModel { ProductId = id, Name = "Pizza" };

            _serviceMock.Setup(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(product);

            // Act
            var result = await _controller.GetById(id, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            var returned = okResult!.Value as ProductModel;
            returned!.Name.Should().Be("Pizza");
        }

        [Fact]
        public async Task GetById_ShouldReturnOk_WhenProductIsNull()
        {
            // Arrange
            var id = Guid.NewGuid();
            _serviceMock.Setup(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync((ProductModel?)null);

            // Act
            var result = await _controller.GetById(id, CancellationToken.None);

            // Assert
            result.Should().BeOfType<OkObjectResult>();
            var ok = result as OkObjectResult;
            ok!.Value.Should().BeNull();
        }

        [Fact]
            public async Task Create_ShouldReturnOk_WithSuccessMessage()
        {
            // Arrange
            var createDto = new ProductMenuCreate
            {
                Name = "Pizza",
                Description = "Delicious"
            };

            _serviceMock.Setup(s => s.CreateAsync(It.IsAny<ProductModel>(), It.IsAny<CancellationToken>()))
                        .Returns(Task.CompletedTask);

            // Act
            var result = await _controller.Create(createDto, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().Be("Successfully Created");
        }

        [Fact]
        public async Task Update_ShouldReturnOk_WhenProductExists()
        {
            // Arrange
            var id = Guid.NewGuid();
            var dbProduct = new ProductModel { ProductId = id, Name = "Old Pizza" };
            var updateDto = new ProductMenuRequestUpdateDto { Name = "New Pizza" };

            _serviceMock.Setup(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync(dbProduct);

            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<ProductModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

            // Act
            var result = await _controller.Update(id, updateDto, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().Be("Successflly Updated");
        }

        [Fact]
        public async Task Update_ShouldReturnNotFound_WhenProductDoesNotExist()
        {
            // Arrange
            var id = Guid.NewGuid();

            _serviceMock.Setup(s => s.GetByIdAsync(id, It.IsAny<CancellationToken>()))
                        .ReturnsAsync((ProductModel?)null);

            // Act
            var result = await _controller.Update(id, new ProductMenuRequestUpdateDto(), CancellationToken.None);

            // Assert
            var notFound = result as NotFoundObjectResult;
            notFound.Should().NotBeNull();
            notFound!.Value.Should().Be("Product not found");
        }

        [Fact]
        public async Task Delete_ShouldReturnOk_WithSuccessMessage()
        {
            // Arrange
            var id = Guid.NewGuid();

            _serviceMock.Setup(s => s.UpdateAsync(It.IsAny<ProductModel>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(true);

            // Act
            var result = await _controller.Delete(id, CancellationToken.None);

            // Assert
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();
            okResult!.Value.Should().Be("Successflly Deleted");
        }
    }
}
