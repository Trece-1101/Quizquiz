using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultFeedback : MonoBehaviour
{

    private Transform correctText;
    private Transform incorrectText;
    private Transform endTimeText;
    private void Awake()
    {
        correctText = transform.Find("CorrectText");
        incorrectText = transform.Find("IncorrectText");
        endTimeText = transform.Find("EndTimeText");
        this.gameObject.SetActive(false);
    }

    private void Start()
    {
        var background = this.gameObject.GetComponent<Image>();
        var tempColor = background.color;
        tempColor.a = 1f;
        background.color = tempColor;
    }

    public void SetResult(bool isCorrect)
    {
        if (isCorrect)
        {
            //var resultText = transform.Find("CorrectText");
            correctText.gameObject.SetActive(true);
        }
        else
        {
            //var resultText = transform.Find("IncorrectText");
            incorrectText.gameObject.SetActive(true);
        }
    }

    public void TimeEnded()
    {
        //var resultText = transform.Find("EndTimeText");
        endTimeText.gameObject.SetActive(true);
    }

    public void ResetImages()
    {
        correctText.gameObject.SetActive(false);
        incorrectText.gameObject.SetActive(false);
        endTimeText.gameObject.SetActive(false);
    }

}
