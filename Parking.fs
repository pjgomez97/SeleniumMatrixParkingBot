module ParkingBot.Parking

open Shade

type ParkingLocations =
    | Office
    | REDSA
    | VID

let selectParkingLocation location =
    let locationText =
        match location with
        | Office -> "MAL Office Parking"
        | REDSA -> "MAL REDSA Parking"
        | VID -> "MAL VID Parking"

    let locationCheckbox = getParent (elementByText locationText)

    if not (isSelected locationCheckbox) then
        locationCheckbox |> click

let showAsList () =
    elementByText "List view" |> getParent |> click

let showByDay () =
    elementByText "Day" |> getParent |> click

let spotsAvailable () =
    match elementsByText "Nothing available." with
    | e when Seq.isEmpty e -> true
    | _ -> false

let isLoading () =
    (element "svg.MuiCircularProgress-svg").Displayed
