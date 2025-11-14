using Microsoft.EntityFrameworkCore;
using Npgsql;
using feedbackApp.Infrastructure.Data;


var builder = WebApplication.CreateBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

builder.Services.AddDbContext<BillingDbContext>(options =>
options.UseNpgsql(connectionString)
);

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddControllers();
builder.Services.AddScoped<ICalculatorService, CalculatorService>();



// Strategy (discount strategies)
builder.Services.AddScoped<IDiscountStrategy, SeasonalDiscountStrategy>(); // default

// Repository + UnitOfWork
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();
builder.Services.AddScoped<IBillingUnitOfWork, BillingUnitOfWork>();

// Invoice calculator + Decorator
builder.Services.AddScoped<IInvoiceCalculator, BaseInvoiceCalculator>();
//builder.Services.Decorate<IInvoiceCalculator, LoggingInvoiceCalculatorDecorator>();

// Event dispatcher (Observer)
builder.Services.AddSingleton<IEventDispatcher, InMemoryEventDispatcher>();
builder.Services.AddScoped<IEventHandler<InvoicePaidEvent>, EmailOnInvoicePaidHandler>();
builder.Services.AddScoped<IEventHandler<InvoicePaidEvent>, LogOnInvoicePaidHandler>();

// Command handler
builder.Services.AddScoped<ICommandHandler<ApplyDiscountCommand>, ApplyDiscountCommandHandler>();

// Invoice services & generators
builder.Services.AddScoped<IInvoiceFactory, InvoiceFactory>();
builder.Services.AddScoped<InvoiceGeneratorTemplate, OnlineInvoiceGenerator>();

builder.Services.AddScoped<BillingLogger>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();
app.MapRazorPages()
   .WithStaticAssets();
app.MapControllers();

app.Run();

//it gives WebApplicationFactory<Program> a concrete Program type to locate the entry point.
public partial class Program { }