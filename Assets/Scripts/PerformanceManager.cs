using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PerformanceManager : MonoBehaviour, IPerformance
{
    private int questionsAnswered = 0;
    private int correctQuestionsAnswered = 0;
    private float performanceValue = 0f;
    private TextMeshProUGUI performanceText;

    private void OnEnable()
    {
        Quiz.CorrectAnswer += CalculatePerformance;
    }

    private void OnDisable()
    {
        Quiz.CorrectAnswer += CalculatePerformance;
    }

    private void Awake()
    {
        performanceText = transform.GetComponentInChildren<TextMeshProUGUI>();
    }

    private void Start()
    {
        UpdatePerformanceUI();
    }

    public void CalculatePerformance(bool isCorrect)
    {
        if(isCorrect) correctQuestionsAnswered++;
        questionsAnswered++;

        // 2 / 4 = 0.5 * 100 = 50
        performanceValue = ((float)correctQuestionsAnswered / questionsAnswered) * 100;
        UpdatePerformanceUI();
    }

    private void UpdatePerformanceUI()
    {
        performanceText.text = $"{(int)performanceValue}%";
    }
}
