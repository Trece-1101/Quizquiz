using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Reflection;

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
        if(index == correcAnswerIndex)
        {
            AnswerDisplay(true);
        }
        else
        {
            answerButtons[index].GetComponent<Image>().color = wrongAnswerColor;
            AnswerDisplay(false);
        }
    }

    public void AnswerDisplay(bool isCorrect, bool timeLeft = true)
    {
        if (!resultImage) return;

        resultImage.SetActive(true);

        if (isCorrect && timeLeft)
        {
            resultImage.GetComponent<ResultFeedback>().SetResult(true);
        }
        else if (!isCorrect && timeLeft)
        {
            resultImage.GetComponent<ResultFeedback>().SetResult(false);
        }
        else
        {
            resultImage.GetComponent<ResultFeedback>().TimeEnded();
        }

        // No matter right or wrong answer, the right one should always be shown to player
        answerButtons[correcAnswerIndex].GetComponent<Image>().color = correctAnswerColor;

        SetButtonState(false);
    }

    private void SetButtonState(bool isEnable)
    {
        foreach (var button in answerButtons)
        {
            button.GetComponent<Button>().interactable = isEnable;
        }
    }

    private void NextQuestion()
    {

    }

}
