using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizViewMyera : QuizViewTMP
{
    public GameObject ProgressBarContainer;
    public Image ProgressBar;

    int questionCount = 1;

    public override void SetupQuiz(List<Question> questions, QuizController quizController)
    {
        base.SetupQuiz(questions, quizController);
        questionCount = questions.Count;
    }

    public override Button DisplayMessage(string message, string buttonMessage = null)
    {
        ProgressBarContainer.SetActive(false);
        return base.DisplayMessage(message, buttonMessage);
    }

    public override void DisplayQuestion(Question question, int questionIndex)
    {
        ProgressBarContainer.SetActive(true);
        ProgressBar.fillAmount = (float)questionIndex / questionCount;
        base.DisplayQuestion(question, questionIndex);
    }
}