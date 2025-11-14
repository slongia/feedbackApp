using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using feedbackApp.Helpers;
using feedbackApp.Domain.Entities;
using feedbackApp.Infrastructure.Data;


namespace feedbackApp.Pages.Customer;

public class IndexModel : PageModel
{
    private readonly BillingDbContext _context;

    public IndexModel(BillingDbContext context)
    {
        _context = context;
    }

    public PaginatedList<feedbackApp.Domain.Entities.Customer> Customers { get; set; } = default!;

    // Sorting state
    public string NameSort { get; set; } = default!;
    public string EmailSort { get; set; } = default!;
    public string LoyaltySort { get; set; } = default!;
    public string CurrentSort { get; set; } = default!;

    // Filtering state
    public string CurrentFilter { get; set; } = default!;

    public async Task OnGetAsync(string? sortOrder, string? currentFilter,
                                 string? searchString, int? pageIndex)
    {
        CurrentSort = sortOrder ?? string.Empty;

        NameSort = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
        EmailSort = sortOrder == "email" ? "email_desc" : "email";
        LoyaltySort = sortOrder == "loyalty" ? "loyalty_desc" : "loyalty";

        if (searchString != null)
        {
            pageIndex = 1;
        }
        else
        {
            searchString = currentFilter;
        }

        CurrentFilter = searchString ?? string.Empty;

        IQueryable<feedbackApp.Domain.Entities.Customer> customersIQ = _context.Customers.AsNoTracking();

        // Filter (search by Name or Email)
        if (!string.IsNullOrWhiteSpace(searchString))
        {
            customersIQ = customersIQ.Where(c =>
                EF.Functions.ILike(c.Name, $"%{searchString}%") ||
                EF.Functions.ILike(c.Email, $"%{searchString}%"));
        }

        // Sort
        customersIQ = sortOrder switch
        {
            "name_desc" => customersIQ.OrderByDescending(c => c.Name),
            "email" => customersIQ.OrderBy(c => c.Email),
            "email_desc" => customersIQ.OrderByDescending(c => c.Email),
            "loyalty" => customersIQ.OrderBy(c => c.LoyaltyLevel),
            "loyalty_desc" => customersIQ.OrderByDescending(c => c.LoyaltyLevel),
            _ => customersIQ.OrderBy(c => c.Name),
        };

        const int pageSize = 10;
        Customers = await PaginatedList<feedbackApp.Domain.Entities.Customer>.CreateAsync(
            customersIQ, pageIndex ?? 1, pageSize);
    }
}

