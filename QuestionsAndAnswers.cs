using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class QuestionsAndAnswers
{
    [TextArea(3, 3)]
    public string question;
    public string[] answers;
    public int CorrectAnswer;
}
