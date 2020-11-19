module Main

open Fable.Core.JsInterop
open Browser.Dom
open Feliz

importAll "./styles/main.scss"


ReactDOM.render(App.counter(), document.getElementById "feliz-app")