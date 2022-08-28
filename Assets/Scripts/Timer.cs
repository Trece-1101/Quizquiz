using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeLeftForAnswer = 20.0f;
    private float originalTimeForAnswer;
    private bool isAnswerSelected = false;
    private bool isTimeLeft = true;
    private float timerFraction;
    private TextMeshProUGUI timeLeftText;
    private Image quizTimer;
    private Quiz quiz;

    private void OnEnable()
    {
        Quiz.AnswerClicked += AnswerSelected;
    }

    private void OnDisable()
    {
        Quiz.AnswerClicked -= AnswerSelected;
    }
    private void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
        quizTimer = GetComponent<Image>();
        timeLeftText = transform.Find("TimeLeft").GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        originalTimeForAnswer = timeLeftForAnswer;
    }
    private void Update()
    {
        if (!isTimeLeft || isAnswerSelected) return;
        UpdateTimer();
    }

    private void AnswerSelected()
    {
        isAnswerSelected = true;
        ResetTimer();
    }

    private void UpdateTimer()
    {
        timeLeftForAnswer -= Time.deltaTime;
        UpdateVisual();
        CheckTimeLeft();
    }

    private void UpdateVisual()
    {
        // originalTimeForAnswer -- 100% (1.0) --> timeLeftForAnswer -- X% (x)
        // X = timeLeftForAnswer * 1.0 / originalTimeForAnswer --> timeLeftForAnswer / originalTimeForAnswer
        timerFraction = timeLeftForAnswer / originalTimeForAnswer;
        quizTimer.fillAmount = 1f - timerFraction;

        var roundTime = Mathf.Ceil(timeLeftForAnswer);
        timeLeftText.text = $"{roundTime}";
    }

    private void CheckTimeLeft()
    {
        if (timeLeftForAnswer <= float.Epsilon)
        {
            // Siguiente pregunta
            quiz.AnswerDisplay(false, false);
            Debug.Log("perdiste");
            isTimeLeft = false;
        }
    }

    private void ResetTimer()
    {
        timeLeftForAnswer = originalTimeForAnswer;
    }
}
