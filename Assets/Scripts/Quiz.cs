using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Reflection;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;

public class Quiz : MonoBehaviour
{
    public delegate void ClickAction();
    public delegate void QuestionAction();
    public static event ClickAction AnswerClicked;
    public static event QuestionAction QuestionStarted;
    

    [Header("Question")]
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private List<Question> questionsList = new List<Question>();
    private Question currentQuestion;

    [Header("Answer")]
    [SerializeField] private GameObject[] answerButtons;
    [SerializeField] private GameObject resultImage;
    private int correcAnswerIndex;

    [Header("Buttons")]
    [SerializeField] private Button nextQuestionButton;
    [SerializeField] private Button returnButton;


    private Color correctAnswerColor = Color.green;
    private Color wrongAnswerColor = Color.red;
    private Color initialColor = Color.white;


    private void Awake()
    {
        nextQuestionButton.interactable = false;
        returnButton.interactable = false;
    }
    private void Start()
    {
        StartQuestion();
    }

    private void StartQuestion()
    {
        currentQuestion = GetRandomQuestion();
        GetQuestionData();
        SetButtonState(true, true);
        resultImage.SetActive(false);
        questionsList.Remove(currentQuestion);

        QuestionStarted();
    }

    private Question GetRandomQuestion()
    {
        int randomIndex = Random.Range(0, questionsList.Count);
        return questionsList[randomIndex];
    }

    private void GetQuestionData()
    {
        correcAnswerIndex = currentQuestion.CorrectAnswerIndex;
        questionText.text = currentQuestion.QuestionText;

        for (int i = 0; i < answerButtons.Length; i++)
        {
            TextMeshProUGUI buttonText = answerButtons[i].GetComponentInChildren<TextMeshProUGUI>();
            buttonText.text = currentQuestion.Answer(i);
        }
    }

    public void OnAnswerSelected(int index)
    {
        AnswerClicked();

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

        SetButtonState(false, false);
        if(!RemainingQuestions()) Invoke(nameof(ReturnToMenu), 3f);
    }

    private bool RemainingQuestions()
    {
        if (questionsList.Count > 0) return true;
        return false;
    }

    private void SetButtonState(bool isEnable, bool isReseting)
    {
        foreach (var button in answerButtons)
        {
            button.GetComponent<Button>().interactable = isEnable;
            if (isReseting && RemainingQuestions())
            {
                button.GetComponent<Image>().color = initialColor;
                resultImage.GetComponent<ResultFeedback>().ResetImages();
            }
        }

        if(RemainingQuestions())
        {
            nextQuestionButton.interactable = !isEnable;
            returnButton.interactable = !isEnable;
        }
    }

    public void NextQuestion()
    {
        StartQuestion();
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("ChoseCategoryScene");
    }
}
