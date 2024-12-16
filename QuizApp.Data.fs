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
            
            // 2 Written-answer questions
            { ID = 4; Text = "What is the type of the following value in F#: 'hello'?"; Options = None; CorrectAnswer = "string" }
            { ID = 5; Text = "Write the result of `let x = 3 + 7` in F#?"; Options = None; CorrectAnswer = "10" }
        ]
