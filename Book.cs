using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Book : ScriptableObject {
    public string title;
    [TextArea(15, 20)]
    public string content;
    public Sprite example;
    public float fontSize = 18f;
}
