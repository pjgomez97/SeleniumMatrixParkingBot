open System.Threading
open ParkingBot.Book
open ParkingBot.Matrix
open ParkingBot.Parking
open ParkingBot.ParkingSpot

[<EntryPoint>]
let main argv =

    accessMatrix ()

    login ()

    bookParking "21/10/2023" Office Building

    Thread.Sleep(2000)

    exitMatrix ()

    0
