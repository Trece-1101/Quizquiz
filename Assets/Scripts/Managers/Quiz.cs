using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System;
using Random = UnityEngine.Random;
using System.Threading.Tasks;

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
    [SerializeField] private Timer timer;
    private int correcAnswerIndex;

    [Header("Buttons")]
    [SerializeField] private Button nextQuestionButton;
    [SerializeField] private Button returnButton;


    private Color correctAnswerColor = Color.green;
    private Color wrongAnswerColor = Color.red;
    private Color initialColor = Color.white;
    private SoundManager soundManger;


    private void Awake()
    {
        soundManger = FindObjectOfType<SoundManager>();
        nextQuestionButton.interactable = false;
        returnButton.interactable = false;        
    }
    private void Start()
    {
        if (gameData.IsConnected)
        {
            LoadQuestionsAsync();
        }
        else
        {
            LoadQuestions();
        }

    }

    private void LoadQuestions()
    {
        string path = "Questions/" + gameData.Category + "/" + gameData.Difficulty;

        Question[] questions = Resources.LoadAll<Question>(path);

        foreach (var question in questions)
        {
            questionsList.Add(question);
        }

        StartQuiz();
    }

    private async void LoadQuestionsAsync()
    {
        var quizez = await ConsumeAPI.GetNewQuestions(5, gameData.CategoryNumber, gameData.Difficulty);

        foreach (var quiz in quizez.results)
        {
            Question question = ScriptableObject.CreateInstance<Question>();
            string questionText = ReplaceUnicodeErrors(quiz.question);
            question.QuestionText = questionText;
            string[] tempAnswers = new string[4];

            int correctAnswerIndex = CreateRandomIndex();
            question.CorrectAnswerIndex = correctAnswerIndex;

            string correctAnswerText = ReplaceUnicodeErrors(quiz.correct_answer);

            tempAnswers.SetValue(correctAnswerText, correctAnswerIndex);

            foreach (var answer in quiz.incorrect_answers)
            {
                string inCorrectAnswerText = ReplaceUnicodeErrors(answer);

                int i = 0;
                while (tempAnswers[i] != null)
                {
                    i++;
                }

                tempAnswers[i] = inCorrectAnswerText;
            }

            question.Answers = tempAnswers;
            questionsList.Add(question);

#if UNITY_EDITOR
        Debug.Log($"Question: {questionText} - Correct: {correctAnswerText}");
#endif
        }

        StartQuiz();
    }

    private void StartQuiz()
    {
        StartFeedback();
        StartQuestion();
        SetProgressBar();
    }


    private int CreateRandomIndex()
    {
        return Random.Range(0, 3);
    }

    private string ReplaceUnicodeErrors(string text)
    {
        return text.Replace("&quot;", "\"").Replace("&#039;", "\'");
    }

    private void StartFeedback()
    {
        timer.SetTimeLeft(gameData.Difficulty == "easy" ? 20 : gameData.Difficulty == "medium" ? 25 : 30);
        timer.StartTimer = true;
        categoryText.text = gameData.Category + " - " + gameData.Difficulty;
        soundManger.PlayGameMusic();
    }


    private void StartQuestion()
    {
        soundManger.PlayGameMusic();
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

        soundManger.PlayAnswerSFX(isCorrect);
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
            soundManger.PlayGameMusic();
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
