module App

open Feliz
open Elmish
open Fable.SignalR
open Fable.SignalR.Feliz
open Somnifs.Library

let counter =
    React.functionComponent (fun () ->
        let (count, setCount) = React.useState (0)

        let hub =
            React.useSignalR<Action, Response> (fun hub ->
                hub.withUrl(sprintf "%s%s" Endpoints.BaseUrl Endpoints.Root).withAutomaticReconnect().configureLogging(LogLevel.Debug).onMessage
                <| function
                | Response.NewCount i -> setCount i)

        Html.div [ Html.h1 count
                   Html.button [ prop.text "Increment"
                                 prop.onClick
                                 <| fun _ -> hub.current.sendNow (Action.IncrementCount count) ]
                   Html.button [ prop.text "Decrement"
                                 prop.onClick
                                 <| fun _ -> hub.current.sendNow (Action.DecrementCount count) ]
                   Html.button [ prop.text "Reset"
                                 prop.onClick
                                 <| fun _ -> hub.current.sendNow (Action.Reset) ] ])
