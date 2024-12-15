namespace QuizApp.Data

open System.Collections.Generic

type Question = {
    ID: int
    Text: string
    Options: string list option
    CorrectAnswer: string
}

module QuizData =
    let getQuestions () =
        [
            // 3 Multiple-choice questions
            { ID = 1; Text = "Which programming language is used to write this quiz?"; Options = Some ["Python"; "C#"; "Java"; "F#"]; CorrectAnswer = "F#" }
            { ID = 2; Text = "Which keyword is used to define a function in F#?"; Options = Some ["fun"; "let"; "function"; "define"]; CorrectAnswer = "let" }
            { ID = 3; Text = "How do you declare a mutable variable in F#?"; Options = Some ["let mutable x = 10"; "let x = 10 mutable"; "var x = 10"; "mutable let x = 10"]; CorrectAnswer = "let mutable x = 10" }

            // 2 Written-answer questions
            { ID = 4; Text = "What is the type of the following value in F#: 'hello'?"; Options = None; CorrectAnswer = "string" }
            { ID = 5; Text = "Write the result of `let x = 3 + 7` in F#?"; Options = None; CorrectAnswer = "10" }
        ]
