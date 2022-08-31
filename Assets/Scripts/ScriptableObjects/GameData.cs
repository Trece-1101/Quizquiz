using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "GameData", fileName = "Persistence")]
public class GameData : ScriptableObject, IPerformance
{
    [SerializeField] private bool showDebug = false;
    private Dictionary<string, int[]> globalPerformance = 
        new Dictionary<string, int[]>() {
            {"Videogames", new int[]{0, 0, 0} },
            {"Films", new int[]{0, 0, 0} },
            {"Music", new int[]{0, 0, 0} }
        };

    private string category;
    private string difficulty;

    public string Category { get => category; set => category = value; }
    public string Difficulty { get => difficulty; set => difficulty = value; }
    public Dictionary<string, int[]> GlobalPerformance { get => globalPerformance; }

    private void OnEnable()
    {
        Quiz.CorrectAnswer += CalculatePerformance;
    }

    private void OnDisable()
    {
        Quiz.CorrectAnswer += CalculatePerformance;
    }

    public void CalculatePerformance(bool isCorrect)
    {
        var correctModifier = isCorrect ? 1 : 0;

        globalPerformance[category][0] += 1;
        globalPerformance[category][1] += correctModifier;
        var performanceValue = ((float)globalPerformance[category][1] / globalPerformance[category][0]) * 100;
        globalPerformance[category][2] = (int)performanceValue;


        if (showDebug)
        {
            foreach (var cat in globalPerformance)
            {
                //var performanceValue = ((float)cat.Value[1] / cat.Value[0]) * 100;
                Debug.Log($"{cat.Key} - [{cat.Value[0]}, {cat.Value[1]}, {cat.Value[2]}]");
                Debug.Log($"{cat.Key} - performance: {performanceValue}");
            }
        }
    }
}
