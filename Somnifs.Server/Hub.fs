namespace Somnifs.Server

module Hub =
    open Fable.SignalR
    open FSharp.Control.Tasks.V2
    open Somnifs.Library

    let update (msg: Action) =
        match msg with
        | Action.IncrementCount i -> Response.NewCount(i + 1)
        | Action.DecrementCount i -> Response.NewCount(i - 1)
        | Action.Reset -> Response.NewCount(0)

    let invoke (msg: Action) (hubContext: FableHub) =
        task { return update msg }

    let send (msg: Action) (hubContext: FableHub<Action,Response>) =
        update msg
        |> hubContext.Clients.Caller.Send