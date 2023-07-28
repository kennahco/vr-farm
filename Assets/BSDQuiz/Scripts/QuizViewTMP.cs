using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class QuizViewTMP : QuizView
{
    public TextMeshProUGUI messageText;
    public TextMeshProUGUI messageButtonText;
    public TextMeshProUGUI questionText;
    public TextMeshProUGUI answerTemplateText;

    public override Button DisplayMessage(string message, string buttonMessage = null)
    {
        ClearAnswers();
        questionText.gameObject.SetActive(false);
        questionImage.gameObject.SetActive(false);
        messageText.gameObject.SetActive(true);
        messageText.text = message;

        messageButton.gameObject.SetActive(buttonMessage != null);
        if (buttonMessage != null)
        {
            messageButtonText.text = buttonMessage;
        }
        return messageButton;
    }

    public override void SetupQuiz(List<Question> questions, QuizController quizController)
    {
        base.SetupQuiz(questions, quizController);
    }

    public override void DisplayQuestion(Question question, int questionIndex)
    {
        questionText.text = question.text;
        questionText.gameObject.SetActive(!string.IsNullOrEmpty(question.text));
        questionImage.sprite = question.image;
        questionImage.gameObject.SetActive(question.image != null);

        ClearAnswers();

        for (int i = 0; i < question.answers.Count; i++)
        {
            var answer = question.answers[i];
            answerTemplateText.text = answer.text;
            answerTemplateText.gameObject.SetActive(!string.IsNullOrEmpty(answer.text));
            answerTemplateImage.sprite = answer.image;
            answerTemplateImage.gameObject.SetActive(answer.image != null);
            var newButton = Instantiate(answerTemplate, answerTemplate.transform.parent);
            newButton.SetAnswer(questionIndex, i, quizController);
            newButton.gameObject.SetActive(true);
            activeAnswers.Add(newButton.gameObject);
        }
    }
}
