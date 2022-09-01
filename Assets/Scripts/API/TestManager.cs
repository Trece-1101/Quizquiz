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
        //QuizAPI quiz = ConsumeTest.GetNewQuestions();
        //textDisplayed.text = quiz.Results[0];
        var quizez = await ConsumeTest.GetNewQuestions();
        foreach (var quiz in quizez.results)
        {            
            textDisplayed.text += quiz.question.Replace("&quot;", "\"").Replace("&#039;", "\'") + "\n";
            textDisplayed.text += quiz.correct_answer + "\n";
            foreach (var answer in quiz.incorrect_answers)
            {
                textDisplayed.text += answer + "\n";
            }

            textDisplayed.text += "\n";
        }
    }
}
