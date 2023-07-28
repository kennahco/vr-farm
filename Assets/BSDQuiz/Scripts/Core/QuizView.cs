using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public abstract class QuizView : MonoBehaviour
{
    public Button messageButton;
    public Image questionImage;
    public QuizAnswerController answerTemplate;
    public Image answerTemplateImage;

    protected QuizController quizController;
    protected List<GameObject> activeAnswers = new List<GameObject>();

    public virtual void SetupQuiz(List<Question> questions, QuizController quizController)
    {
        this.quizController = quizController;
    }

    public abstract Button DisplayMessage(string message, string buttonMessage = null);

    public abstract void DisplayQuestion(Question question, int questionIndex);

    protected virtual void ClearAnswers()
    {
        foreach (var item in activeAnswers)
        {
            Destroy(item);
        }
        activeAnswers.Clear();
    }
}
