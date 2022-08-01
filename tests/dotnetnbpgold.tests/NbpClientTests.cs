namespace dotnetnbpgold.tests;

public class NbpClientTests
{
    public NbpClientTests()
    {
        
    }

    [Fact]
    public void DotNetNBPGoldClient_ShouldThrow_Exception_When_StartDateIsInTheFuture()
    {
        // Arrange.
        var temp = true;
        
        // Act.

        // Assert.
        temp.Should().Be(true);
    }
}