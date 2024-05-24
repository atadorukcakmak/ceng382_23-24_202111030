using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;


public class ReservationHandler
{
    public void bookRoom(Reservation reservation) 
    {
        ReservationService.reserveRoom(reservation);
        
    }

    public void removeBooking(Reservation reservation) 
    {
        ReservationService.deleteReservation(reservation);
        
    }


}