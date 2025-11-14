// Domain/Repositories/IInvoiceRepository.cs
using feedbackApp.Domain.Entities;
public interface IInvoiceRepository
{
    Task<Invoice?> GetByIdAsync(int id);
    Task AddAsync(Invoice invoice);
    Task UpdateAsync(Invoice invoice);
}


