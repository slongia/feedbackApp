// Domain/Factory/IInvoiceFactory.cs
public interface IInvoiceFactory
{
    Invoice CreateInvoice(int customerId, string type, IEnumerable<(string desc, int qty, decimal price)> lines);
}