using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;

public class RoomData
{
    [JsonPropertyName("Room")]
    public Room[]? Rooms {get; set;}
}


public class Room
{
    [JsonPropertyName("roomId")]
    public string? roomId{get; set;}

    [JsonPropertyName("roomName")]
    public string? roomName{get; set;}

    [JsonPropertyName("capacity")]
    public int? capacity{get; set;}
}

class Reservation
{
    public string? reserverName;
    public Room? room;
    public DateTime date;
}

class ReservationHandler
{
    private Reservation[,]? reservations = new Reservation[10,7]; 
    int row=0, column=0;

    public void addReservation(Reservation? res)
    {
        if(res!=null) 
        {
            switch(res.date.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    column=0;
                    break;
                case DayOfWeek.Tuesday:
                    column=1;
                    break;
                case DayOfWeek.Wednesday:
                    column=2;
                    break;
                case DayOfWeek.Thursday:
                    column=3;
                    break;
                case DayOfWeek.Friday:
                    column=4;
                    break;
                case DayOfWeek.Saturday:
                    column=5;
                    break;
                case DayOfWeek.Sunday:
                    column=6;
                    break;
            }

            switch(res.date.Hour)
            {
                case 8:
                    row=0;
                    break;
                case 9:
                    row=1;
                    break;
                case 10:
                    row=2;
                    break;
                case 11:
                    row=3;
                    break;
                case 12:
                    row=4;
                    break;
                case 13:
                    row=5;
                    break;
                case 14:
                    row=6;
                    break;
                case 15:
                    row=7;
                    break;
                case 16:
                    row=8;
                    break;
                case 17:
                    row=9;
                    break;
            }


            #pragma warning disable CS8602 // Dereference of a possibly null reference.
            reservations[row,column] = res; //zaten ilk çağırılan addReservation için 'reservations' null olmalı
            #pragma warning restore CS8602 // Dereference of a possibly null reference.

        }

    }

    public void deleteReservation(Reservation res)
    {
        if(reservations != null)
        {
            for(int i = 0 ; i<10 ; i++)
            {
                for(int k = 0 ; k<7 ; k++)
                {
                    if( reservations[i,k] == res)
                    {
                        reservations[i,k] = null;
                    }
                }
            }     
        }
       
    }

    public void displayWeeklySchedule()
    {   
        

        Console.Write("".PadRight(22));
        Console.Write("Monday  ".PadRight(17));
        Console.Write("Tuesday".PadRight(17));
        Console.Write("Wednesday".PadRight(17));
        Console.Write("Thursday".PadRight(17));
        Console.Write("Friday".PadRight(17));
        Console.Write("Saturday".PadRight(17));
        Console.Write("Sunday".PadRight(17));
        Console.WriteLine();

        for(int i = 0 ; i<10 ; i++)
        {
            string count = ""+(i+8);
            Console.Write(count.PadLeft(2)+".00"+"".PadRight(17));
            for(int k = 0 ; k<7 ; k++){
            
                if(reservations?[i,k] != null ) {
                    string info = reservations[i,k].reserverName+ ", " + reservations[i,k].room?.roomId + ", " + reservations[i,k].room?.roomName + "";
                    Console.Write(info.PadRight(17));
                }
                else{
                    Console.Write("- ".PadRight(17));
                }
            }
            Console.WriteLine();
        }
        
    

    }

}

class Program
{
    static void Main(string [] args)
    {
        
        //path to json
        string jsonFilePath = "Data.json";
        string jsonString = File.ReadAllText(jsonFilePath);

        //options to read 
        var options = new JsonSerializerOptions()
        {
            NumberHandling =JsonNumberHandling.AllowReadingFromString |
            JsonNumberHandling.WriteAsString
        };

        //read try catch
        try
        {
            var roomData =JsonSerializer.Deserialize<RoomData> (jsonString,options);
            //print
            if(roomData?.Rooms != null)
            {
                foreach(var room in roomData.Rooms)
                {
                Console.WriteLine ($"Room ID: {room.roomId}, Name: {room.roomName}, Capacity : {room.capacity}" );
                }
                Console.WriteLine();
            }
        }
        catch (Exception ex)
        {
            Console.Error.WriteLine("An error occurred: " + ex.Message);
        }

       
    
        #region 

        ReservationHandler reshandler = new ReservationHandler();

        Room room1 = new Room();
        room1.roomId = "001";
        room1.roomName = "A-101";

        Reservation reservation1 = new Reservation();
        reservation1.reserverName = "Messi";
        reservation1.room=room1;
        reservation1.date = new DateTime(2024, 4, 15, 9, 0, 0, 0);



        Room room2 = new Room();
        room2.roomId = "002";
        room2.roomName = "A-102";

        Reservation reservation2 = new Reservation();
        reservation2.reserverName = "Ronaldo";
        reservation2.room=room2;
        reservation2.date = new DateTime(2024, 4, 16, 11, 0, 0, 0);
        
        

        Room room3 = new Room();
        room3.roomId = "003";
        room3.roomName = "A-103";

        Reservation reservation3 = new Reservation();
        reservation3.reserverName = "Neymar";
        reservation3.room=room3;
        reservation3.date = new DateTime(2024, 4, 17, 13, 0, 0, 0);



        Room room4 = new Room();
        room4.roomId = "004";
        room4.roomName = "A-104";

        Reservation reservation4 = new Reservation();
        reservation4.reserverName = "Trossard";
        reservation4.room=room4;
        reservation4.date = new DateTime(2024, 4, 18, 15, 0, 0, 0);

        reshandler.addReservation(reservation1);
        reshandler.addReservation(reservation2);
        reshandler.addReservation(reservation3);
        reshandler.addReservation(reservation4);
        
        reshandler.displayWeeklySchedule();
        reshandler.deleteReservation(reservation2);
        reshandler.displayWeeklySchedule();
        
        
        
        #endregion

    }
}