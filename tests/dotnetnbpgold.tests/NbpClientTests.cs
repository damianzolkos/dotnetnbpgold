using dotnetnbpgold.nbp.client;
using dotnetnbpgold.nbp.client.Models.Settings;
using Microsoft.Extensions.Options;

namespace dotnetnbpgold.tests;

public class NbpClientTests
{
    private readonly DotNetNBPGoldClient _sut;
    public NbpClientTests()
    {
        DotNetNBPGoldClientSettings mockOptions = new DotNetNBPGoldClientSettings() { };
        IOptions<DotNetNBPGoldClientSettings> options = Options.Create(mockOptions);

        _sut = new DotNetNBPGoldClient(options);
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrow_Exception_When_StartDateIsInTheFuture()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(10);
        DateTime endDate = DateTime.Now.AddDays(5);

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrowException_When_EndDateIsInTheFuture()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-10);
        DateTime endDate = DateTime.Now.AddDays(5);

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrowException_When_EndDateIsBeforeStartDate()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-10);
        DateTime endDate = DateTime.Now.AddDays(-20);

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrowException_When_EndDateIsBeforeMinimumDate()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-10);
        DateTime endDate = new DateTime(1990, 1, 1);

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrowException_When_PeriodIsLongerThanMaximumPeriodDays()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-91);
        DateTime endDate = DateTime.Now;

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrowException_When_NbpApiReturnsNot200StatusCode()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-10);
        DateTime endDate = DateTime.Now;

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldReturn_ListOfNBPGoldDatePriceResponse()
    {
        // Arrange.
        DateTime startDate = DateTime.Now.AddDays(-10);
        DateTime endDate = DateTime.Now;

        // Act.

        // Assert.
    }
}