// Web/Controllers/InvoiceController.cs
using Microsoft.AspNetCore.Mvc;
using feedbackApp.Domain.Entities;
using feedbackApp.Infrastructure.Data;

[ApiController]
[Route("api/[controller]")]
public class InvoiceController : ControllerBase
{
    private readonly InvoiceGeneratorTemplate _generator;
    private readonly IBillingUnitOfWork _uow;
    private readonly IEventDispatcher _eventDispatcher;
    private readonly ICommandHandler<ApplyDiscountCommand> _applyDiscountHandler;

    public InvoiceController(
        InvoiceGeneratorTemplate generator,
        IBillingUnitOfWork uow,
        IEventDispatcher eventDispatcher,
        ICommandHandler<ApplyDiscountCommand> applyDiscountHandler)
    {
        _generator = generator;
        _uow = uow;
        _eventDispatcher = eventDispatcher;
        _applyDiscountHandler = applyDiscountHandler;
    }

    // Factory + Template + Strategy + Decorator + Repository + UoW
    [HttpPost("{customerId}/create")]
    public async Task<ActionResult> CreateInvoice(int customerId, [FromBody] CreateInvoiceDto dto)
    {
        var lines = dto.Lines.Select(l => (l.Description, l.Quantity, l.UnitPrice));
        var invoice = await _generator.GenerateAsync(customerId, lines, dto.InvoiceType);
        return Ok(invoice);
    }

    // Command + Strategy + Decorator + UoW
    [HttpPost("{invoiceId}/apply-discount")]
    public async Task<ActionResult> ApplyDiscount(int invoiceId, [FromBody] ApplyDiscountDto dto)
    {
        var command = new ApplyDiscountCommand(invoiceId, dto.DiscountCode);
        await _applyDiscountHandler.HandleAsync(command);
        return Ok();
    }

    // Observer (events) + UoW
    [HttpPost("{invoiceId}/pay")]
    public async Task<ActionResult> Pay(int invoiceId, [FromBody] PayInvoiceDto dto)
    {
        var invoice = await _uow.Invoices.GetByIdAsync(invoiceId);
        if (invoice == null)
            return NotFound();

        var payment = new Payment
        {
            InvoiceId = invoiceId,
            PaidAmount = dto.Amount,
            PaidAt = DateTime.UtcNow,
            PaymentMethod = dto.Method
        };

        await _uow.Payments.AddAsync(payment);
        invoice.Status = "Paid";
        await _uow.Invoices.UpdateAsync(invoice);
        await _uow.SaveChangesAsync();

        await _eventDispatcher.PublishAsync(new InvoicePaidEvent(invoiceId, dto.Amount));

        return Ok();
    }
}

// DTOs
public record CreateInvoiceDto(
    string InvoiceType,
    List<CreateInvoiceLineDto> Lines);

public record CreateInvoiceLineDto(
    string Description,
    int Quantity,
    decimal UnitPrice);

public record ApplyDiscountDto(string DiscountCode);
public record PayInvoiceDto(decimal Amount, string Method);
