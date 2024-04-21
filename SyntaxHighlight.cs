using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

public class SyntaxHighlightOnChange : MonoBehaviour
{
    [SerializeField] private TMP_InputField inputField;

    private string[] keywords = {
        "False", "None", "True", "and", "as", "assert", "async", "await", "break",
        "class", "continue", "def", "del", "elif", "else", "except", "finally", "for",
        "from", "global", "if", "import", "in", "is", "lambda", "nonlocal", "not",
        "or", "pass", "raise", "return", "try", "while", "with", "yield"
    };

    public string keywordColor = "#FF0000";

    private void Start()
    {
        inputField.onValueChanged.AddListener(delegate { HighlightSyntax(); });
    }

    private void HighlightSyntax()
    {
        string processedText = inputField.text;
        foreach (string keyword in keywords)
        {
            string pattern = $@"\b{keyword}\b";
            string replacement = $"<color={keywordColor}>{keyword}</color>";
            processedText = Regex.Replace(processedText, pattern, replacement);
        }

        inputField.onValueChanged.RemoveListener(delegate { HighlightSyntax(); });
        inputField.text = processedText;

        inputField.onValueChanged.AddListener(delegate { HighlightSyntax(); });

        inputField.caretPosition = processedText.Length;
    }
}
