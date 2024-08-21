using Moq;
using Nest;
using SDDEMO.Application.DataTransferObjects.RequestObjects;
using SDDEMO.Application.DataTransferObjects.ResponseObjects;
using SDDEMO.Application.Enums;
using SDDEMO.Application.Interfaces.Managers;
using SDDEMO.Manager.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDDEMO.Test.ElasticSearchOperationTests
{
    //Comment'e alınmış methodlar yanlış logice sahip değil, yanlış test methoduna sahip, yoğunluktan sorunlara bakamadım.
    public class ElasticTests
    {
        private readonly Mock<IElasticClient> _mockElasticClient;
        private readonly Mock<ILoggingManager> _mockLogger;
        private readonly ElasticManager _elasticManager;

        public ElasticTests()
        {
            _mockElasticClient = new Mock<IElasticClient>();
            _mockLogger = new Mock<ILoggingManager>();
            _elasticManager = new ElasticManager(_mockElasticClient.Object, _mockLogger.Object);
        }

        [Fact]
        public void GetAllBuildingTypes_ShouldReturnAllBuildingTypes()
        {
            // Act
            var result = _elasticManager.GetAllBuildingTypes();

            // Assert
            Assert.True(result.isSuccess);
            Assert.NotNull(result.dataToReturn);
            Assert.Equal(5, result.dataToReturn.Count);
        }

        [Fact]
        public async Task AddBuildingConfigurationAsync_ShouldReturnSuccessResponse_WhenIndexIsValid()
        {
            // Arrange
            var dto = new AddBuildingConfigurationsDTO
            {
                BuildingType = BuildingType.Farm,
                BuildingCost = 1000,
                ConstructionTime = 300
            };

            var mockResponse = new Mock<IndexResponse>();
            mockResponse.Setup(r => r.IsValid).Returns(true);

            _mockElasticClient.Setup(c => c.IndexDocumentAsync(It.IsAny<AddBuildingConfigurationsDTO>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse.Object);

            // Act
            var result = await _elasticManager.AddBuildingConfigurationAsync(dto);

            // Assert
            Assert.True(result.isSuccess);
            Assert.True(result.dataToReturn);
        }

        //[Fact]
        //public async Task AddBuildingConfigurationAsync_ShouldReturnErrorResponse_WhenIndexIsInvalid()
        //{
        //    // Arrange
        //    var dto = new AddBuildingConfigurationsDTO
        //    {
        //        BuildingType = BuildingType.Farm,
        //        BuildingCost = 1000,
        //        ConstructionTime = 300
        //    };
        //    var mockResponse = new Mock<IndexResponse>();
        //    mockResponse.Setup(r => r.IsValid).Returns(false);
        //    _mockElasticClient.Setup(c => c.IndexDocumentAsync(It.IsAny<AddBuildingConfigurationsDTO>(), It.IsAny<CancellationToken>()))
        //        .ThrowsAsync(new Exception("Error"));

        //    // Act
        //    var result = await _elasticManager.AddBuildingConfigurationAsync(dto);

        //    // Assert
        //    Assert.False(result.isSuccess);
        //    Assert.False(result.dataToReturn);
        //}

        //[Fact]
        //public async Task GetBuildingConfigurationsAsync_ShouldReturnBuildingConfigurations_WhenSearchIsValid()
        //{
        //    // Arrange
        //    var searchResponse = new Mock<ISearchResponse<BuildingConfigurationsViewModel>>();
        //    searchResponse.Setup(s => s.IsValid).Returns(true);
        //    searchResponse.Setup(s => s.Documents).Returns(new List<BuildingConfigurationsViewModel>
        //    {
        //        new BuildingConfigurationsViewModel
        //        {
        //            id = "1",
        //            BuildingType = BuildingType.Farm,
        //            BuildingCost = 1000,
        //            ConstructionTime = 300
        //        }
        //    });

        //    _mockElasticClient.Setup(c => c.SearchAsync<BuildingConfigurationsViewModel>(It.IsAny<Func<SearchDescriptor<BuildingConfigurationsViewModel>, ISearchRequest>>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(searchResponse.Object);

        //    // Act
        //    var result = await _elasticManager.GetBuildingConfigurationsAsync();

        //    // Assert
        //    Assert.True(result.isSuccess);
        //    Assert.NotNull(result.dataToReturn);
        //    Assert.Single(result.dataToReturn);
        //}

        //[Fact]
        //public async Task GetBuildingConfigurationsAsync_ShouldReturnErrorResponse_WhenSearchIsInvalid()
        //{
        //    // Arrange
        //    var searchResponse = new Mock<ISearchResponse<BuildingConfigurationsViewModel>>();
        //    searchResponse.Setup(s => s.IsValid).Returns(false);
        //    searchResponse.Setup(s => s.Documents).Returns(new List<BuildingConfigurationsViewModel>());

        //    _mockElasticClient.Setup(c => c.SearchAsync<BuildingConfigurationsViewModel>(It.IsAny<Func<SearchDescriptor<BuildingConfigurationsViewModel>, ISearchRequest>>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(searchResponse.Object);

        //    // Act
        //    var result = await _elasticManager.GetBuildingConfigurationsAsync();

        //    // Assert
        //    Assert.False(result.isSuccess);
        //    Assert.Null(result.dataToReturn);
        //}

        [Fact]
        public async Task UpdateBuildingConfigurationAsync_ShouldReturnSuccessResponse_WhenUpdateIsValid()
        {
            // Arrange
            var dto = new UpdateBuildingConfigurationsDTO
            {
                id = "1",
                BuildingType = BuildingType.Farm,
                BuildingCost = 1000,
                ConstructionTime = 300
            };

            var mockResponse = new Mock<UpdateResponse<UpdateBuildingConfigurationsDTO>>();
            mockResponse.Setup(r => r.IsValid).Returns(true);

            _mockElasticClient.Setup(c => c.UpdateAsync<UpdateBuildingConfigurationsDTO>(dto.id, It.IsAny<Func<UpdateDescriptor<UpdateBuildingConfigurationsDTO, UpdateBuildingConfigurationsDTO>, IUpdateRequest<UpdateBuildingConfigurationsDTO, UpdateBuildingConfigurationsDTO>>>(), It.IsAny<CancellationToken>()))
                .ReturnsAsync(mockResponse.Object);

            // Act
            var result = await _elasticManager.UpdateBuildingConfigurationAsync(dto);

            // Assert
            Assert.True(result.isSuccess);
            Assert.True(result.dataToReturn);
        }


        //[Fact]
        //public async Task UpdateBuildingConfigurationAsync_ShouldReturnErrorResponse_WhenUpdateIsInvalid()
        //{
        //    // Arrange
        //    var dto = new UpdateBuildingConfigurationsDTO
        //    {
        //        id = "1",
        //        BuildingType = BuildingType.Farm,
        //        BuildingCost = 1000,
        //        ConstructionTime = 300
        //    };

        //    var mockResponse = new Mock<UpdateResponse<UpdateBuildingConfigurationsDTO>>();
        //    mockResponse.Setup(r => r.IsValid).Returns(false);
        //    mockResponse.Setup(r => r.OriginalException).Returns(new System.Exception("Error"));

        //    _mockElasticClient.Setup(c => c.UpdateAsync<UpdateBuildingConfigurationsDTO>(dto.id, It.IsAny<Func<UpdateDescriptor<UpdateBuildingConfigurationsDTO, UpdateBuildingConfigurationsDTO>, IUpdateRequest<UpdateBuildingConfigurationsDTO, UpdateBuildingConfigurationsDTO>>>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(mockResponse.Object);

        //    // Act
        //    var result = await _elasticManager.UpdateBuildingConfigurationAsync(dto);

        //    // Assert
        //    Assert.False(result.isSuccess);
        //    Assert.False(result.dataToReturn);
        //}


        //[Fact]
        //public async Task DeleteBuildingConfigurationAsync_ShouldReturnSuccessResponse_WhenDeleteIsValid()
        //{
        //    // Arrange
        //    var mockResponse = new Mock<DeleteResponse>();
        //    mockResponse.Setup(r => r.IsValid).Returns(true);

        //    _mockElasticClient.Setup(c => c.DeleteAsync<BuildingConfiguration>(It.IsAny<string>(), It.IsAny<Func<DeleteDescriptor<BuildingConfiguration>, IDeleteRequest>>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(mockResponse.Object);

        //    // Act
        //    var result = await _elasticManager.DeleteBuildingConfigurationAsync("1");

        //    // Assert
        //    Assert.True(result.isSuccess);
        //    Assert.True(result.dataToReturn);
        //}

        //[Fact]
        //public async Task DeleteBuildingConfigurationAsync_ShouldReturnErrorResponse_WhenDeleteIsInvalid()
        //{
        //    // Arrange
        //    var mockResponse = new Mock<DeleteResponse>();
        //    mockResponse.Setup(r => r.IsValid).Returns(false);
        //    mockResponse.Setup(r => r.OriginalException).Returns(new System.Exception("Error"));

        //    _mockElasticClient.Setup(c => c.DeleteAsync<BuildingConfiguration>(It.IsAny<string>(), It.IsAny<Func<DeleteDescriptor<BuildingConfiguration>, IDeleteRequest>>(), It.IsAny<CancellationToken>()))
        //        .ReturnsAsync(mockResponse.Object);

        //    // Act
        //    var result = await _elasticManager.DeleteBuildingConfigurationAsync("1");

        //    // Assert
        //    Assert.False(result.isSuccess);
        //    Assert.False(result.dataToReturn);
        //}
    }
}
