using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuizViewDefault : QuizView
{
    public Text messageText;
    public Text messageButtonText;
    public Text questionText;
    public Text answerTemplateText;

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

    public override void DisplayQuestion(Question question, int questionIndex)
    {
        questionText.text = question.text;
        questionText.gameObject.SetActive(string.IsNullOrEmpty(question.text));
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
            answerTemplate.SetAnswer(questionIndex, i, quizController);
            activeAnswers.Add(newButton.gameObject);
        }

        foreach (var item in activeAnswers)
        {
            item.SetActive(true);
        }
    }
}
