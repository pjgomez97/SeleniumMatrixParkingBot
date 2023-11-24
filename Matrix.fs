module ParkingBot.Matrix

open System
open Location
open Logger
open Shade

let MATRIX_URL = "https://app.matrixbooking.com/"

let accessMatrix () =
    start "Chrome"
    
    Console.Clear()

    logInfo $"Accessing %s{MATRIX_URL}"

    url MATRIX_URL

let exitMatrix () =
    logInfo "Exiting..."
    quit ()

let login () =
    let username = Environment.GetEnvironmentVariable "MATRIX_USER"

    let password = Environment.GetEnvironmentVariable "MATRIX_PASSWORD"

    if username = null || password = null then
        logError "Error. You must set MATRIX_USER and MATRIX_PASSWORD environment variables"
        logError "Exiting..."
        exit -1
        
    logInfo $"Trying to log in with username %s{username}"

    element "#username" |> write username

    element "#password" |> write password

    elementByTextAndType "Log in" ElementType.Button |> click
    
    try
        elementByText "Welcome" |> ignore
        logInfo "Successfully logged in"
    with
    | :? OpenQA.Selenium.NoSuchElementException ->
        logError "Error while logging. Review the credentials and check if the site is up"
        exitMatrix ()
        exit -1

let goToLocation location =
    let locationText =
        match location with
        | MalOffice -> "MAL Office"
        | MalParking -> "MAL Parking"

    logInfo $"Accessing %s{locationText}"

    elementByText locationText |> click

let inputDate date =
    logInfo $"Inserting date %s{date}"

    element "#dateInputFrom" |> write date

let search () =
    elementByText "Submit search to" |> getParent |> click
