using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(QuizController))]
public class QuizControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        QuizController quizController = (QuizController)target;
        if (GUILayout.Button("Display Quiz"))
        {
            if(EditorUtility.DisplayDialog("Quiz Display", quizController.ToString(), "CopyToClipboard", "Close"))
            {
                GUIUtility.systemCopyBuffer = quizController.ToString();
            }
        }
    }
}
