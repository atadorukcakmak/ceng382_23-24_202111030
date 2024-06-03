using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using loginDemo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using loginDemo.Data;

namespace loginDemo.Pages
{
    public class ReserveRoomModel : PageModel
    {
        private readonly ApplicationDbContext _context; // Change to ApplicationDbContext

        public ReserveRoomModel(ApplicationDbContext context) // Change to ApplicationDbContext
        {
            _context = context;
        }

        [BindProperty]
        public int RoomId { get; set; }

        [BindProperty]
        public DateTime StartTime { get; set; }

        [BindProperty]
        public DateTime EndTime { get; set; }

        public List<SelectListItem>? Rooms { get; set; }

        public string? ErrorMessage { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            Rooms = await _context.Rooms.Select(r => new SelectListItem
            {
                Value = r.Id.ToString(),
                Text = r.Name
            }).ToListAsync();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (RoomId == 0 || StartTime >= EndTime)
            {
                ErrorMessage = "Invalid input.";
                return Page();
            }

            var reservation = new Reservation
            {
                RoomId = RoomId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                StartTime = StartTime,
                EndTime = EndTime,
                IsCancelled = false
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            //return RedirectToPage("ListReservations");
            return RedirectToPage("MakePayment", new { reservationId = reservation.Id });
        }
    }
}
