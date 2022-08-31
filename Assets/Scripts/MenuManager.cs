using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //public enum Category { None, Videogames, Films, Music }
    //public enum Difficulty { None, Easy, Medium, Hard }
    [SerializeField] private GameData gameData;

    private string categorySelected = "";
    private string diffultySelected = "";
    private Button startButton;

    private void Awake()
    {
        startButton = transform.Find("StartButton").GetComponent<Button>();
    }
    private void Start()
    {
        startButton.interactable = false;
    }
    public void StartGame()
    {
        if (categorySelected == "" || diffultySelected == "") return;

        gameData.Category = categorySelected;
        gameData.Difficulty = diffultySelected;
        SceneManager.LoadScene("GameScene");
    }

    public void SeePerformance()
    {
        SceneManager.LoadScene("PerformanceScene");
    }

    public void ChooseCategory(string category)
    {
        categorySelected = category;
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
