using Microsoft.Extensions.DependencyInjection;

namespace feedbackApp.Tests;

public class CalculatorServiceTests
{
    [Fact]
    public void Add_ReturnCorrectSum()
    {
        //arrange
        var service = new CalculatorService();

        //act
        var result = service.Add(2, 3);

        //assert
        Assert.Equal(5, result);

    }
    [Theory]
    [InlineData(2, 3, 6)]
    [InlineData(-1, 5, -5)]
    [InlineData(0, 10, 0)]
    public void Multiply_ReturnCorrectProduct(int a, int b, int expected)
    {
        //arrange
        var service = new CalculatorService();

        //act
        var result = service.Multiply(a, b);

        //assert
        Assert.Equal(expected, result);

    }
}
