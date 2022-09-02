using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using static QuizAPI;

public class TestManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textDisplayed;

    public async void NewText()
    {
        var quizez = await ConsumeTest.GetNewQuestions();

        foreach (var quiz in quizez.results)
        {
            string questionText = ReplaceUnicodeErrors(quiz.question) + "\n";
            string correctAnswerText = ReplaceUnicodeErrors(quiz.correct_answer) + "\n";

            textDisplayed.text += questionText;
            textDisplayed.text += correctAnswerText;
#if UNITY_EDITOR
            Debug.Log($"Question: {questionText} - Correct: {correctAnswerText}");
#endif

            foreach (var answer in quiz.incorrect_answers)
            {
                string inCorrectAnswerText = ReplaceUnicodeErrors(answer) + "\n";
                textDisplayed.text += inCorrectAnswerText;
            }

            textDisplayed.text += "\n";
        }
    }

    private string ReplaceUnicodeErrors(string text)
    {
        return text.Replace("&quot;", "\"").Replace("&#039;", "\'");
    }
}
