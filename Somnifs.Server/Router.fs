namespace Somnifs.Server

open Saturn
open Giraffe.ResponseWriters

module Router = 
    let api = pipeline {
        plug acceptJson
    }

    let apiRouter = router {
        not_found_handler (text "Api 404")
        pipe_through api

        get "/" (text "Hello world!")
    }

    let appRouter = router {
        forward "/api" apiRouter
    }