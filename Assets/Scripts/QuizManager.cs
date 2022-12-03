using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class QuizManager : MonoBehaviour
{
    public List<QuestionsAndAnswers> QnA;
    public GameObject[] options;
    public int currentQuestion = 0;

    public Text QuestionTxt;
    public Text scoreTxt;

    public TMP_Text title;
    public TMP_Text[] lines;

    public GameObject Quizpanel;
    public GameObject Gopanel;

    public GameObject homePanel;
    public GameObject InfoPanel;

    public GameObject AllPrefabs;

    GameObject shape1;
    int totalQuestions = 0;
    public int score = 0;
    private void Start()
    {
        homePanel.SetActive(true);
        totalQuestions = QnA.Count;
        InfoPanel.SetActive(false);
        Gopanel.SetActive(false);
        Quizpanel.SetActive(false);

    }

    public void startGame()
    {
        homePanel.SetActive(false);
        Quizpanel.SetActive(true);
        generateQuestion();
    }
    public void retry()
    {
        // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        currentQuestion = 0;
        score = 0;
        Gopanel.SetActive(false);
        startGame();
    }
    public void gameOver()
    {
        Quizpanel.SetActive(false);
        Gopanel.SetActive(true);
        scoreTxt.text = score + " / " + totalQuestions;
    }
    public void correct()
    {
        score++;
        StartCoroutine(WaitForNext());
    }

    public void wrong()
    {
        StartCoroutine(WaitForNext());
    }
    IEnumerator WaitForNext()
    {
        yield return new WaitForSeconds(0.2f);
        title.text = QnA[currentQuestion].title;
        for (int i = 0; i < QnA[currentQuestion].lines.Length; i++)
        {
            lines[i].text = QnA[currentQuestion].lines[i];
        }
        InfoPanel.SetActive(true);
    }
    public void next()
    {
        QnA[currentQuestion].firstPrefab.SetActive(false);
        currentQuestion++;
        if (currentQuestion < QnA.Count)
        {
            Debug.Log(currentQuestion);
            InfoPanel.SetActive(false);
            generateQuestion();
        }
        else
        {
            InfoPanel.SetActive(false);
            gameOver();
        }

    }
    void SetAnswers()
    {
        for (int i = 0; i < options.Length; i++)
        {
            options[i].GetComponent<AnswerScript>().isCorrect = false;
            options[i].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = QnA[currentQuestion].Answers[i];
            // options[i].GetComponent<Image>().color = options[i].GetComponent<AnswerScript>().startColor;
            options[i].GetComponent<Image>().color = Color.white;
            if (QnA[currentQuestion].CorrectAnswer == i + 1)
            {
                options[i].GetComponent<AnswerScript>().isCorrect = true;

            }
        }
    }

    void generateQuestion()
    {
        QuestionTxt.text = QnA[currentQuestion].Question;
        QnA[currentQuestion].firstPrefab.transform.position =  AllPrefabs.transform.position;
        QnA[currentQuestion].firstPrefab.SetActive(true);
        SetAnswers();
    }
}
