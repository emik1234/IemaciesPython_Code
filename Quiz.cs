using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Quiz : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion;
    public TMP_Text QuestionTxt;

    public GameObject startScreen;
    public GameObject gameScreen;
    public GameObject howToPlayScreen;
    public GameObject gameOverScreen;
    public Timer timer;
    public GameObject parent;

    public TMP_Text gameOverScore;
    public TMP_Text correctCountText;
    public int correctCount = 0;

    public RawImage[] answerBoxes = new RawImage[3];

    public Color startColor;
    Color green = Color.green;
    Color red = Color.red;
    Color redLerped;
    Color greenLerped;
    float duration = 2f;
    public List<QuestionsAndAnswers> originalQnA = new List<QuestionsAndAnswers>();

    int correctAnsIndex;

    public Button[] buttons = new Button[3];

    void Start()
    {
        originalQnA = new List<QuestionsAndAnswers>(QnA);
    }

    public void GameOver()
    {
        gameScreen.SetActive(false);
        gameOverScreen.SetActive(true);

        if(correctCount > 20) {
            correctCount = 20;
        }

        correctCountText.text = correctCount.ToString() + "/" + originalQnA.Count.ToString();
    }

    public IEnumerator Correct()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        correctCount++;
        timer.Score();
        Debug.Log("Correct: "+ correctCount);
        QnA.RemoveAt(currentQuestion);

        StartCoroutine(ChangeBoxColors());
        yield return new WaitForSeconds(2);
        GenerateQuestion();
    }

    public IEnumerator Wrong()
    {
        foreach (Button button in buttons)
        {
            button.interactable = false;
        }

        QnA.RemoveAt(currentQuestion);

        StartCoroutine(ChangeBoxColors());
        yield return new WaitForSeconds(2);
        GenerateQuestion();
    }


    void DisplayAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<Answer>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TMP_Text>().text = QnA[currentQuestion].answers[i];

            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                correctAnsIndex = QnA[currentQuestion].CorrectAnswer;
                options[i].GetComponent<Answer>().isCorrect = true;
            }

        }
    }

    void GenerateQuestion()
    {
        if(QnA.Count > 0)
        {
            foreach(Button button in buttons)
            {
                button.interactable = true;
            }

            foreach (RawImage rawImage in answerBoxes)
            {
                rawImage.color = startColor;
            }

            currentQuestion = Random.Range(0, QnA.Count);
            QuestionTxt.text = QnA[currentQuestion].question;
            DisplayAnswers();
        }
        else
        {
            gameOverScore.text = "Punktu skaits: " + timer.totalScore.ToString();
            GameOver();
        }
        
    }

    public void StartGame()
    {
        startScreen.SetActive(false);
        gameScreen.SetActive(true);
        timer.startingGame = true;
        GenerateQuestion();
    }

    public void HowToPlay()
    {
        startScreen.SetActive(false);
        howToPlayScreen.SetActive(true);
    }

    public void BackToStart()
    {   
        timer.totalScore = 0;
        timer.Score();
        correctCount = 0;
        QnA.Clear();
        QnA.AddRange(originalQnA);
        gameOverScreen.SetActive(false);
        howToPlayScreen.SetActive(false);
        startScreen.SetActive(true);
    }

    public void Quit()
    {
        Destroy(parent);
    }


    IEnumerator ChangeBoxColors()
    {
        
        float elapsedTime = 0f;
        int i = 0;
        while (elapsedTime < duration)
        {
            i = 0;
            float t = elapsedTime / duration;
            Color redLerped = Color.Lerp(startColor, red, t);
            Color greenLerped = Color.Lerp(startColor, green, t);

            foreach (RawImage rawImage in answerBoxes)
            {
                if(i == correctAnsIndex-1)
                {
                    rawImage.color = greenLerped;
                }
                else
                {
                    rawImage.color = redLerped;
                }

                i++;
            }

            elapsedTime += Time.deltaTime;
            yield return null;
        }

        i = 0;
        foreach (RawImage rawImage in answerBoxes)
        {
            if (i == correctAnsIndex)
            {
                rawImage.color = greenLerped;
            }
            else
            {
                rawImage.color = redLerped;
            }
            i++;
        }
    }
}
