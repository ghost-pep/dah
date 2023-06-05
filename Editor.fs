namespace Dah

open Avalonia.FuncUI.DSL

module Editor =
    open Avalonia.Controls
    open Avalonia.Layout

    let title = "Dah Editor"

    let init () = ()

    let update () () = ()

    let view () (dispatch) =
        TextBox.create [ TextBox.text "HI"
                         TextBox.onTextChanged (fun newText -> ()) ]
