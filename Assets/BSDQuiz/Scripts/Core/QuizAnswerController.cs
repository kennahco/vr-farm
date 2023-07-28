using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class QuizAnswerController : MonoBehaviour
{
    public QuizController quizController;
    int questionIndex, answerIndex;

    internal void SetAnswer(int questionIndex, int answerIndex, QuizController controller)
    {
        quizController = controller;
        this.questionIndex = questionIndex;
        this.answerIndex = answerIndex;
    }

    public void SelectAnswer()
    {
        quizController.SelectAnswer(questionIndex, answerIndex);
    }
}
