// Domain/Repositories/IPaymentRepository.cs
using feedbackApp.Domain.Entities;
public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
}