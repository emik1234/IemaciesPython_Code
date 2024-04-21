using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class OpenFile : MonoBehaviour
{
    [SerializeField] Notepad opener;
    string t;
    string path;
    [SerializeField] GameObject deleteFileButton;

    public void Open()
    {
        t = this.GetComponentInChildren<TextMeshProUGUI>().text;
        path = Application.persistentDataPath + @"\SavedFiles\" + t;
        opener = GameObject.FindGameObjectWithTag("ButtonController").GetComponent<Notepad>();
        
        if (!File.Exists(path))
        {
            return;
        }

        string readText = File.ReadAllText(path);

        if (t.EndsWith("txt")) {
            opener.OpenNotepad(readText);
        }
        else if(t.EndsWith("py"))
        {
            opener.OpenTextEditor(readText);
        }
    }

    GameObject FindTopMostParent(GameObject obj)
    {
        Transform currentTransform = obj.transform;

        while (currentTransform.parent != null)
        {
            currentTransform = currentTransform.parent;
        }

        return currentTransform.gameObject;
    }

    public void DeleteFile()
    {
        t = transform.parent.gameObject.GetComponentInChildren<TextMeshProUGUI>().text;
        path = Application.persistentDataPath + @"\" + @"SavedFiles\" + t;

        if (!File.Exists(path))
        {
            return;
        }

        File.Delete(path);
        GameObject fileExplorer = GameObject.FindGameObjectWithTag("FileExplorer");
        Destroy(fileExplorer);

        opener = GameObject.FindGameObjectWithTag("ButtonController").GetComponent<Notepad>();
        opener.OpenFiles();
    }

    


}
