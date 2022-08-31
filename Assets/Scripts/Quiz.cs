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
    
    public static event Action<bool> CorrectAnswer;

    [Header("Question")]
    [SerializeField] private Slider questionProgressSlider;
    [SerializeField] private GameData gameData;
    [SerializeField] private TextMeshProUGUI questionText;
    [SerializeField] private TextMeshProUGUI categoryText;
    private List<Question> questionsList = new List<Question>();
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
        categoryText.text = gameData.Category + " - " + gameData.Difficulty;

        string path = "Questions/" + gameData.Category + "/" + gameData.Difficulty;

        Question[] questions = Resources.LoadAll<Question>(path);

        foreach (var question in questions)
        {
            questionsList.Add(question);
        }
    }
    private void Start()
    {
        StartQuestion();
        SetProgressBar();
    }


    private void StartQuestion()
    {
        currentQuestion = GetRandomQuestion();
        GetQuestionData();
        SetButtonState(true, true);
        resultImage.SetActive(false);
        questionsList.Remove(currentQuestion);

        QuestionStarted?.Invoke();
    }
    private void SetProgressBar()
    {
        questionProgressSlider.value = 0;
        questionProgressSlider.maxValue = questionsList.Count;
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
        AnswerClicked?.Invoke();

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
            CorrectAnswer?.Invoke(true);
            resultImage.GetComponent<ResultFeedback>().SetResult(true);
        }
        else if (!isCorrect && timeLeft)
        {
            CorrectAnswer?.Invoke(false);
            resultImage.GetComponent<ResultFeedback>().SetResult(false);
        }
        else
        {
            CorrectAnswer?.Invoke(false);
            resultImage.GetComponent<ResultFeedback>().TimeEnded();
        }

        // No matter right or wrong answer, the right one should always be shown to player
        answerButtons[correcAnswerIndex].GetComponent<Image>().color = correctAnswerColor;

        SetButtonState(false, false);
        if(!RemainingQuestions()) Invoke(nameof(ReturnToMenu), 2f);
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
        questionProgressSlider.value++;
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("ChoseCategoryScene");
    }
}
