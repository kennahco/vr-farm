using System;
using System.Collections.Generic;

public static class GradeCalcUtil
{
    public static readonly List<GradeCalcUtil.GradeLetter> DefaultGradeLetters = new List<GradeCalcUtil.GradeLetter> {
        //A's were this easy??
        //Based roughly off https://en.wikipedia.org/wiki/Academic_grading_in_Canada
        new GradeCalcUtil.GradeLetter("A",80),
        new GradeCalcUtil.GradeLetter("B",70),
        new GradeCalcUtil.GradeLetter("C",60),
        new GradeCalcUtil.GradeLetter("D",50),
        new GradeCalcUtil.GradeLetter("F")
    };

    //Figure out the highest possible letter grade based on passed score and list of letter grades
    public static string CalculateLetterGrade(int percentage, List<GradeLetter> gradeLetters)
    {
        gradeLetters.Sort();
        return gradeLetters.FindLast((letter) => letter.requiredScore <= percentage).letter;
    }

    public static string CalculateLetterGrade(int percentage)
    {
        return CalculateLetterGrade(percentage, DefaultGradeLetters);
    }

    public static string CalculateLetterGrade(float percentageAsDecimal, List<GradeLetter> gradeLetters)
    {
        return CalculateLetterGrade((int)(percentageAsDecimal * 100), gradeLetters);
    }

    public static string CalculateLetterGrade(float percentageAsDecimal)
    {
        return CalculateLetterGrade((int)(percentageAsDecimal * 100), DefaultGradeLetters);
    }

    public static string GetWorstLetter(List<GradeLetter> gradeLetters)
    {
        gradeLetters.Sort();
        return gradeLetters.Find((item) => item != null).letter;
    }

    [System.Serializable]
    public class GradeLetter : IComparable<GradeLetter>
    {
        public string letter;
        public int requiredScore;

        public GradeLetter(string letter, int requiredScore = int.MinValue)
        {
            this.letter = letter;
            this.requiredScore = requiredScore;
        }

        public int CompareTo(GradeLetter other)
        {
            return requiredScore.CompareTo(other.requiredScore);
        }
    }
}
