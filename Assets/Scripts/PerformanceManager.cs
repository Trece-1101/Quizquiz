using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PerformanceManager : MonoBehaviour
{
    private int questionsAnswered = 0;
    private int correctQuestionsAnswered = 0;
    private float performanceValue = 0f;

    private void OnEnable()
    {
        Quiz.CorrectAnswer += CalculatePerformance;
    }

    private void OnDisable()
    {
        Quiz.CorrectAnswer += CalculatePerformance;
    }

    private void Start()
    {
        UpdatePerformanceUI();
    }

    private void CalculatePerformance(bool isCorrect)
    {
        if(isCorrect) correctQuestionsAnswered++;
        questionsAnswered++;

        // 2 / 4 = 0.5 * 100 = 50
        performanceValue = ((float)correctQuestionsAnswered / questionsAnswered) * 100;
        UpdatePerformanceUI();
    }

    private void UpdatePerformanceUI()
    {
        transform.GetComponentInChildren<TextMeshProUGUI>().text = $"{(int)performanceValue}%";
    }



}
