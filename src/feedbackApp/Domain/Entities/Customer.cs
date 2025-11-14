using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
namespace feedbackApp.Domain.Entities;

[Index(nameof(Email), IsUnique = true)]
public class Customer
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Name is required.")]
    [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
    public string Name { get; set; } = default!;

    [Required(ErrorMessage = "Email is required.")]
    [EmailAddress(ErrorMessage = "Please enter a valid email address.")]
    [StringLength(200, ErrorMessage = "Email cannot be longer than 200 characters.")]
    public string Email { get; set; } = default!;

    [Required(ErrorMessage = "Loyalty level is required.")]
    [RegularExpression("Standard|Gold|Platinum",
        ErrorMessage = "Loyalty level must be Standard, Gold, or Platinum.")]
    public string LoyaltyLevel { get; set; } = "Standard";

    public ICollection<Invoice> Invoices { get; set; } = new List<Invoice>();
}
