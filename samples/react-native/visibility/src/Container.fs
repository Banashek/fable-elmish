﻿module Container

open Fable.Elmish

module c = Counter

// Model
type Model =
  { Top : c.Model
    Bottom : c.Model }

let init() =
  { Top = c.init(true)
    Bottom = c.init(true) }, Cmd.none

// Update
type Msg =
  | Reset
  | Top of c.Msg
  | Bottom of c.Msg

let update msg model : Model*Cmd<Msg>=
  match msg with
  | Reset -> {Top = c.init(true); Bottom = c.init(true)}, []
  | Top cmd ->
    let res = c.update cmd model.Top
    {model with Top = res; Bottom = {model.Bottom with visible = (model.Top.count % 2 = 0) }}, []
  | Bottom cmd ->
    let res = c.update cmd model.Bottom
    {model with Bottom = res}, []

// View
module R = Fable.Helpers.ReactNative

let view model (dispatch: Dispatch<Msg>) =
  R.view []
    [ c.view model.Top (Top >> dispatch)
      c.view model.Bottom (Bottom >> dispatch)
      Styles.button "Reset" (fun _ -> dispatch Reset) ]
