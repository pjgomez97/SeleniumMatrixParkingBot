module ParkingBot.ParkingSpot

open System
open Logger
open Shade

type OfficeSpotTypes =
    | Building
    | Exterior

let getSpots spotType =
    let spotText =
        match spotType with
        | Building -> "Building"
        | Exterior -> "Exterior"
        
    logInfo $"Trying to find spots in %s{spotText}"

    elementsByTextAndType spotText Span
    |> Seq.filter (fun e -> e.GetAttribute("class").Contains("checkbox-button-label"))

let insertPlateNumber () =
    let number = Environment.GetEnvironmentVariable "MATRIX_PLATE"

    if number = null then
        logError "Error. You must set MATRIX_PLATE environment variable"
        logError "Exiting..."
        exit -1

    logInfo $"Inserting plate number: %s{number}"

    element "#title" |> write number

let book () =
    elementByTextAndType "Book" Span |> getParent |> click

    insertPlateNumber ()

//elementByTextAndType "Book" Span |> getParent |> click

let selectRandomSpot spotType =
    let spots = getSpots spotType

    logInfo $"Found %d{Seq.length spots} spots of type %s{spotType.ToString()}"

    let spotNumber = Random().Next(0, Seq.length spots)

    let spot = spots |> Seq.item spotNumber
    
    logInfo $"Trying to book %s{spot.Text}"

    getParent spot |> click

    book ()
