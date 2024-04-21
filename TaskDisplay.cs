using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Runtime.InteropServices;
using System.Linq;

public class TaskDisplay : MonoBehaviour
{  
    SQLiteDB db;
    public GameObject prefabTask;
    public List<Task> tasks = new List<Task>();
    [SerializeField] GameObject parent;
    TaskAnswer answerScript;
    // Start is called before the first frame update
    void Start()
    {   
        db = GameObject.Find("DBController").GetComponent<SQLiteDB>();
        tasks = GameObject.Find("TaskList").GetComponent<TaskDisplay>().tasks;

        string t = db.ReturnDoneTasks();

        string[] doneTasks = t.Split(';');
        tasks.RemoveAll(task => doneTasks.Contains(task.name));

        if (parent != null)
        {
            foreach (var task in tasks)
            {
                GameObject currentPrefab = Instantiate(prefabTask, Vector2.zero, Quaternion.identity);
                currentPrefab.transform.SetParent(parent.transform, false);

                GameObject title1 = currentPrefab.transform.Find("title").gameObject;
                GameObject content1 = currentPrefab.transform.Find("content").gameObject;
                GameObject difficulty1 = currentPrefab.transform.Find("difficultyLevel").gameObject;
                answerScript = currentPrefab.transform.Find("Submit").gameObject.GetComponent<TaskAnswer>();

                answerScript.answer = task.answer;
                title1.GetComponent<TMP_Text>().text = task.title;
                content1.GetComponent<TMP_Text>().text = task.content;
                difficulty1.GetComponent<TMP_Text>().text = task.difficultyLevel;
            }
        }
        
    }
}
