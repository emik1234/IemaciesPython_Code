using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
    public Quiz quizManager;

    public Slider timerSlider;
    public TMP_Text timerText;
    public float gameTime;

    float startTime = 0f;
    bool startUpdate = false;

    public Color startColor = Color.green;
    public Color endColor = Color.red;

    int seconds;
    public int score;
    public int totalScore = 0;

    public TMP_Text totalScoreText;

    public bool startingGame = true;

    public Answer ans;

    void Update()
    {
        if(quizManager.QnA.Count > 0)
        {
            if (startUpdate)
            {
                float elapsedTime = Time.time - startTime;
                float timeLeft = Mathf.Max(0f, gameTime - elapsedTime);

                int minutes = Mathf.FloorToInt(timeLeft / 60);
                int seconds = Mathf.FloorToInt(timeLeft % 60);
                string textTime = string.Format("{0:00}:{1:00}", minutes, seconds);

                timerText.text = textTime;
                timerSlider.maxValue = gameTime;
                timerSlider.value = timeLeft;

                float normalizedTime = timeLeft / gameTime;
                timerSlider.fillRect.GetComponent<Image>().color = Color.Lerp(endColor, startColor, normalizedTime);

                if (timeLeft <= 0)
                {
                    StartCoroutine(quizManager.Wrong());
                    ResetTimer();
                }

                score = seconds;
            }
        }
    }

    public void ResetTimer()
    {
            if (!startingGame)
            {
                StartCoroutine(WaitAndResetTimer());
            }
            else
            {
                startUpdate = false;
                startingGame = false;
                startTime = Time.time;
                timerSlider.value = gameTime;
                timerSlider.fillRect.GetComponent<Image>().color = startColor;
                startUpdate = true;
            }
    }
    
    public IEnumerator WaitAndResetTimer()
    {
        startUpdate = false;
        yield return new WaitForSeconds(2);
        startTime = Time.time;
        timerSlider.value = gameTime;
        timerSlider.fillRect.GetComponent<Image>().color = startColor;
        startUpdate = true;
    }


    public void Score()
    {
        if(quizManager.QnA.Count > 0)
        {
            totalScore += score;
        }
        
        score = 0;
        totalScoreText.text = totalScore.ToString();
    }
}

