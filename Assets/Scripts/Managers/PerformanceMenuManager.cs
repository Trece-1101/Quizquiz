using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PerformanceMenuManager : MonoBehaviour
{
    [SerializeField] private GameData gameData;
    [SerializeField] private GameObject performancePanel;
    private Transform template;

    private void Awake()
    {
        template = performancePanel.transform.Find("Template");
    }

    private void Start()
    {
        LoadPerformanceData();
    }

    private void LoadPerformanceData()
    {
        foreach (var category in gameData.GlobalPerformance)
        {
            var newTemplate = Instantiate(template);
            newTemplate.SetParent(performancePanel.transform);
            string categoryTittle = category.Key;
            int categoryPerformance = category.Value[2];
            newTemplate.GetComponent<TextMeshProUGUI>().text = $"{categoryTittle}\n{categoryPerformance}%";
            newTemplate.gameObject.SetActive(true);
        }
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene("ChoseCategoryScene");
    }
}
