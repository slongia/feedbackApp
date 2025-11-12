public interface ICalculatorService
{
    int Add(int a, int b);
    int Multiply(int a, int b);
}

public class CalculatorService : ICalculatorService
{

    public int Add(int a, int b) => a + b;

    public int Multiply(int a, int b) => a * b;


}