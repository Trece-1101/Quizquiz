using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeForAnswer = 20.0f;
    private bool isAnswerSelected = false;
    private bool isTimeLeft = true;
    private Quiz quiz;

    private void OnEnable()
    {
        Quiz.AnswerClicked += AnswerSelected;
    }

    private void OnDisable()
    {
        Quiz.AnswerClicked -= AnswerSelected;
    }


    private void AnswerSelected()
    {
        isAnswerSelected = true;
    }
    private void Awake()
    {
        quiz = FindObjectOfType<Quiz>();
    }
    private void Update()
    {
        if (!isTimeLeft || isAnswerSelected) return;
        UpdateTimer();
    }

    private void UpdateTimer()
    {
        timeForAnswer -= Time.deltaTime;
        // cambiar valor numero timer solo cuando es segundo exacto...

        if (timeForAnswer <= float.Epsilon)
        {
            // Siguiente pregunta
            quiz.AnswerDisplay(false, false);
            Debug.Log("perdiste");
            isTimeLeft = false;
        }

        Debug.Log(timeForAnswer);
    }
}
