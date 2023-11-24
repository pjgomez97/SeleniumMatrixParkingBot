module ParkingBot.Book

open System.Threading
open Location
open Logger
open Matrix
open Parking
open ParkingSpot
open Shade

let waitUntilSpotsAvailable spotType =
    while not (spotsAvailable () || Seq.length (getSpots spotType) = 0) do
        logWarning $"No spots of type %s{spotType.ToString()} available. Retrying..."

        Thread.Sleep(3000)

        refresh ()

        showAsList ()

        while isLoading () do
            Thread.Sleep(1000)

        showByDay ()

let bookParking date location spotType =
    goToLocation MalParking

    inputDate date

    search ()

    selectParkingLocation location

    showAsList ()

    while isLoading () do
        Thread.Sleep(1000)

    showByDay ()

    waitUntilSpotsAvailable spotType

    selectRandomSpot spotType
