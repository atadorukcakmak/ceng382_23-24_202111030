using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Diagnostics;
using System.Text.Json.Serialization;
using Microsoft.VisualBasic;


public class Program
{
    public static void Main()
    {
        string jsonFilePath = "ReservationData.json";
        ReservationService.InitializeReservations(jsonFilePath);

        ReservationService.DisplayReservationByReserver("Ata");
        ReservationService.DisplayReservationByRoomId("002");
        
        
       
            
            ReservationHandler reservationHandler = new ReservationHandler();
            
            RoomHandler roomHandler= new RoomHandler
            {
                reservationHandler = reservationHandler,
                DateTime = "2024-05-23",
                ReserverName = "Ata",
            };
            Room room = new Room("002", "A-103", 4);
            roomHandler.manageRoom(room,true);

            

            RoomHandler roomHandler1= new RoomHandler
            {
                reservationHandler = reservationHandler,
                DateTime = "2024-05-27",
                ReserverName = "Efe",
            };
            Room room1 = new Room("001", "A-102", 3);
            roomHandler1.manageRoom(room1,true);
            


            RoomHandler roomHandler2= new RoomHandler
            {
                reservationHandler = reservationHandler,
                DateTime = "2024-05-20",
                ReserverName = "Fatih Terim",
            };
            Room room2 = new Room("010", "A-110", 1);
            roomHandler2.manageRoom(room2,true);

            //removing reservation2
            roomHandler2.manageRoom(room2,false);

            RoomHandler roomHandler3= new RoomHandler
            {
                reservationHandler = reservationHandler,
                DateTime = "2024-05-25",
                ReserverName = "Arda",
            };
            Room room3 = new Room("009", "A-106", 4);
            roomHandler3.manageRoom(room3,true);


            RoomHandler roomHandler4= new RoomHandler
            {
                reservationHandler = reservationHandler,
                DateTime = "2024-05-19",
                ReserverName = "Emre",
            };
            Room room4 = new Room("011", "A-117", 2);
            roomHandler4.manageRoom(room4,true);



        ReservationService .PrintReservations();
        DateTime firstTime = new DateTime(2024, 5, 28, 13, 31, 39, 8540); 
        DateTime lastTime = new DateTime(2024, 5, 28, 13, 31, 39, 9306);   


        LogService.DisplayLogs(firstTime, lastTime);
        LogService.DisplayLogsByName("Efe");
    }
}
