using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using System.Security.AccessControl;
using UnityEngine.UI;


public class SaveFile : MonoBehaviour
{
    [SerializeField] GameObject data;
    
    [SerializeField] GameObject saveScreen;
    [SerializeField] GameObject fileNameText;
    [SerializeField] string extension;
    [SerializeField] CodeOverwriter regex;

    private Color red = Color.red;
    private Color green = Color.green;
    public TMP_Text confirmationText;
    public Button button;
    string inGameFilesPath;
    List<string> fileNames = new List<string>();

    public void SaveBox()
    {
        saveScreen.SetActive(true);
    }

    public void CloseSaveBox()
    {
        saveScreen.SetActive(false);
    }

    public IEnumerator Save()
    {
        button.interactable = false;
        bool notValid = false;
        string folderName = "SavedFiles";
        string folderPath = Path.Combine(Application.persistentDataPath, folderName);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        string path = folderPath; 
        
        fileNames.Clear();
        string[] fileInfo = Directory.GetFiles(path);
        foreach (string file in fileInfo)
        {
            fileNames.Add(Path.GetFileName(file));
        }

        if(fileNames.Count >= 32) {
            confirmationText.text = "Sasniegts maksimālo failu skaits!";
            confirmationText.color = red;
            yield return new WaitForSeconds(2f);
            confirmationText.text = "";
            button.interactable = true;
        } else {
            string text = regex.RemoveZeroWidthSpaces(data.GetComponent<TextMeshProUGUI>().text);
            string fileName = regex.RemoveZeroWidthSpaces(fileNameText.GetComponent<TextMeshProUGUI>().text);

            if (fileName != "" && fileName != extension)
            {
                if (!fileNames.Contains(fileName + '.' + extension))
                {
                    foreach (char letter in fileName)
                    {
                        if (!Char.IsDigit(letter) && !Char.IsLetter(letter) && letter != '_')
                        {

                            confirmationText.text = "Nederīgs nosaukums!";
                            confirmationText.color = red;
                            yield return new WaitForSeconds(2f);
                            confirmationText.text = "";
                            notValid = true;
                            break;
                        }
                    }
                    if(!notValid) {
                        fileName += "." + extension;
                        string filePath = Path.Combine(folderPath, fileName);
                        data.GetComponent<TextMeshProUGUI>().text = "";
                        
                        confirmationText.text = "Fails Saglabāts!";
                        confirmationText.color = green;
                        yield return new WaitForSeconds(2f);
                        confirmationText.text = "";
                        saveScreen.SetActive(false);
                        File.WriteAllText(filePath, text);
                    }

                    button.interactable = true;
                } else {
                    
                    confirmationText.text = "Fails ar šādu nosaukumu jau eksistē!";
                    confirmationText.color = red;
                    yield return new WaitForSeconds(2f);
                    confirmationText.text = "";
                    button.interactable = true;
                }
            }
        }
    }

    public void StartSave() {
        StartCoroutine(Save());
    }

}
