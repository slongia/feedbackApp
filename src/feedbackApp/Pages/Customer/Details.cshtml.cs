using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using feedbackApp.Infrastructure.Data;
using feedbackApp.Domain.Entities;

namespace feedbackApp.Pages.Customer
{
    public class DetailsModel : PageModel
    {
        private readonly BillingDbContext _context;

        public DetailsModel(BillingDbContext context)
        {
            _context = context;
        }

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
    }
}
