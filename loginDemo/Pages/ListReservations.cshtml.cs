using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using loginDemo.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using loginDemo.Data;

namespace loginDemo.Pages
{
    [Authorize]
    public class ListReservationsModel : PageModel
    {
        private readonly ApplicationDbContext _context;

        public List<Room>? Rooms { get; set; }
        public List<Room>? AvailableRooms { get; set; }
        public string? ErrorMessage { get; set; }

        public ListReservationsModel(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task OnGetAsync(DateTime? SearchStartTime, DateTime? SearchEndTime)
        {
            Rooms = await _context.Rooms.Include(r => r.Reservations).ToListAsync();

            if (SearchStartTime.HasValue && SearchEndTime.HasValue)
            {
                AvailableRooms = await _context.Rooms
                    .Where(r => !r.Reservations.Any(res =>
                        (SearchStartTime < res.EndTime && SearchEndTime > res.StartTime) && !res.IsCancelled))
                    .ToListAsync();
            }
        }

        public async Task<IActionResult> OnPostReserveAsync(int roomId, DateTime startTime, DateTime endTime)
        {
            if (roomId == 0 || startTime >= endTime)
            {
                ErrorMessage = "Invalid input.";
                return Page();
            }

            var reservation = new Reservation
            {
                RoomId = roomId,
                UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
                StartTime = startTime,
                EndTime = endTime,
                IsCancelled = false
            };

            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();

            return RedirectToPage();
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
