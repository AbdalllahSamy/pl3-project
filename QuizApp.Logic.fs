namespace QuizApp.Logic

open QuizApp.Data

module QuizLogic =
    let mutable currentQuestionIndex = 0
    let mutable userAnswers: Map<int, string> = Map.empty

    let getQuestions () = QuizData.getQuestions ()

    

    let submitAnswer (answer: string) (question: Question) =
        userAnswers <- userAnswers.Add(question.ID, answer)

    let nextQuestion () =
        currentQuestionIndex <- currentQuestionIndex + 1

    let getScore () =
        let questions = getQuestions ()
        questions
        |> List.sumBy (fun q ->
            match userAnswers.TryFind q.ID with
            | Some answer when answer = q.CorrectAnswer -> 1
            | _ -> 0)
