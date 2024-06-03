using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using loginDemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using loginDemo.Data;

namespace loginDemo.Pages
{
    [Authorize]
    public class MyReservationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public MyReservationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public List<Reservation>? Reservations { get; set; }

        public async Task OnGetAsync()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            Reservations = await _context.Reservations
                .Include(r => r.Room)
                .Where(r => r.UserId == userId)
                .ToListAsync();
        }

        public async Task<IActionResult> OnPostCancelAsync(int id)
        {
            var reservation = await _context.Reservations.FindAsync(id);

            if (reservation != null)
            {
                reservation.IsCancelled = true;
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
