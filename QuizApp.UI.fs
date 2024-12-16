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
        