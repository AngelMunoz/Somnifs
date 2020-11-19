namespace Somnifs.Server

open Saturn
open Giraffe.ResponseWriters
open Fable.SignalR
open Somnifs.Library

module Program =
    let endpointPipe =
        pipeline {
            plug head
            plug requestId
        }

    let app =
        application {
            use_signalr
                (configure_signalr {
                    endpoint Endpoints.Root
                    send Hub.send
                    invoke Hub.invoke
                 })
            use_cors "Any" (fun policy ->
                policy.WithOrigins("http://localhost:8080", "http://127.0.0.1:8080").AllowAnyMethod().AllowAnyHeader().AllowCredentials()
                |> ignore)
            pipe_through endpointPipe
            error_handler (fun ex _ -> text ex.Message)
            use_router Router.appRouter
            url "http://0.0.0.0:4000"
            memory_cache
            use_gzip
        }

    [<EntryPoint>]
    let main _ =
        printfn "Working directory - %s" (System.IO.Directory.GetCurrentDirectory())
        run app
        0 // return an integer exit code
