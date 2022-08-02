using dotnetnbpgold.nbp.client;
using dotnetnbpgold.nbp.client.Models.Settings;
using dotnetnbpgold.nbp.client.Exceptions;
using System.Net;
using dotnetnbpgold.tests.Mocks;
using dotnetnbpgold.nbp.client.Models.NBP.Responses;
using System.Text.Json;

namespace dotnetnbpgold.tests;

public class NbpClientTests
{
    private readonly DotNetNBPGoldClient _sut;
    private readonly IOptions<DotNetNBPGoldClientSettings> _options;

    public NbpClientTests()
    {
        DotNetNBPGoldClientSettings mockOptions = new DotNetNBPGoldClientSettings() { };
        _options = Options.Create(mockOptions);

        _sut = new DotNetNBPGoldClient(_options, new HttpClient());
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrow_Exception_When_StartDateIsInTheFuture()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(10);
        DateTime endDate = DateTime.Now.AddDays(5);

        // Act.
        var result = async () => await _sut.GetGoldPricesAsync(startDate, endDate);

        // Assert.
        result.Should().ThrowAsync<DotNetNBPGoldClientException>();
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrowException_When_EndDateIsInTheFuture()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-10);
        DateTime endDate = DateTime.Now.AddDays(5);

        // Act.
        var result = async () => await _sut.GetGoldPricesAsync(startDate, endDate);

        // Assert.
        result.Should().ThrowAsync<DotNetNBPGoldClientException>();
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrowException_When_EndDateIsBeforeStartDate()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-10);
        DateTime endDate = DateTime.Now.AddDays(-20);

        // Act.
        var result = async () => await _sut.GetGoldPricesAsync(startDate, endDate);

        // Assert.
        result.Should().ThrowAsync<DotNetNBPGoldClientException>();
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrowException_When_EndDateIsBeforeMinimumDate()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-10);
        DateTime endDate = new DateTime(1990, 1, 1);

        // Act.
        var result = async () => await _sut.GetGoldPricesAsync(startDate, endDate);

        // Assert.
        result.Should().ThrowAsync<DotNetNBPGoldClientException>();
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrowException_When_PeriodIsLongerThanMaximumPeriodDays()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-91);
        DateTime endDate = DateTime.Now;

        // Act.
        var result = async () => await _sut.GetGoldPricesAsync(startDate, endDate);

        // Assert.
        result.Should().ThrowAsync<DotNetNBPGoldClientException>();
    }

    [Fact]
    public async Task DotNetNBPGoldClient_ShouldReturnListOfDTOs_When_NbpApiReturnsDataAndOkStatusCode()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-10);
        DateTime endDate = DateTime.Now;

        var response = new List<NBPGoldDatePriceResponse>()
        {
            new NBPGoldDatePriceResponse() { Price = 100, Date = "2020-01-01"},
            new NBPGoldDatePriceResponse() { Price = 200, Date = "2020-01-02"},
            new NBPGoldDatePriceResponse() { Price = 300, Date = "2020-01-03"}
        };
        var responseJsonString = JsonSerializer.Serialize(response);
        var messageHandler = new HttpMessageHandlerMock(responseJsonString, HttpStatusCode.OK);
        var httpClientMock = new HttpClient(messageHandler);
        var sut = new DotNetNBPGoldClient(_options, httpClientMock);

        // Act.
        var result = await sut.GetGoldPricesAsync(startDate, endDate);

        // Assert.
        result.Should().NotBeNull();
        result.Count.Should().Be(response.Count);
    }

    [Theory]
    [InlineData(HttpStatusCode.BadRequest)]
    [InlineData(HttpStatusCode.BadGateway)]
    [InlineData(HttpStatusCode.Forbidden)]
    [InlineData(HttpStatusCode.NotFound)]
    public void DotNetNBPGoldClient_ShouldThrowException_When_NbpApiReturnsNonOkStatusCode(HttpStatusCode statusCode)
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-10);
        DateTime endDate = DateTime.Now;

        var response = new List<NBPGoldDatePriceResponse>();
        var responseJsonString = JsonSerializer.Serialize(response);
        var messageHandler = new HttpMessageHandlerMock(responseJsonString, statusCode);
        var httpClientMock = new HttpClient(messageHandler);
        var sut = new DotNetNBPGoldClient(_options, httpClientMock);

        // Act.
        var result = async () => await sut.GetGoldPricesAsync(startDate, endDate);

        // Assert.
        result.Should().ThrowAsync<DotNetNBPGoldClientException>();
    }
}