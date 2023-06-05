namespace Dah

open Avalonia.FuncUI.DSL

module Editor =
    open Avalonia.Controls
    open Avalonia.FuncUI.Types
    open Avalonia.Layout
    open Avalonia.Input
    open Avalonia.Interactivity

    let title = "Dah Editor"

    type Mode =
        | Normal
        | Insert

    type State = { mode: Mode; buffer: string }

    let init () = { mode = Normal; buffer = "" }

    type Msg =
        | Nothing
        | EnterInsert
        | EnterNormal
        | BufferUpdate of string

    let update (msg: Msg) (state: State) : State =
        match msg with
        | Nothing -> state
        | EnterInsert -> { state with mode = Insert }
        | EnterNormal -> { state with mode = Normal }
        | BufferUpdate b -> { state with buffer = b }

    let handleNormalKeyDown (keyEvent: KeyEventArgs) =
        match keyEvent.Key with
        | Key.I ->
            keyEvent.Handled <- true
            EnterInsert
        | x -> Nothing

    let handleInsertKeyDown (keyEvent: KeyEventArgs) =
        match keyEvent.Key with
        | Key.Escape ->
            keyEvent.Handled <- true
            EnterNormal
        | _ -> Nothing

    let handleKeyDown (mode: Mode) (keyEvent: KeyEventArgs) =
        match mode with
        | Normal -> handleNormalKeyDown keyEvent
        | Insert -> handleInsertKeyDown keyEvent

    let view (state: State) (dispatch: Msg -> unit) : IView =
        TextBox.create [ TextBox.text state.buffer
                         TextBox.onKeyDown (handleKeyDown state.mode >> dispatch)
                         TextBox.onTextChanged (BufferUpdate >> dispatch)
                         TextBox.isReadOnly (state.mode = Normal) ]
