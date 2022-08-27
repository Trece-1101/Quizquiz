using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] private float timeForAnswer = 20.0f;
    private bool isAnswerSelected = false;
    private bool isTimeLeft = true;
    private Quiz quiz;

    public bool IsAnswerSelected { get => isAnswerSelected; set => isAnswerSelected = value; }

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
