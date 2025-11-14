using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using feedbackApp.Domain.Entities;
using feedbackApp.Infrastructure.Data;
namespace feedbackApp.Pages.Customer;

public class CreateModel : PageModel
{
    private readonly BillingDbContext _context;

    public CreateModel(BillingDbContext context)
    {
        _context = context;
    }

    [BindProperty]
    public feedbackApp.Domain.Entities.Customer Customer { get; set; } = new feedbackApp.Domain.Entities.Customer();

    public void OnGet()
    {
    }

    public async Task<IActionResult> OnPostAsync()
    {
        if (!ModelState.IsValid)
        {
            return Page();
        }


        _context.Set<feedbackApp.Domain.Entities.Customer>().Add(this.Customer);
        await _context.SaveChangesAsync();

        return RedirectToPage("./Index");
    }
}

