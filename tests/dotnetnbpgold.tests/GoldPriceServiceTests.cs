using dotnetnbpgold.db.Entities;
using dotnetnbpgold.db.Repositories;
using dotnetnbpgold.nbp.client;
using dotnetnbpgold.nbp.client.Models.NBP.Responses;
using dotnetnbpgold.web.Services;
using dotnetnbpgold.web.Models.ViewModels;
using Microsoft.Extensions.Logging;
using NSubstitute.ExceptionExtensions;

namespace dotnetnbpgold.tests
{
    public class GoldPriceServiceTests
    {
        private readonly IGoldPriceService _sut;
        private readonly IDotNetNBPGoldClient _nbpClientMock = Substitute.For<IDotNetNBPGoldClient>();
        private readonly IGoldPriceRepository _goldPriceRepositoryMock = Substitute.For<IGoldPriceRepository>();
        private readonly IFileService _fileServiceMock = Substitute.For<IFileService>();
        private readonly ILogger<GoldPriceService> _loggerMock = Substitute.For<ILogger<GoldPriceService>>();

        public GoldPriceServiceTests()
        {
            _sut = new GoldPriceService(_nbpClientMock, _goldPriceRepositoryMock, _fileServiceMock, _loggerMock);

            _fileServiceMock.SaveTextFileAsync(Arg.Any<string>(), Arg.Any<string>(), Arg.Any<string>());
            _goldPriceRepositoryMock.AddAsync(Arg.Any<GoldPrice>());
        }

        [Fact]
        public async Task GoldPriceService_GetGoldPricesAsync_ShouldReturn_Message_When_ThereIsZeroGoldPrices()
        {
            // Arrange.
            DateTime startDate = DateTime.Now.AddDays(10);
            DateTime endDate = DateTime.Now.AddDays(5);

            _nbpClientMock.GetGoldPricesAsync(startDate, endDate).Returns(new List<NBPGoldDatePriceResponse>());

            // Act.
            var result = await _sut.GetForViewAsync(startDate, endDate);

            // Assert.
            result.Should().NotBeNull();
            result.StartDateGoldPrice.Should().BeNull();
            result.EndDateGoldPrice.Should().BeNull();
            result.Average.Should().Be(0);
            result.ErrorMessage.Should().Be("No gold prices for the selected period were found.");
        }   

        [Fact]
        public async Task GoldPriceService_GetGoldPricesAsync_ShouldReturn_Response_When_ThereAreGoldPrices()
        {
            // Arrange.
            DateTime startDate = DateTime.Now.AddDays(-10);
            DateTime endDate = DateTime.Now.AddDays(-5);

            var goldPrices = new List<NBPGoldDatePriceResponse>()
            { 
                new NBPGoldDatePriceResponse() { Date = "2022-01-01", Price = 100 },
                new NBPGoldDatePriceResponse() { Date = "2022-01-02", Price = 200 },
                new NBPGoldDatePriceResponse() { Date = "2022-01-03", Price = 300 },
            };

            _nbpClientMock.GetGoldPricesAsync(startDate, endDate).Returns(goldPrices);

            // Act.
            var result = await _sut.GetForViewAsync(startDate, endDate);

            // Assert.
            result.Should().NotBeNull();
            result.StartDateGoldPrice.Should().NotBeNull();
            result.EndDateGoldPrice.Should().NotBeNull();
            result.Average.Should().NotBe(0);
            result.Average.Should().Be(200);
            result.ErrorMessage.Should().BeNullOrEmpty();
        } 

        [Fact]
        public async Task GoldPriceService_GetGoldPricesAsync_ShouldReturn_ErrorMessage_When_SomethingWasWrong()
        {
            // Arrange.
            DateTime startDate = DateTime.Now.AddDays(-10);
            DateTime endDate = DateTime.Now.AddDays(-5);

            _nbpClientMock.GetGoldPricesAsync(startDate, endDate).Throws<Exception>();

            // Act.
            var result = await _sut.GetForViewAsync(startDate, endDate);

            // Assert.
            result.ErrorMessage.Should().NotBeNull();
            result.ErrorMessage.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public async Task GoldPriceService_GetForListViewAsync_ShouldReturn_Response_When_ThereAreGoldPricesInDB()
        {
            // Arrange.
            var goldPricesFromDB = new List<GoldPrice>()
            { 
                new GoldPrice() { EndDate = DateTime.Now, StartDate = DateTime.Now.AddDays(-1), Average = 100, AddedAt = DateTime.Now.AddHours(-1), Id = 1 },
                new GoldPrice() { EndDate = DateTime.Now, StartDate = DateTime.Now.AddDays(-2), Average = 200, AddedAt = DateTime.Now.AddHours(-2), Id = 2 },
                new GoldPrice() { EndDate = DateTime.Now, StartDate = DateTime.Now.AddDays(-3), Average = 300, AddedAt = DateTime.Now.AddHours(-3), Id = 3 },
            };

            _goldPriceRepositoryMock.GetAllAsync().Returns(goldPricesFromDB);

            // Act.
            var result = await _sut.GetForListViewAsync();

            // Assert.
            result.Should().NotBeNull();
            result.Count.Should().Be(goldPricesFromDB.Count);
        } 
    }
}