using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Task : ScriptableObject
{
    public string title;
    [TextArea(3, 3)]
    public string content;
    public int coins;
    public string difficultyLevel;
    [TextArea(3, 3)]
    public string answer;
    public bool completed = false;
}

