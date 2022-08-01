namespace dotnetnbpgold.tests;
using dotnetnbpgold.nbp.client;
using dotnetnbpgold.nbp.client.Models.Settings;
using Microsoft.Extensions.Options;

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
        var temp = true;
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = new DateTime(2022, 1, 1);

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrow_Exception_When_EndDateIsInTheFuture()
    {
        // Arrange.
        var temp = true;
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = new DateTime(2022, 1, 1);

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrow_Exception_When_EndDateIsBeforeStartDate()
    {
        // Arrange.
        var temp = true;
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = new DateTime(2022, 1, 1);

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrow_Exception_When_EndDateIsBeforeMinimumDate()
    {
        // Arrange.
        var temp = true;
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = new DateTime(2022, 1, 1);

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrow_Exception_When_PeriodIsLongerThanMaximumPeriodDays()
    {
        // Arrange.
        var temp = true;
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = new DateTime(2022, 1, 1);

        // Act.

        // Assert.
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldReturn_ListOfNBPGoldDatePriceResponse()
    {
        // Arrange.
        var temp = true;
        DateTime startDate = new DateTime(2022, 1, 1);
        DateTime endDate = new DateTime(2022, 1, 1);

        // Act.

        // Assert.
    }
}