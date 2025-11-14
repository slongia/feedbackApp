// Domain/Factory/IInvoiceFactory.cs
using feedbackApp.Domain.Entities;
public interface IInvoiceFactory
{
    Invoice CreateInvoice(int customerId, string type, IEnumerable<(string desc, int qty, decimal price)> lines);
}