namespace QuizApp

open System.Windows.Forms
open QuizApp.UI

module Main =
    [<EntryPoint>]
    let main _ =
        let form = QuizForm.createForm ()
        Application.Run(form)
