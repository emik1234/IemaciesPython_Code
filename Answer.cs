using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Answer : MonoBehaviour
{
    public bool isCorrect = false;
    public Quiz quizManager;


    public void CheckAnswer()
    {
        
        if(this.GetComponent<Button>().interactable)
        {
            if (isCorrect)
            {
                StartCoroutine(quizManager.Correct());
            }
            else
            {
                StartCoroutine(quizManager.Wrong());
            }
        }


        this.GetComponent<Button>().interactable = false;
    }
        
   


}
