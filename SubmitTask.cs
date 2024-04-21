using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;


public class SubmitTask : MonoBehaviour
{
    [SerializeField] GameObject chooseFile;
    Canvas canvas;
    [SerializeField] PythonRunner pythonRunner;
    public string ans;

    [SerializeField] SQLiteDB db;
    public ParticleSystem correctParticles;

    Color green = Color.green;
    Color red = Color.red;
    Color startColor = new Color(75f / 255f, 75f / 255f, 75f / 255f);

    public TaskDisplay taskDisplay;


    // Start is called before the first frame update
    void Start()
    {
        db = GameObject.Find("DBController").GetComponent<SQLiteDB>();
        taskDisplay = GameObject.Find("TaskList").GetComponent<TaskDisplay>();
        canvas = GameObject.FindWithTag("Canvas").GetComponent<Canvas>();
    }

    void Correct(GameObject panel)
    {
        Instantiate(correctParticles, new Vector3(0, -6.84f, 1), Quaternion.identity);

        for (int i = taskDisplay.tasks.Count - 1; i >= 0; i--)
        {
            if (ans == taskDisplay.tasks[i].answer)
            {
                db.AddDoneTask(taskDisplay.tasks[i].name + ";");
                taskDisplay.tasks.RemoveAt(i);
            }
        }

        Destroy(panel);
    }

    void Wrong()
    {
        GameObject.FindWithTag("Fade").GetComponent<WrongAnswer>().Fade();
    }

    public void OpenChooseFiles()
    {
        ans = this.GetComponentInChildren<TaskAnswer>().answer;
        GameObject panel = this.transform.parent.gameObject; 
        GameObject choose = Instantiate(chooseFile, Vector2.zero, Quaternion.identity);
        choose.transform.SetParent(canvas.transform, false);
        choose.GetComponent<TaskAnswer>().answer = ans;
        choose.GetComponent<TaskAnswer>().panel = panel;
    }
    

    public void SelectFile()
    {
        string t = this.GetComponentInChildren<TextMeshProUGUI>().text;
        Transform parent = transform.parent.parent.parent;
        ans = parent.GetComponent<TaskAnswer>().answer;
        GameObject panel = parent.GetComponent<TaskAnswer>().panel;
        Destroy(parent.gameObject);
        string path = Application.persistentDataPath.Replace("/", "\\") + "\\" + "SavedFiles\\" + t;

        if (!File.Exists(path))
        {
            return;
        }

        string output = pythonRunner.ExecuteScript(path);

        ans = RemoveZeroWidthSpace(ans);
        output = RemoveZeroWidthSpace(output);

        if (output == ans)
        {
            Correct(panel);
        } else
        {
            Wrong();
        }

    }

    string RemoveZeroWidthSpace(string input)
    {
        input = input.Trim();

        if (input.Length > 0 && input[0] == '\uFEFF')
        {
            return input.Substring(1);
        }
        return input;
    }


}
