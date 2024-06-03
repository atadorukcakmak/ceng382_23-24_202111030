using System;
using System.Collections.Generic;

namespace loginDemo.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Description { get; set; }
        public int Capacity { get; set; }
        public string? PhotoPath { get; set; }
        public List<Reservation>? Reservations { get; set; }
    }
    
}
