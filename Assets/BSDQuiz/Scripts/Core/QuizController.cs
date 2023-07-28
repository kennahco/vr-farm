using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizController : MonoBehaviour
{
    public List<Question> questions;
    public QuizView quizView;
    public bool repeatWrongQuestions;

    private int questionIndex;

    private void Start()
    {
        questionIndex = 0;
        quizView.SetupQuiz(questions, this);
        quizView.DisplayQuestion(questions[questionIndex], questionIndex);
    }

    internal void SelectAnswer(int questionIndex, int answerIndex)
    {
        if (questions[questionIndex].answers[answerIndex].isCorrect)
        {
            if (!questions[questionIndex].wasAttempted)
            {
                questions[questionIndex].wasCorrect = true;
            }
        }
        else if (repeatWrongQuestions)
        {
            questions[questionIndex].wasAttempted = true;
            quizView.DisplayMessage("Incorrect, Try Again.", "Back");
            return;
        }
        questionIndex++;
        if (questionIndex < questions.Count)
        {
            quizView.DisplayQuestion(questions[questionIndex], questionIndex);
        }
        else
        {
            DisplayResults();
        }
    }

    private void DisplayResults()
    {
        int sumofCorrect = 0;
        for (int i = 0; i < questions.Count; i++)
        {
            if (questions[i].wasCorrect)
            {
                sumofCorrect++;
            }
        }
        string LetterGrade = GradeCalcUtil.CalculateLetterGrade((float)sumofCorrect / questions.Count);
        if (LetterGrade == GradeCalcUtil.GetWorstLetter(GradeCalcUtil.DefaultGradeLetters))
        {
            quizView.DisplayMessage("You Failed!");
        }
        else
        {
            quizView.DisplayMessage("Grade: " + LetterGrade + "\n Good Job!");
        }
    }

    public override string ToString()
    {
        string output = "";
        int number = 1;
        foreach (var question in questions)
        {
            output += "\n" + number + ")" + question.ToString() +"\n";
            number++;
        }
        return output;
    }
}
