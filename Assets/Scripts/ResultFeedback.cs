using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ResultFeedback : MonoBehaviour
{
    private void Awake()
    {
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
            var resultText = transform.Find("CorrectText");
            resultText.gameObject.SetActive(true);
        }
        else
        {
            var resultText = transform.Find("IncorrectText");
            resultText.gameObject.SetActive(true);
        }
    }

    public void TimeEnded()
    {
        var resultText = transform.Find("EndTimeText");
        resultText.gameObject.SetActive(true);
    }

}
