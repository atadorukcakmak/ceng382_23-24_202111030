using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using loginDemo.Models;
using System;
using System.Threading.Tasks;
using System.Security.Claims;
using loginDemo.Data;

namespace loginDemo.Pages
{
    public class MakePaymentModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MakePaymentModel(ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public decimal? Amount { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnPostAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            // Assuming a reservation ID is passed through the URL
            if (Request.Query.ContainsKey("reservationId"))
            {
                int reservationId = int.Parse(Request.Query["reservationId"]);

                var payment = new Payment
                {
                    ReservationId = reservationId,
                    UserId = userId,
                    Amount = Amount,
                    PaymentDate = DateTime.Now,
                    Status = "Completed"
                };

                _context.Payments.Add(payment);
                await _context.SaveChangesAsync();

                return RedirectToPage("MyReservations");
            }

            ErrorMessage = "Invalid request.";
            return Page();
        }
    }
}
