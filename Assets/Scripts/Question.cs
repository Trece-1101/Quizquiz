using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]
public class Question : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] private string questionText = "Question Text";

    public string QuestionText { get => questionText; set => questionText = value; }
}
