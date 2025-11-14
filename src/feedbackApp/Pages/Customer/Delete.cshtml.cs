using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using feedbackApp.Infrastructure.Data;
using feedbackApp.Domain.Entities;

namespace feedbackApp.Pages.Customer
{
    public class DeleteModel : PageModel
    {
        private readonly BillingDbContext _context;

        public DeleteModel(BillingDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public feedbackApp.Domain.Entities.Customer Customer { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id.Value);

            if (customer == null)
            {
                return NotFound();
            }

            Customer = customer;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var customer = await _context.Customers.FindAsync(id.Value);

            if (customer != null)
            {
                _context.Customers.Remove(customer);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
