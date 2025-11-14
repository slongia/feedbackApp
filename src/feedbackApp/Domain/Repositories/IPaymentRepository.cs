// Domain/Repositories/IPaymentRepository.cs
public interface IPaymentRepository
{
    Task AddAsync(Payment payment);
}