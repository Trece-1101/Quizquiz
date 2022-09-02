using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject booksCategorySelector;
    [SerializeField] private GameObject televisionCategorySelector;
    [SerializeField] private TextMeshProUGUI connectionText;

    private string categorySelected = "";
    private string diffultySelected = "";
    private int categoryNumberSelected = -1;
    private Button startButton;
    private SoundManager soundManger;
    private bool isConnectedToAPI = false;

    private void Awake()
    {
        startButton = transform.Find("StartButton").GetComponent<Button>();
        soundManger = FindObjectOfType<SoundManager>();
    }
    private void Start()
    {
        startButton.interactable = false;
        soundManger.PlayMenuMusic();
        CheckConnection();
    }

    private async void CheckConnection()
    {
        var quizez = await ConsumeAPI.GetNewQuestions(1, -1);
        if(quizez.response_code == 0)
        {
            isConnectedToAPI = true;
            connectionText.text = "Connected!";
            AllowAllCategories();
        }
        else
        {
            isConnectedToAPI = false;
            connectionText.text = "Can't stablish connection";
        }

        gameData.IsConnected = isConnectedToAPI;
#if UNITY_EDITOR
        Debug.Log($"Connected to API: {isConnectedToAPI}");
#endif
    }

    private void AllowAllCategories()
    {
        connectionText.gameObject.SetActive(false);
        booksCategorySelector.SetActive(true);
        televisionCategorySelector.SetActive(true);
    }

    public void StartGame()
    {
        if (categorySelected == "" || diffultySelected == "") return;

        gameData.Category = categorySelected;
        gameData.Difficulty = diffultySelected;
        gameData.CategoryNumber = categoryNumberSelected;
        SceneManager.LoadScene("GameScene");
    }

    public void SeePerformance()
    {
        SceneManager.LoadScene("PerformanceScene");
    }
    
    public void Exit()
    {
        Application.Quit();
    }

    public void ChooseCategory(string category)
    {
        categorySelected = category;

        //TODO: Nasty Workaround -- Should go with events listeners
        // 15 = Videogames //// 11 = Films //// 12 = Music //// 14 = Television //// 10 = Books
        switch (categorySelected)
        {
            case "Videogames":
                categoryNumberSelected = 15;
                break;
            case "Films":
                categoryNumberSelected = 11;
                break;
            case "Music":
                categoryNumberSelected = 12;
                break;
            case "Books":
                categoryNumberSelected = 10;
                break;
            case "Television":
                categoryNumberSelected = 14;
                break;
            default:
                Debug.LogError("No category matched");
                break;
        }

        if(diffultySelected != "")
        {
            startButton.interactable = true;
        }
    }

    public void ChooseDifficulty(string difficulty)
    {
       diffultySelected = difficulty;
        if(categorySelected != "")
        {
            startButton.interactable = true;
        }
    }

}
