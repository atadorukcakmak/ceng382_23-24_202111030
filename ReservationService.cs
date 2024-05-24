using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Reflection.Metadata;

public static class ReservationService{
    private static List<Reservation> reservations=new List<Reservation>();
    
    public static void InitializeReservations(string jsonFilePath)
    {
    
        if (jsonFilePath!=null)
        {
            ReservationRepository._jsonFilePath=jsonFilePath;
            ReservationRepository.loadReservations(jsonFilePath);

            if (ReservationRepository.reservations!=null)
            {
                reservations = ReservationRepository.reservations;  
            }
            LogHandler.handleLog("Reservations have been initialized.");
        }
    }


    
    public static void reserveRoom(Reservation reservation)
    {
        ReservationRepository.reservations?.Add(reservation);
        ReservationRepository.saveReservation();
        LogHandler.handleLog($"New  reservation added: {reservation.ReserverName} for Room {reservation.Room.roomName} at {reservation.DateTime}");
    }

    public static void deleteReservation(Reservation reservation)
    {
        ReservationRepository.reservations?.Remove(reservation);
        ReservationRepository.saveReservation();
        LogHandler.handleLog($"New reservation removed: {reservation.ReserverName} for Room {reservation.Room.roomName} at {reservation.DateTime}");
    }


    public static void PrintReservations()
    {
        foreach (var reservation in reservations)
        {
            Console.WriteLine($"DataTime : {reservation.DateTime}, Reserver : {reservation.ReserverName}, Room : {reservation.Room} , Capacity : {reservation.Room.capacity}");
            
            LogHandler.handleLog("Reservations are printed.");
        
        }
    }

    private static void PrintReservations(List<Reservation> reservations)
    {
        foreach (var reservation in reservations)
        {
            Console.WriteLine($"DataTime : {reservation.DateTime}, Reserver : {reservation.ReserverName}, Room : {reservation.Room} , Capacity : {reservation.Room.capacity}");
            
        }
    }





    public static void DisplayReservationByReserver(string name)
    {
        var filteredReservations = filterByName(name);
        Console.WriteLine($"\n Reservations for {name}");
        PrintReservations(filteredReservations);

        LogHandler.handleLog($"Displayed reservations for reserver: {name}");
    }


    private static List<Reservation> filterByName ( string name)
    {
        var filteredReservations = reservations.Where(r => r.ReserverName.Equals(name,StringComparison.OrdinalIgnoreCase)).ToList();
        return filteredReservations;
    }





    public static void DisplayReservationByRoomId(string Id)
    {
        var filteredReservations = filterByRoomId(Id);
        Console.WriteLine($"\n Reservations for RoomId {Id}");
        PrintReservations(filteredReservations);

        LogHandler.handleLog($"Displayed reservations for Room ID: {Id}");

    }


    private static List<Reservation> filterByRoomId ( string Id)
    {
        var filteredReservations = reservations.Where(r => r.Room.roomId.Equals(Id,StringComparison.OrdinalIgnoreCase)).ToList();
        return filteredReservations;
    }


}