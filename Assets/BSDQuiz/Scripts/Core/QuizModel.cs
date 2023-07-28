using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question : TextAndImage
{
    public List<Answer> answers;
    public bool wasCorrect;
    public bool wasAttempted;

    public override string ToString()
    {
        string output = base.ToString();
        char letter = 'a';
        foreach (var answer in answers)
        {
            output += "\n" + letter + ")" + answer.ToString();
            letter++;
        }
        return output;
    }
}

[System.Serializable]
public class Answer : TextAndImage
{
    public bool isCorrect;

    public override string ToString()
    {
        return base.ToString() + "\nCorrect:" + isCorrect;
    }
}

[System.Serializable]
public class TextAndImage
{
    public string text;
    public Sprite image;

    public override string ToString() 
    {
        string output = "";
        if (image != null)
        {
            output += image.name + "\n";
        }
        return output + text;
    }
}
