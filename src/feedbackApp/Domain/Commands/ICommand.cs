// Domain/Commands/ICommand.cs
public interface ICommand { }

public interface ICommandHandler<TCommand> where TCommand : ICommand
{
    Task HandleAsync(TCommand command);
}

// Domain/Commands/ApplyDiscountCommand.cs
public class ApplyDiscountCommand : ICommand
{
    public int InvoiceId { get; }
    public string DiscountCode { get; }

    public ApplyDiscountCommand(int invoiceId, string discountCode)
    {
        InvoiceId = invoiceId;
        DiscountCode = discountCode;
    }
}
