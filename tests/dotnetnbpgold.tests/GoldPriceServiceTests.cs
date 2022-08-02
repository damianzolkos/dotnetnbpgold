using dotnetnbpgold.db.Repositories;
using dotnetnbpgold.nbp.client;
using dotnetnbpgold.nbp.client.Models.NBP.Responses;
using dotnetnbpgold.web.Services;
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
        }

        [Fact]
        public async Task DotNetNBPGoldClient_ShouldReturn_Message_When_ThereIsZeroGoldPrices()
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
        public async Task DotNetNBPGoldClient_ShouldReturn_Response_When_ThereAreGoldPrices()
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
        public async Task DotNetNBPGoldClient_ShouldReturn_ErrorMessage_When_SomethingWasWrong()
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
    }
}