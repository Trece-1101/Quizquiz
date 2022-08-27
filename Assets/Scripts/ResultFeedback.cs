using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ResultFeedback : MonoBehaviour
{
    private void Awake()
    {
        this.gameObject.SetActive(false);
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

}
