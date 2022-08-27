using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Quiz : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private Question question;
    [SerializeField] private GameObject[] answerButtons;
    [SerializeField] private GameObject resultImage;
    private int correcAnswerIndex;
    private Color correctAnswerColor = Color.green;
    private Color wrongAnswerColor = Color.red;

    private void Start()
    {
        if (!question) return;
        GetQuestionData();
    }

    private void GetQuestionData()
    {
        correcAnswerIndex = question.CorrectAnswerIndex;
        questionText.text = question.QuestionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = question.Answer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        if (!resultImage) return;

        resultImage.SetActive(true);

        if(index == correcAnswerIndex)
        {
            resultImage.GetComponent<ResultFeedback>().SetResult(true);
        }
        else
        {
            answerButtons[index].GetComponent<Image>().color = wrongAnswerColor;
            resultImage.GetComponent<ResultFeedback>().SetResult(false);
        }

        // No matter right or wrong answer, the right one should always be shown to player
        answerButtons[correcAnswerIndex].GetComponent<Image>().color = correctAnswerColor;
    }

}
