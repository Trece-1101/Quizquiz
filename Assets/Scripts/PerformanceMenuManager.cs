using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PerformanceMenuManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("ChoseCategoryScene");
    }
}
