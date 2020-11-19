namespace Somnifs.Library

[<RequireQualifiedAccess>]
type Action =
    | IncrementCount of int
    | DecrementCount of int
    | Reset

[<RequireQualifiedAccess>]
type Response =
    | NewCount of int

module Endpoints =
    let [<Literal>] BaseUrl = "http://localhost:4000"
    let [<Literal>] Root = "/SignalR"