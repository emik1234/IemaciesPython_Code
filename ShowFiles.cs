using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using TMPro;

public class ShowFiles : MonoBehaviour
{
    [SerializeField] Button pyLogo;
    [SerializeField] Button txtLogo;
    Button file;

    [SerializeField] List<string> fileNames;
    [SerializeField] List<GameObject> grid;
   
    // Start is called before the first frame update
    void Start()
    {
        ReadFiles();
    }

    public void ReadFiles()
    {   
        string path = Path.Combine(Application.persistentDataPath, "SavedFiles");
        string[] fileInfo = Directory.GetFiles(path);
        
        foreach(string file in fileInfo)
        {
            fileNames.Add(Path.GetFileName(file));
        }

        if(gameObject.name == "PythonGrid") {
            LoadPythonFiles();
        } else {
            LoadFiles();
        }

    }

    void LoadFiles()
    {

        for (int i = 0; i < fileNames.Count; i++)
        {
            GameObject box = grid[i];

            if(txtLogo != null)
            {
                if (fileNames[i].EndsWith("txt"))
                {
                    file = Instantiate(txtLogo, Vector2.zero, Quaternion.identity);
                }
            }

            if (fileNames[i].EndsWith("py"))
            {
                file = Instantiate(pyLogo, Vector2.zero, Quaternion.identity);
            }

            if(file != null)
            {
                TextMeshProUGUI fileTextBox = file.GetComponentInChildren<TextMeshProUGUI>();

                if(txtLogo == null)
                {
                    box = grid[i];
                    file.transform.SetParent(box.transform, false);
                }
                else
                {
                    file.transform.SetParent(box.transform, false);
                }
                

                if (fileTextBox != null)
                {
                    fileTextBox.text = fileNames[i];
                }
            }
            
        }
    }

    void LoadPythonFiles()
    {
        int index = 0;

        for (int i = 0; i < fileNames.Count; i++)
        {
            GameObject box = grid[index];

            if (fileNames[i].EndsWith("py"))
            {
                file = Instantiate(pyLogo, Vector2.zero, Quaternion.identity);
            }

            if(file != null)
            {
                TextMeshProUGUI fileTextBox = file.GetComponentInChildren<TextMeshProUGUI>();

                if(txtLogo == null)
                {
                    box = grid[index];
                    file.transform.SetParent(box.transform, false);
                }
                else
                {
                    file.transform.SetParent(box.transform, false);
                }
                

                if (fileTextBox != null)
                {
                    fileTextBox.text = fileNames[i];
                }
                index++;
            }

            file = null;
            
        }
    }


}
