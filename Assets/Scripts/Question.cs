using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Quiz Question", fileName = "New Question")]

public class Question : ScriptableObject
{
    [TextArea(2, 6)]
    [SerializeField] private string questionText = "Question Text";
    [SerializeField] private string[] answers = new string[4];
    [SerializeField][Range(0, 3)] private int correctAnswerIndex = 0;

    public string QuestionText { get => questionText; }
    public string[] Answers { get => answers; }
    public string Answer(int index) { return Answers[index]; }   
    public int CorrectAnswerIndex { get => correctAnswerIndex; }
}
