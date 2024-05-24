using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using System.Linq;
using System.Text.Json.Nodes;

public static class ReservationRepository
{
    public static List<Reservation>? reservations;  
    public static List<Room>? rooms;
    public static string _jsonFilePath;


    public static void saveReservation()
    {
         try
        {
            if (_jsonFilePath != null)
            {
                string jsonString = JsonSerializer.Serialize(reservations, new JsonSerializerOptions
                {
                    WriteIndented = true,
                    PropertyNameCaseInsensitive = true
                });

                File.WriteAllText(_jsonFilePath, jsonString);
                LogHandler.handleLog("Reservations have been saved to the JSON file.");

            }
        }
        catch (Exception ex)
        {
            LogHandler.handleLog($"An error occurred while saving reservations: {ex.Message}");
        }
    }
        
    
   


    public static void loadReservations(string jsonFilePath)
    {
        try
        {
            if (File.Exists(jsonFilePath))
            {
                string jsonString = File.ReadAllText(jsonFilePath);


                if (string.IsNullOrWhiteSpace(jsonString))
                {
                    LogHandler.handleLog("JSON file is empty. An empty reservations list is initialized");
                    reservations = new List<Reservation>();
                }    
                
                else
                {
                    reservations = JsonSerializer.Deserialize<List<Reservation>>(jsonString, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true
                    }) ?? new List<Reservation>();
                    LogHandler.handleLog("Reservations have been loaded from the JSON file.");
                
                }
            
            
            }
            else
            {
                LogHandler.handleLog("JSON file not found. Initializing an empty reservations list.");
                reservations = new List<Reservation>();
            }
        }
        catch (JsonException ex)
        {
            LogHandler.handleLog($"An error occurred while deserializing the JSON: {ex.Message}");
            reservations = new List<Reservation>();
        }

    }

}
