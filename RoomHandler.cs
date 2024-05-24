using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;


public class RoomHandler
{
    public ReservationHandler? reservationHandler { get; set; }
    public string? DateTime { get; set; }
    public string? ReserverName { get; set; }
    
    
    public void manageRoom(Room room, bool book) 
    {
        Reservation res = new Reservation(DateTime, ReserverName, room);

        if (book)
            reservationHandler?.bookRoom(res);
        else
            reservationHandler?.removeBooking(res);
        

    }


}