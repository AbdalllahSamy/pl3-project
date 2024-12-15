namespace QuizApp.UI

open System
open System.Windows.Forms
open System.Drawing
open QuizApp.Data
open QuizApp.Logic

module QuizForm =
    let createForm () =
        let form = new Form(Text = "Quiz Application", Width = 800, Height = 500, BackColor = Color.Black)

        // Label for the question number and qustion text
        let lblQuestionNumber = new Label(Left = 20, Top = 20, Width = 50, Height = 50, Font = new Font("Arial", 16.0f, FontStyle.Bold), ForeColor = Color.White)
        let lblQuestion = new Label(Left = 80, Top = 20, Width = 700, Height = 50, Font = new Font("Arial", 16.0f, FontStyle.Bold), ForeColor = Color.White)
        
        // Panel for the question to create a border around it
        let questionPanel = new Panel(Left = 20, Top = 20, Width = 750, Height = 80, BorderStyle = BorderStyle.FixedSingle)
        questionPanel.Controls.Add(lblQuestion)

        // GroupBox for options (Initially hidden)
        let groupOptions = new GroupBox(Left = 20, Top = 130, Width = 750, Height = 250, BackColor = Color.Black)
        let txtAnswer = new TextBox(Left = 20, Top = 150, Width = 500, Height = 70, BackColor = Color.White, ForeColor = Color.Blue, Font = new Font("Arial", 14.0f))
        let lblFeedback = new Label(Left = 20, Top = 390, Width = 500, Height = 50, Font = new Font("Arial", 12.0f))

        // Buttons for checking the answer and moving to the next question
        let btnCheck = new Button(Text = "Check Answer", Left = 500, Top = 400, Width = 150, Height = 40, BackColor = Color.White, ForeColor = Color.DarkGoldenrod, Font = new Font("Arial", 12.0f, FontStyle.Bold))
        let btnNext = new Button(Text = "Next", Left = 670, Top = 400, Width = 100, Height = 40, BackColor = Color.White, ForeColor = Color.DarkGoldenrod, Font = new Font("Arial", 12.0f, FontStyle.Bold))

        // To track whether the user has answered the current question
        let mutable lastAnswerProvided = false

        // Function to display the question and options
        let displayQuestion () =
            lblFeedback.Text <- ""
            lblFeedback.ForeColor <- Color.Black
            lastAnswerProvided <- false

            match QuizLogic.getCurrentQuestion () with
            | Some question -> 
                lblQuestionNumber.Text <- sprintf "Q%d" question.ID
                lblQuestion.Text <- question.Text
                groupOptions.Controls.Clear()

                match question.Options with
                | Some options -> 
                    groupOptions.Visible <- true
                    txtAnswer.Visible <- false
                    options |> List.iteri (fun i option -> 
                        let rb = new RadioButton(Text = option, Left = 10, Top = i * 45, Width = 700, Font = new Font("Arial", 14.0f), ForeColor = Color.White, BackColor = Color.Black)
                        rb.FlatStyle <- FlatStyle.Flat
                        rb.Appearance <- Appearance.Button  // Customizing the button appearance
                        groupOptions.Controls.Add(rb))
                | None -> 
                    groupOptions.Visible <- false
                    txtAnswer.Visible <- true
                    txtAnswer.Clear()
            | None -> 
                // Display the score and feedback in the "X out of Y" format
                let score = QuizLogic.getScore ()
                let totalQuestions = 5  // Total number of questions in the quiz
                lblQuestion.Text <- "Quiz Finished!"
                let feedbackMessage = 
                    match score with
                    | s when s = totalQuestions -> sprintf "Excellent! Score: %d out of %d" score totalQuestions
                    | s when s >= totalQuestions - 1 -> sprintf "Good! Score: %d out of %d" score totalQuestions
                    | _ -> sprintf "Retake the test. Score: %d out of %d" score totalQuestions

                lblFeedback.Text <- feedbackMessage
                lblFeedback.Font <- new Font("Arial", 18.0f, FontStyle.Bold)
                lblFeedback.ForeColor <- 
                    match score with
                    | s when s = totalQuestions -> Color.Green
                    | s when s >= totalQuestions - 1 -> Color.Gold
                    | _ -> Color.Red
                lblFeedback.TextAlign <- ContentAlignment.MiddleCenter
                lblFeedback.BackColor <- Color.Black

                // Disable options and answer fields when quiz is finished
                groupOptions.Visible <- false
                txtAnswer.Visible <- false

        // Function to check the answer
        btnCheck.Click.Add(fun _ -> 
            match QuizLogic.getCurrentQuestion () with
            | Some question -> 
                let userAnswer = 
                    match question.Options with
                    | Some _ -> 
                        groupOptions.Controls
                        |> Seq.cast<RadioButton>
                        |> Seq.tryFind (fun rb -> rb.Checked)
                        |> Option.map (fun rb -> rb.Text)
                    | None -> Some txtAnswer.Text
                match userAnswer with
                | Some answer -> 
                    lastAnswerProvided <- true
                    QuizLogic.submitAnswer answer question
                    if answer = question.CorrectAnswer then
                        lblFeedback.Text <- "Correct!"
                        lblFeedback.ForeColor <- Color.Green
                    else
                        lblFeedback.Text <- sprintf "Wrong! Correct: %s" question.CorrectAnswer
                        lblFeedback.ForeColor <- Color.Red
                | None -> 
                    lblFeedback.Text <- "Please select an answer!"
                    lblFeedback.ForeColor <- Color.Red
            | None -> ())

        // Function for the "Next" button to validate if an answer is provided
        btnNext.Click.Add(fun _ -> 
            if not lastAnswerProvided then
                MessageBox.Show("This question is required!", "Answer Required", MessageBoxButtons.OK, MessageBoxIcon.Warning) |> ignore
            else
                QuizLogic.nextQuestion ()
                displayQuestion ())

        // Add all controls to the form
        form.Controls.Add(lblQuestionNumber)
        form.Controls.Add(questionPanel)
        form.Controls.Add(groupOptions)
        form.Controls.Add(txtAnswer)
        form.Controls.Add(lblFeedback)
        form.Controls.Add(btnCheck)
        form.Controls.Add(btnNext)

        // Display the first question
        displayQuestion ()
        form
